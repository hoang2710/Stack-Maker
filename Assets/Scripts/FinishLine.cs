using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject FireCracker;
    void Start()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void GameManagerOnGameStateChanged(GameManager.GameState state)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InputManager.Instance.UpdateInputLock(true);
            GameManager.Instance.ChangeGameState(GameManager.GameState.PreEndLevel);

            if (FireCracker != null)
            {
                FireCracker.SetActive(true);
            }
        }
    }
}
