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
    private Vector3 localStackPosition;

    public void OnObjectSpawn()
    {
        SetLockDirection();
        StackTrans.parent = BlockTrans;
        StackTrans.localPosition = localStackPosition;
    }
    private void Start()
    {
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
    public void SetLockDirection()
    {
        isUpLock = false;
        isDownLock = false;
        isLeftLock = false;
        isRightLock = false;
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
}
