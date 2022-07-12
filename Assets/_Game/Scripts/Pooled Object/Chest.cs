using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IPooledObject
{
    public GameObject DefaultChest;
    public GameObject OpenChest;
    public Transform StayPosition;

    public void OnObjectSpawn()
    {
        DefaultChest.SetActive(true);
        OpenChest.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstValue.TAG_PLAYER))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.MoveDir = Vector3.zero;
                player.transform.position = StayPosition.position;

                GameManager.Instance.ChangeGameState(GameManager.GameState.EndLevel);
                StartCoroutine(DelayOpenChest());
            }
        }
    }
    IEnumerator DelayOpenChest()
    {
        yield return new WaitForSeconds(2f);
        OpenChest.SetActive(true);
        DefaultChest.SetActive(false);
    }
}
