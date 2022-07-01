using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerObject : MonoBehaviour
{
    public bool isUpLock = false;
    public bool isDownLock = false;
    public bool isLeftLock = false;
    public bool isRightLock = false;
    public bool isPreEndTrigger = false;
    public bool isEndTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            {
                if (!isPreEndTrigger)
                {
                    player.MoveDir = Vector3.zero;
                    player.transform.position = transform.position;
                }
            }

            InputManager.Instance.UpdateDirectionLock(isUpLock, isDownLock, isLeftLock, isRightLock);

            if (isEndTrigger)
            {
                GameManager.Instance.ChangeGameState(GameManager.GameState.EndLevel);
            }
            if (isPreEndTrigger)
            {
                GameManager.Instance.ChangeGameState(GameManager.GameState.PreEndLevel);
            }

        }
    }
}
