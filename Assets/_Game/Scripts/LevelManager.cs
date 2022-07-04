using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level CurLevel = Level.Level_1;
    public int GoldBonus = 0;
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

        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameManager.GameState state)
    {

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
    public enum Level
    {
        Level_1,
        Level_2,
        Level_3,
        Level_4,
        Level_5
    }
}
