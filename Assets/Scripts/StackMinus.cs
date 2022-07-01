using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMinus : MonoBehaviour
{
    [SerializeField]
    private float stackHeight = 0.45f;
    public GameObject GoldenPlate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            GameObject tempStack = player.StackList.Peek();
            player.StackList.Pop();
            player.StackParent.position -= Vector3.up * stackHeight;
            player.CharacterTrans.position -= Vector3.up * stackHeight;
            Destroy(tempStack.gameObject);
            GoldenPlate.SetActive(true);

            player.anim.SetInteger("renwu", 1);
            Destroy(this);
        }
    }
}
