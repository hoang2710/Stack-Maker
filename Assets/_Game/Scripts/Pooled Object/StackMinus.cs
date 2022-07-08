using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMinus : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private float stackHeight = 0.45f;
    public GameObject GoldenPlate;
    public Collider Col;

    public void OnObjectSpawn()
    {
        Col.enabled = true;
        GoldenPlate.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                GameObject tempStack = player.StackList.Peek();
                player.StackList.Pop();
                player.StackParent.position -= Vector3.up * stackHeight;
                player.CharacterTrans.position -= Vector3.up * stackHeight;
                tempStack.gameObject.SetActive(false);
                GoldenPlate.SetActive(true);

                player.Anim.SetInteger(ConstValue.PLAYER_ANIM, 1);
                Col.enabled = false;
            }
        }
    }
}
