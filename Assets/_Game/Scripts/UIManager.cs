using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject SettingMenuDropdown;
    public GameObject InGamePanel;
    public GameObject EndGamePanel;
    public TMP_Text GoldText;
    public TMP_Text GoldBonusText;
    public static UIManager Instance { get; private set; }
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
            case GameManager.GameState.Play:
                InGamePanel.SetActive(true);
                EndGamePanel.SetActive(false);
                break;
            case GameManager.GameState.PreEndLevel:
                InGamePanel.SetActive(false);
                UpdateGoldBonus();
                break;
            case GameManager.GameState.ResultPhase:
                EndGamePanel.SetActive(true);
                break;
            default:
                break;
        }

    }
    public void UpdatePlayerGoldUI(int count)
    {
        GoldText.text = count.ToString();
    }
    public void UpdateGoldBonus()
    {
        int goldCount = LevelManager.Instance.GetGoldBonus();
        Debug.Log(LevelManager.Instance.CurLevel + "   " + goldCount);
        GoldBonusText.text = "+" + goldCount.ToString();
    }
    public void OnClickSettingButton()
    {
        SettingMenuDropdown.SetActive(!SettingMenuDropdown.activeInHierarchy);
    }
    public void OnClickPlayAgainButton()
    {
        LevelManager.Instance.PlayAgain();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.ChangeGameState(GameManager.GameState.Play);
    }
    public void OnClickNextLevelButton()
    {
        LevelManager.Instance.NextLevel();
        GameManager.Instance.ChangeGameState(GameManager.GameState.Play);
    }
}
