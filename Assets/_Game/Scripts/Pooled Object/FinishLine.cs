using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinishLine : MonoBehaviour, IPooledObject
{
    public GameObject FireCracker;

    public void OnObjectSpawn()
    {
        FireCracker.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstValue.TAG_PLAYER))
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
