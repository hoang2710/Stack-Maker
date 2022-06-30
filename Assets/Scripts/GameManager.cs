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
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        OnGameStateChanged?.Invoke(state);
    }

    public void OnGameStateLoading()
    {

    }
    public void OnGameStatePlay()
    {

    }
    public void OnGameStateEndLevel()
    {

    }
    public void OnGameStateRestartLevel()
    {

    }
    public void OnGameStatePreEndLevel()
    {

    }
    public void OnGameStateStartGame()
    {

    }

    public enum GameState
    {
        Loading,
        Play,
        EndLevel,
        RestartLevel,
        PreEndLevel,
        StartGame
    }
}
