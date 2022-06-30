using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerObject : MonoBehaviour
{
    public bool isUpLock = false;
    public bool isDownLock = false;
    public bool isLeftLock = false;
    public bool isRightLock = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            {
                player.MoveDir = Vector3.zero;
                player.transform.position = transform.position;
            }

            InputManager.Instance.UpdateDirectionLock(isUpLock, isDownLock, isLeftLock, isRightLock);

            if(isUpLock&&isDownLock&&isLeftLock&&isRightLock){
                GameManager.Instance.ChangeGameState(GameManager.GameState.EndLevel);
            }

        }
    }
}
