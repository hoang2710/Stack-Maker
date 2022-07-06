using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private float stackHeight = 0.3f;
    public Collider Col;
    public Transform StackTrans;
    public Transform BlockTrans;
    private Vector3 localStackPosition; 

    public void OnObjectSpawn()
    {
        StackTrans.parent = BlockTrans;
        StackTrans.localPosition = localStackPosition;
    }
    private void Start() {
        localStackPosition = StackTrans.localPosition;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void GameManagerOnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Loading)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Col.enabled = false;
            Player player = other.GetComponent<Player>();
            Debug.Log(player.name);

            // if (player.StackParent == null)
            // {
            //     player.StackParent = transform;
            //     player.StackRoot.position = transform.position;
            //     player.StackParent.position = player.StackRoot.position;
            //     player.StackParent.parent = player.StackRoot;
            // }
            // else
            if (player != null)
            {
                player.StackList.Push(StackTrans.gameObject);
                player.StackParent.position += Vector3.up * stackHeight;
                player.CharacterTrans.position += Vector3.up * stackHeight;
                StackTrans.position = player.StackRoot.position;
                StackTrans.parent = player.StackParent;

                player.Anim.SetInteger(ConstValue.PLAYER_ANIM, 1);

                Debug.Log("stack plus");
            }

        }

    }

}


