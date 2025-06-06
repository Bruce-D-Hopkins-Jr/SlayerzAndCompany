using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Deck deck;
    public Player player;
    public Player ai;
    public MonsterCard currentMonster;

    enum GamePhase { DRAW, PLAY, SLAY, MONSTERTURN }
    private GamePhase currentPhase = GamePhase.DRAW;

    private Player activePlayer;
    private bool playerTurn = true;

    void Start()
    {
        deck.Shuffle();
        DrawStartingHands();
        currentMonster.SpawnMonster();
        activePlayer = player;        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleTurn();
        }
    }
    void HandleTurn()
    {
        switch (currentPhase)
        {
            case GamePhase.DRAW:
                DrawPhase();
                break;
            case GamePhase.PLAY:
                PlayPhase();
                break;
            case GamePhase.SLAY:
                SlayPhase();
                break;
            case GamePhase.MONSTERTURN:
                MonsterTurn();
                break;
        }
    }

    void DrawStartingHands()
    {
        for (int i = 0; i < 5; i++)
        {
            Card playerCard = deck.DrawCard();
            if (playerCard != null)
            {
                player.hand.Add(playerCard);
                if (playerCard is HeroCard hero) hero.InitializeStats();
                if (playerCard is PlayCard play) play.InitializeValue();
            }

            Card aiCard = deck.DrawCard();
            if (aiCard != null)
            {
                ai.hand.Add(aiCard);
                if (aiCard is HeroCard hero) hero.InitializeStats();
                if (aiCard is PlayCard play) play.InitializeValue();
            }
        }

        Debug.Log("Both players have drawn their starting hands.");
    }

    void DrawPhase()
    {
        Card drawn = deck.DrawCard();
        if (drawn is HeroCard hero) hero.InitializeStats();
        if (drawn is PlayCard play) play.InitializeValue();
        if (drawn != null)
        {
            activePlayer.hand.Add(drawn);
            Debug.Log($"{activePlayer.name} drew {drawn.cardName}");
        }

        activePlayer.ResetTurn(); // reset play limits

        currentPhase = GamePhase.PLAY;
    }

    void PlayPhase()
    {
        // NOTE: Replace this with UI interaction later
        Card heroToPlay = activePlayer.hand.FirstOrDefault(c => c.cardType == CardType.HERO);
        if (!activePlayer.playedHero && heroToPlay is HeroCard heroCard && activePlayer.heroes.Count < 3)
        {
            activePlayer.heroes.Add(heroCard);
            activePlayer.hand.Remove(heroCard);

            activePlayer.SpawnHero(heroCard);

            activePlayer.playedHero = true;
            Debug.Log($"{activePlayer.name} played hero {heroCard.cardName}");
        }

        Card playCardToUse = activePlayer.hand.FirstOrDefault(c => c.cardType == CardType.PLAY);
        if (!activePlayer.playedPlayCard && playCardToUse is PlayCard playCard)
        {
            ApplyPlayCard(playCard, activePlayer == player ? ai : player);
            activePlayer.hand.Remove(playCard);
            activePlayer.playedPlayCard = true;
        }

        currentPhase = GamePhase.SLAY;
    }

    void SlayPhase()
    {
        HeroCard attacker = activePlayer.heroes.FirstOrDefault();
        if (attacker != null && currentMonster != null)
        {
            currentMonster.currentHitPoints -= attacker.currentAttackPoints;
            Debug.Log($"{attacker.cardName} attacked monster for {attacker.currentAttackPoints}!");

            if (currentMonster.currentHitPoints <= 0)
            {
                Debug.Log($"{activePlayer.name} slayed the monster! Game Over!");
                return;
            }
        }

        currentPhase = GamePhase.MONSTERTURN;
    }

    void MonsterTurn()
    {
        MonsterAttack();
        CheckWinConditions();

        // Switch player and reset for next cycle
        playerTurn = !playerTurn;
        activePlayer = playerTurn ? player : ai;
        currentPhase = GamePhase.DRAW;
    }

    void ApplyPlayCard(PlayCard card, Player opponent)
    {
        if (card.effectType == PlayCardType.HEAL)
        {
            activePlayer.lifePoints += card.currentEffectValue;
            Debug.Log($"{activePlayer.name} healed for {card.currentEffectValue}. LP: {activePlayer.lifePoints}");
        }
        else if (card.effectType == PlayCardType.DAMAGE)
        {
            if (currentMonster != null)
            {
                currentMonster.currentHitPoints -= card.currentEffectValue;
                Debug.Log($"{activePlayer.name} dealt {card.currentEffectValue} damage to the monster!");
            }
        }
    }

    void MonsterAttack()
    {
        Player target = Random.value < 0.5f ? player : ai;
        Debug.Log($"Monster attacks {target.name}!");

        if (target.heroes.Count > 0)
        {
            HeroCard targetHero = target.heroes[Random.Range(0, target.heroes.Count)];
            targetHero.currentHitPoints -= currentMonster.attackPoints;
            Debug.Log($"Monster hits {targetHero.cardName} for {currentMonster.attackPoints}");

            if (targetHero.currentHitPoints <= 0)
            {
                Debug.Log($"{targetHero.cardName} was defeated!");
                target.heroes.Remove(targetHero);
            }
        }
        else
        {
            target.TakeDamage(currentMonster.attackPoints);
        }
    }

    void CheckWinConditions()
    {
        if (currentMonster.currentHitPoints <= 0)
        {
            Debug.Log("Monster defeated! You win!");
            Time.timeScale = 0;
        }
        else if (player.lifePoints <= 0 && ai.lifePoints <= 0)
        {
            Debug.Log("It's a draw! Everyone lost!");
            Time.timeScale = 0;
        }
        else if (player.lifePoints <= 0)
        {
            Debug.Log("AI wins!");
            Time.timeScale = 0;
        }
        else if (ai.lifePoints <= 0)
        {
            Debug.Log("Player wins!");
            Time.timeScale = 0;
        }
    }
}
