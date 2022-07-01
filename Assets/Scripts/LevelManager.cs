using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject Chest;
    public GameObject OpenChestPrefab;
    public float RotateX;
    public float RotateY;
    public float RotateZ;
    public static LevelManager Instance { get; private set; }
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

        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameManager.GameState state){
        if(state == GameManager.GameState.EndLevel){
            Instantiate(OpenChestPrefab, Chest.transform.position, Quaternion.Euler(RotateX,RotateY,RotateZ));
            Destroy(Chest);
        }
    }


}
