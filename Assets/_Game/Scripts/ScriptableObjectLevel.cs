using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectLevel : ScriptableObject
{
    public LevelManager.Level level;
    public List<TileData> tileList;
}
