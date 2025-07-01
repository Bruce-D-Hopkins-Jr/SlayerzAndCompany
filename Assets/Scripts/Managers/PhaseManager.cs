using System;
using UnityEngine;

[Serializable]
public class PhaseManager : MonoBehaviour
{
    public GamePhase CurrentPhase { get; private set; } = GamePhase.DRAW;

    public enum GamePhase
    {
        DRAW,
        PLAY,
        SLAY,
        MONSTER
    }

    public void AdvancePhase()
    {
        switch (CurrentPhase)
        {
            case GamePhase.DRAW:
                StartDrawPhase();
                CurrentPhase = GamePhase.PLAY;
                break;
            case GamePhase.PLAY:
                StartPlayPhase();
                CurrentPhase = GamePhase.SLAY;
                break;
            case GamePhase.SLAY:
                StartSlayPhase();
                CurrentPhase = GamePhase.MONSTER;
                break;
            case GamePhase.MONSTER:
                StartMonsterPhase();
                CurrentPhase = GamePhase.DRAW;
                break;
        }
    }

    private void StartDrawPhase()
    {
        Debug.Log("Starting DRAW phase.");
        // Notify systems to draw until 5 cards, for example
    }

    private void StartPlayPhase()
    {
        Debug.Log("Starting PLAY phase.");
        // Notify UI to enable card play
    }

    private void StartSlayPhase()
    {
        Debug.Log("Starting SLAY phase.");
        // Enable hero targeting/attacks
    }

    private void StartMonsterPhase()
    {
        Debug.Log("Starting MONSTER phase.");
        // Trigger monster AI
    }
}
