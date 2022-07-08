using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : CornerObject
{
    public Transform PlayerTrans;
    private Vector3 localPlayerOffset;

    public override void OnObjectSpawn()
    {
        PlayerTrans.position = BlockTrans.position + localPlayerOffset;
        StartCoroutine(DelaySetLockDir());
    }
    private void Awake()
    {
        localPlayerOffset = PlayerTrans.position - BlockTrans.position;
    }
    IEnumerator DelaySetLockDir()
    {
        yield return new WaitForSeconds(0.5f);
        SetLockDirection();
        
        InputManager.Instance.UpdateDirectionLock(isUpLock, isDownLock, isLeftLock, isRightLock);
        InputManager.Instance.UpdateInputLock(false);
    }
}
