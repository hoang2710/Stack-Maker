using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public Animator anim;
    public CinemachineVirtualCamera[] cinemachines;
    public static CameraManager Instance { get; private set; }
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
    void Start()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    public void GameManagerOnGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Loading:
                anim.Play("Default Cam");
                break;
            case GameManager.GameState.Play:
                Transform playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

                if (playerTrans != null)
                {
                    foreach (var item in cinemachines)
                    {
                        item.m_Follow = playerTrans.transform;
                        item.m_LookAt = playerTrans.transform;
                    }
                }
                break;
            case GameManager.GameState.PreEndLevel:
                anim.Play("Pre End Cam");
                break;
            case GameManager.GameState.EndLevel:
                anim.Play("End Cam");
                break;
            default:
                break;
        }
    }

}
