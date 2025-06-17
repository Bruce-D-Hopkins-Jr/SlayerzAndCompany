using System.Linq;
using UnityEngine;

/// <summary>
/// Controls the main turn-based game loop and manages core state transitions.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Game State References")]
    public Deck deck;
    public Player player;
    public MonsterCard currentMonster;

    public enum GamePhase { DRAW, PLAY, SLAY, MONSTER }
    [SerializeField] private GamePhase currentPhase = GamePhase.DRAW;

    [HideInInspector]public Player activePlayer;
    private Transform monsterPosition;

    [Header("UI Butttons")]
    public GameObject drawButton;
    public GameObject nextPhaseButton;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Set up singleton instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        deck.Shuffle();
        DrawStartingHands();
        currentMonster.SpawnMonster();
        activePlayer = player;
        UpdatePhaseButtons();
    }

    private void Update()
    {
        monsterPosition = GameObject.Find("MonsterPosition").transform;
        if (currentPhase != GamePhase.PLAY)
        {
            monsterPosition.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            monsterPosition.GetComponent<BoxCollider>().enabled = true;
        }
    }

    /// <summary>
    /// Handles turn logic based on the current phase of the game.
    /// </summary>
    private void HandleTurn()
    {
        switch (currentPhase)
        {
            case GamePhase.DRAW:
                break;
            case GamePhase.PLAY:
                PlayPhase();
                break;
            case GamePhase.SLAY:
                SlayPhase();
                break;
            case GamePhase.MONSTER:
                MonsterPhase();
                break;
        }
    }

    /// <summary>
    /// Initializes both players' starting hands.
    /// </summary>
    private void DrawStartingHands()
    {
        for (int i = 0; i < 5; i++)
        {
            Card playerCard = deck.DrawCard();
            if (playerCard == null) continue;

            player.hand.Add(playerCard);

            if (playerCard is HeroCard hero) hero.InitializeStats();
            if (playerCard is PlayCard play) play.InitializeValue();

            HandManager.Instance.AddCardToHand(playerCard);
        }

        Debug.Log("Both players have drawn their starting hands.");
    }

    /// <summary>
    /// Draw phase logic where the player draws one card and prepares for play.
    /// </summary>
    private void DrawCard()
    { 
        Card drawn = deck.DrawCard();
        if (drawn == null) return;

        if (drawn is HeroCard hero) hero.InitializeStats();
        if (drawn is PlayCard play) play.InitializeValue();

        activePlayer.hand.Add(drawn);
        Debug.Log($"{activePlayer.name} drew {drawn.cardName}");

        HandManager.Instance.AddCardToHand(drawn);
    }

    /// <summary>
    /// Attempts to play one hero and one play card, then moves to the Slay phase.
    /// </summary>
    private void PlayPhase()
    {       
        // TODO: Replace auto-play logic with actual player input via UI
        Debug.Log("Play Phase: waiting for player actions...");

        UpdatePhaseButtons();
    }

    /// <summary>
    /// Hero attacks the monster. If the monster is defeated, the game ends.
    /// </summary>
    private void SlayPhase()
    {
        HeroCard attacker = activePlayer.heroes.FirstOrDefault();
        if (attacker != null && currentMonster != null)
        {
            currentMonster.currentHitPoints -= attacker.currentAttackPoints;
            Debug.Log($"{attacker.cardName} attacked the monster for {attacker.currentAttackPoints} damage!");

            if (currentMonster.currentHitPoints <= 0)
            {
                Debug.Log($"{activePlayer.name} slayed the monster! Game Over!");
                return;
            }
        }

        currentPhase = GamePhase.MONSTER;
        UpdatePhaseButtons();
    }

    /// <summary>
    /// The monster performs its attack, then the game checks win/loss conditions and begins a new round.
    /// </summary>
    private void MonsterPhase()
    {       
        MonsterAttack();
        CheckWinConditions();

        // For now, only one player
        activePlayer = player;
        currentPhase = GamePhase.DRAW;
        UpdatePhaseButtons();
    }

    /// <summary>
    /// Player presses NextPhase Button and game advances to the next phase.
    /// </summary>
    public void AdvancePhaseManually()
    {
        switch (currentPhase)
        {
            case GamePhase.DRAW:
                break;

            case GamePhase.PLAY:
                if (activePlayer.playedHero && activePlayer.playedPlayCard)
                {
                    currentPhase = GamePhase.SLAY;
                    Debug.Log("Play phase complete. Moving to Slay phase.");                    
                }
                else
                {
                    Debug.LogWarning("You must play both a hero and a play card before ending your turn.");
                }
                break;

            case GamePhase.SLAY:
                SlayPhase();
                currentPhase = GamePhase.MONSTER;
                Debug.Log("Moving to Monster turn...");
                
                break;

            case GamePhase.MONSTER:
                MonsterPhase();
                currentPhase = GamePhase.DRAW;
                Debug.Log("New round begins. Back to draw phase.");
                break;
        }
    }

    private void MonsterAttack()
    {
        // Monster attacks the first hero
        HeroCard target = activePlayer.heroes.FirstOrDefault();
        if (target != null)
        {
            target.currentHitPoints -= currentMonster.attackPoints;
            Debug.Log($"Monster attacked {target.cardName} for {currentMonster.attackPoints} damage.");

            if (target.currentHitPoints <= 0)
            {
                Debug.Log($"{target.cardName} was defeated.");
                activePlayer.heroes.Remove(target);
                Destroy(target.modelInstance);
                target.modelInstance = null;
            }
        }
    }

    public void ApplyHealPlayCard(PlayCard card)
    {
        // Heal the first available hero
        HeroCard target = activePlayer.heroes.FirstOrDefault();
        if (target != null)
        {
            target.currentHitPoints += card.currentEffectValue;
            activePlayer.playedPlayCard = true;
            Debug.Log($"{target.cardName} healed for {card.currentEffectValue} HP.");
        }
    }

    public void ApplyHealPlayCard(PlayCard card, HeroCard target)
    {
        if (target != null)
        {
            target.currentHitPoints += card.currentEffectValue;
            activePlayer.playedPlayCard = true;
            Debug.Log($"{target.cardName} healed for {card.currentEffectValue} HP");
        }
    }

    public void ApplyDamagePlayCard(PlayCard card)
    {
        if (currentMonster != null)
        {
            currentMonster.currentHitPoints -= card.currentEffectValue;
            activePlayer.playedPlayCard = true;
            Debug.Log($"Monster took {card.currentEffectValue} damage from play card.");

            if (currentMonster.currentHitPoints <= 0)
            {
                Debug.Log("Monster defeated by play card!");
            }
        }
    }

    /// <summary>
    /// Summons a hero to the specified world-space transform. Delegates to the active player.
    /// </summary>
    /// <param name="heroData">The hero card to summon.</param>
    /// <param name="spawnPoint">The transform where the hero should appear.</param>
    public void SummonHero(HeroCard heroData, Transform spawnPoint)
    {
        if (activePlayer != null)
        {
            activePlayer.heroes.Add(heroData);
            activePlayer.SpawnHero(heroData, spawnPoint);
            activePlayer.playedHero = true;
            activePlayer.hand.Remove(heroData);
            Debug.Log($"{activePlayer.name} summoned {heroData.cardName} at {spawnPoint.name}");
        }
        else
        {
            Debug.LogWarning("No active player set. Cannot summon hero.");
        }
    }

    public GamePhase GetCurrentPhase() => currentPhase;

    public void OnDrawButtonPressed()
    {
        Debug.Log("Draw button pressed... Moving to PLAY phase...");
        DrawCard();

        currentPhase = GamePhase.PLAY;
        UpdatePhaseButtons();
    }

    private void UpdatePhaseButtons()
    {
        if (drawButton == null || nextPhaseButton == null) return;
        
        drawButton.SetActive(currentPhase == GamePhase.DRAW);
        nextPhaseButton.SetActive(currentPhase != GamePhase.DRAW );        
    }

    /// <summary>
    /// Checks win conditions to determine the victor!
    /// </summary>
    private void CheckWinConditions()
    {
        if (activePlayer.heroes.Count == 0)
        {
            Debug.Log($"{activePlayer.name} has no heroes left. Game Over.");
        }
    }
}

