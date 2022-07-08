using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    private Vector2 mouseDownPos;
    private Vector2 mouseUpPos;
    private bool isSwipe = false;
    private float swipeDetectTriggerLine = 0.6f;  //cos(45) ~ 1
    [SerializeField]
    private float sensitiveThreshold = 150f;
    [SerializeField]
    private bool isUpLock = false;
    [SerializeField]
    private bool isDownLock = false;
    [SerializeField]
    private bool isLeftLock = false;
    [SerializeField]
    private bool isRightLock = false;
    [SerializeField]
    private bool isInputLock = false;
    private bool isMove = false;


    public static InputManager Instance { get; private set; }
    public static event Action<InputType> OnInputUpdate;
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
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseUpPos = Input.mousePosition;
            isSwipe = true;
        }

        if (isSwipe)
        {
            isSwipe = false;
            if (!isInputLock)
            {
                isMove = false;

                if ((mouseUpPos - mouseDownPos).magnitude > sensitiveThreshold)
                {
                    Vector2 dir = (mouseUpPos - mouseDownPos).normalized;

                    if (dir.x > swipeDetectTriggerLine && !isRightLock)
                    {
                        TriggerInput(InputType.Right);
                        isMove = true;
                    }
                    else
                    if (dir.x < -swipeDetectTriggerLine && !isLeftLock)
                    {
                        TriggerInput(InputType.Left);
                        isMove = true;
                    }
                    else
                    if (dir.y > swipeDetectTriggerLine && !isUpLock)
                    {
                        TriggerInput(InputType.Up);
                        isMove = true;
                    }
                    else
                    if (dir.y < -swipeDetectTriggerLine && !isDownLock)
                    {
                        TriggerInput(InputType.Down);
                        isMove = true;
                    }
                }

                if (isMove)
                {
                    isInputLock = true;
                }
            }
        }
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
                isInputLock = false;
                break;
            case GameManager.GameState.ResultPhase:
                isUpLock = false;
                isDownLock = false;
                isLeftLock = false;
                isRightLock = false;
                break;
            default:
                break;
        }
    }
    public void TriggerInput(InputType input)
    {
        OnInputUpdate?.Invoke(input);
    }
    public void UpdateDirectionLock(bool up, bool down, bool left, bool right)
    {
        isUpLock = up;
        isDownLock = down;
        isLeftLock = left;
        isRightLock = right;
    }
    public void UpdateInputLock(bool val)
    {
        isInputLock = val;
    }

    public enum InputType
    {
        Up,
        Down,
        Left,
        Right
    }
}
