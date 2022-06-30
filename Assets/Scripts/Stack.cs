using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField]
    private float stackHeight = 0.45f;
    private Collider col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            col.enabled = false;
            Player player = other.GetComponent<Player>();

            // if (player.StackParent == null)
            // {
            //     player.StackParent = transform;
            //     player.StackRoot.position = transform.position;
            //     player.StackParent.position = player.StackRoot.position;
            //     player.StackParent.parent = player.StackRoot;
            // }
            // else
            player.StackList.Push(this.gameObject);
            player.StackParent.position += Vector3.up * stackHeight;
            player.CharacterTrans.position += Vector3.up * stackHeight;
            transform.position = player.StackRoot.position;
            transform.parent = player.StackParent;

            player.anim.SetInteger("renwu", 1);

        }

    }

}


