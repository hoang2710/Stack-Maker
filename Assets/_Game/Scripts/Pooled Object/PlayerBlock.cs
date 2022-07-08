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
    }
    private void Awake()
    {
        localPlayerOffset = PlayerTrans.position - BlockTrans.position;
    }
}
