using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action<GameState> OnGameStateChanged;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Screen.SetResolution(1080, 1920, false);
        Debug.Log(Screen.currentResolution.width + "   " + Screen.currentResolution.height);
        Screen.autorotateToPortrait = true;
    }

    public void ChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Loading:
                OnGameStateLoading();
                break;
            case GameState.Play:
                OnGameStatePlay();
                break;
            case GameState.EndLevel:
                OnGameStateEndLevel();
                break;
            case GameState.RestartLevel:
                OnGameStateRestartLevel();
                break;
            case GameState.PreEndLevel:
                OnGameStatePreEndLevel();
                break;
            case GameState.StartGame:
                OnGameStateStartGame();
                break;
            case GameState.ResultPhase:
                OnGameStateResultPhase();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        OnGameStateChanged?.Invoke(state);
    }

    public void OnGameStateLoading()
    {
        Debug.Log("GameStateLoading");
    }
    public void OnGameStatePlay()
    {
        Debug.Log("GameStatePlay");
    }
    public void OnGameStateEndLevel()
    {
        Debug.Log("GameStateEndLevel");
    }
    public void OnGameStateRestartLevel()
    {
        Debug.Log("GameStateRestartLevel");
    }
    public void OnGameStatePreEndLevel()
    {
        Debug.Log("GameStatePreEndLevel");
    }
    public void OnGameStateStartGame()
    {
        Debug.Log("GameStateStartGame");
    }
    public void OnGameStateResultPhase()
    {
        Debug.Log("GameStateResultPhase");
    }

    public enum GameState
    {
        Loading,
        Play,
        EndLevel,
        RestartLevel,
        PreEndLevel,
        StartGame,
        ResultPhase
    }
}
