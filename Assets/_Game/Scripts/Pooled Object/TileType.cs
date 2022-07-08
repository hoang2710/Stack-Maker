using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileType : MonoBehaviour
{
    public PrefabManager.ObjectType Tag;
    public Vector3 Position;

    private void Start() {
        Position = transform.position;
    }
}
