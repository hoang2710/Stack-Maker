using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    public Level CurLevel = Level.Level_1;
    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Loading:
                LoadLevel();
                break;
            default:
                break;
        }
    }
    public int GetGoldBonus()
    {
        int goldCount = 0;
        switch (CurLevel)
        {
            case Level.Level_1:
                goldCount = ConstValue.GOLD_AT_LVL_1;
                break;
            case Level.Level_2:
                goldCount = ConstValue.GOLD_AT_LVL_2;
                break;
            case Level.Level_3:
                goldCount = ConstValue.GOLD_AT_LVL_3;
                break;
            case Level.Level_4:
                goldCount = ConstValue.GOLD_AT_LVL_4;
                break;
            case Level.Level_5:
                goldCount = ConstValue.GOLD_AT_LVL_5;
                break;
            default:
                break;
        }
        return goldCount;
    }
    public void PlayAgain()
    {

    }
    public void NextLevel()
    {

    }
    public void LoadLevel()
    {
        var level = Resources.Load<ScriptableObjectLevel>($"LevelData/level {(int)CurLevel}");

        if (level != null)
        {
            Debug.Log("data found");
            if (!ClearLevelEditor())
            {
                ClearLevel();
            }
            foreach (var item in level.tileList)
            {
                // Debug.Log(item.Tag);
                PrefabManager.Instance.PopFromPool(item.Tag, item.Position, Quaternion.identity);
            }
        }
        Debug.Log("level loaded");
    }
    public void ClearLevel()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Tile Block");
        foreach (var item in objs)
        {
            item.SetActive(false);
        }
    }


    public enum Level
    {
        Null,
        Level_1,
        Level_2,
        Level_3,
        Level_4,
        Level_5
    }

#if UNITY_EDITOR

    public Level CurLevelEditor = Level.Level_1;

    public void SaveLevel()
    {

        var newLevel = ScriptableObject.CreateInstance<ScriptableObjectLevel>();

        newLevel.level = CurLevelEditor;
        newLevel.name = $"level {(int)CurLevelEditor}";

        GameObject[] tile = GameObject.FindGameObjectsWithTag("Tile Block");
        List<TileData> tileDatas = new List<TileData>();
        foreach (var item in tile)
        {
            TileType tmp = item.GetComponent<TileType>();
            TileData tileData = new TileData();
            tileData.Tag = tmp.Tag;
            tileData.Position = tmp.Position;
            Debug.Log(tileData.Tag + "  " + tileData.Position);
            tileDatas.Add(tileData);
        }
        Debug.Log(tileDatas.Count);
        newLevel.tileList = tileDatas;
        Debug.Log(newLevel.tileList.Count + " tileList");
        SaveToAsset(newLevel);

    }
    public bool ClearLevelEditor()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Tile Block");
        if (objs != null)
        {
            foreach (var item in objs)
            {
                DestroyImmediate(item);
            }
        }

        return true;
    }
    public void CountWideBlockNeedToBuild()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Tile Block");

        int stackCount = 0;
        int stackMinusCount = 0;
        if (objs != null)
        {
            foreach (var item in objs)
            {
                TileType tt = item.GetComponent<TileType>();
                switch (tt.Tag)
                {
                    case PrefabManager.ObjectType.StackBlock:
                        stackCount++;
                        break;
                    case PrefabManager.ObjectType.BridgeBlock:
                        stackMinusCount++;
                        break;
                    case PrefabManager.ObjectType.WideBrigeBlock:
                        stackMinusCount++;
                        break;
                    case PrefabManager.ObjectType.CornerBlock:
                        stackCount++;
                        break;
                    default:
                        break;
                }
            }
        }

        Debug.Log(stackCount - stackMinusCount);
    }
    private void SaveToAsset(ScriptableObjectLevel level)
    {
        AssetDatabase.CreateAsset(level, $"Assets/_Game/Resources/LevelData/{level.name}.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
}
