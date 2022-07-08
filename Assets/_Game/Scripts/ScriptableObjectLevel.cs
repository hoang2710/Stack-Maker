using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectLevel : ScriptableObject
{
    public LevelManager.Level level;
    [SerializeField]
    public List<TileData> tileList;
}
