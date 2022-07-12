using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private float stackHeight = 0.3f;
    public Collider Col;
    public Transform StackTrans;
    public GameObject StackObject;
    public Transform BlockTrans;
    private Vector3 localOffset;

    public void OnObjectSpawn()
    {
        StackObject.SetActive(true);
        Col.enabled = true;
        StackTrans.parent = BlockTrans;
        StackTrans.position = BlockTrans.position + localOffset;
    }
    private void Awake()
    {
        localOffset = StackTrans.position - BlockTrans.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstValue.TAG_PLAYER))
        {
            Col.enabled = false;
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.StackList.Push(StackTrans.gameObject);
                player.StackParent.position += Vector3.up * stackHeight;
                player.CharacterTrans.position += Vector3.up * stackHeight;
                StackTrans.position = player.StackRoot.position;
                StackTrans.parent = player.StackParent;

                player.Anim.SetInteger(ConstValue.PLAYER_ANIM, 1);

            }

        }

    }

}


