using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerObject : MonoBehaviour, IPooledObject
{
    public bool isUpLock = false;
    public bool isDownLock = false;
    public bool isLeftLock = false;
    public bool isRightLock = false;
    public Transform CornerBlockObject;
    private bool isStackAvailable = true;
    public Transform StackTrans;
    [SerializeField]
    private float stackHeight = 0.3f;
    public Transform BlockTrans;
    private Vector3 localOffset;

    public void OnObjectSpawn()
    {
        StartCoroutine(DelaySetLockDir());
        StackTrans.parent = BlockTrans;
        StackTrans.position = BlockTrans.position + localOffset;
    }
    private void Awake()
    {
        localOffset = StackTrans.position - BlockTrans.position;
    }

    public void SetLockDirection()
    {
        RaycastHit hit;
        Debug.Log("set value  " + BlockTrans.position);
        if (Physics.Raycast(BlockTrans.position, Vector3.forward,out hit, 1f, ConstValue.WALL_BLOCK_LAYER_MASK))
        {
            isUpLock = true; Debug.Log("up " + hit.transform.name);
        }
        if (Physics.Raycast(BlockTrans.position, Vector3.back,out hit, 1f, ConstValue.WALL_BLOCK_LAYER_MASK))
        {
            isDownLock = true; Debug.Log("down " + hit.transform.name);
        }
        if (Physics.Raycast(BlockTrans.position, Vector3.left,out hit, 1f, ConstValue.WALL_BLOCK_LAYER_MASK))
        {
            isLeftLock = true; Debug.Log("left " + hit.transform.name);
        }
        if (Physics.Raycast(BlockTrans.position, Vector3.right,out hit, 1f, ConstValue.WALL_BLOCK_LAYER_MASK))
        {
            isRightLock = true; Debug.Log("right " + hit.transform.name);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.MoveDir = Vector3.zero;
                player.transform.position = CornerBlockObject.position;

                if (isStackAvailable)
                {
                    player.StackList.Push(StackTrans.gameObject);
                    player.StackParent.position += Vector3.up * stackHeight;
                    player.CharacterTrans.position += Vector3.up * stackHeight;
                    StackTrans.position = player.StackRoot.position;
                    StackTrans.parent = player.StackParent;

                    player.Anim.SetInteger(ConstValue.PLAYER_ANIM, 1);

                    Debug.Log("stack plus");
                    isStackAvailable = false;
                }
            }

            InputManager.Instance.UpdateDirectionLock(isUpLock, isDownLock, isLeftLock, isRightLock);
        }
    }

    IEnumerator DelaySetLockDir(){
        yield return new WaitForSeconds(1f);
        SetLockDirection();
    }
}
