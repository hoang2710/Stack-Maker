using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public Animator anim;
    public GameObject[] cinemachines;
    public int fullWidthUnits = 8;
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
        if (state == GameManager.GameState.Play)
        {
            anim.Play("Default Cam");
        }

        if (state == GameManager.GameState.PreEndLevel)
        {
            anim.Play("Pre End Cam");
        }

        if (state == GameManager.GameState.EndLevel)
        {
            anim.Play("End Cam");
        }
    }

    IEnumerator DelaySettingCinemachine()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return null;
        }

        float ratio = (float)Screen.height / (float)Screen.width;
        foreach (var item in cinemachines)
        {
            item.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = (float)fullWidthUnits * ratio / 2.0f;
        }
    }
}
