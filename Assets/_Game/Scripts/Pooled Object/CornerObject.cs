using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerObject : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private protected bool isUpLock = false;
    [SerializeField]
    private protected bool isDownLock = false;
    [SerializeField]
    private protected bool isLeftLock = false;
    [SerializeField]
    private protected bool isRightLock = false;
    public Transform CornerBlockObject;
    private bool isStackAvailable = true;
    public Transform StackTrans;
    public GameObject StackObject;
    [SerializeField]
    private float stackHeight = 0.3f;
    public Transform BlockTrans;
    private Vector3 localOffset;

    public virtual void OnObjectSpawn()
    {

        StartCoroutine(DelaySetLockDir());
        StackObject.SetActive(true);
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

        isUpLock = false;
        isDownLock = false;
        isLeftLock = false;
        isRightLock = false;

        if (Physics.Raycast(BlockTrans.position, Vector3.forward, out hit, 1f, ConstValue.WALL_BLOCK_LAYER_MASK))
        {
            isUpLock = true;
        }
        if (Physics.Raycast(BlockTrans.position, Vector3.back, out hit, 1f, ConstValue.WALL_BLOCK_LAYER_MASK))
        {
            isDownLock = true;
        }
        if (Physics.Raycast(BlockTrans.position, Vector3.left, out hit, 1f, ConstValue.WALL_BLOCK_LAYER_MASK))
        {
            isLeftLock = true;
        }
        if (Physics.Raycast(BlockTrans.position, Vector3.right, out hit, 1f, ConstValue.WALL_BLOCK_LAYER_MASK))
        {
            isRightLock = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstValue.TAG_PLAYER))
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

                    player.Anim.SetInteger(ConstValue.PLAYER_ANIM, ConstValue.PLAYER_ANIM_JUMP);

                    isStackAvailable = false;
                }
            }

            InputManager.Instance.UpdateDirectionLock(isUpLock, isDownLock, isLeftLock, isRightLock);
            InputManager.Instance.UpdateInputLock(false);
        }
    }

    IEnumerator DelaySetLockDir()
    {
        yield return new WaitForSeconds(0.5f);
        SetLockDirection();
    }
}
