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
    private bool isUpLock = false;
    private bool isDownLock = false;
    private bool isLeftLock = false;
    private bool isRightLock = false;
    private bool isInputLock = false;


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
                Debug.Log((mouseUpPos - mouseDownPos).magnitude + "   " + mouseDownPos + "   " + mouseUpPos +"  " + (mouseUpPos - mouseDownPos));
                if ((mouseUpPos - mouseDownPos).magnitude > sensitiveThreshold)
                {
                    Vector2 dir = (mouseUpPos - mouseDownPos).normalized;
                    if (dir.x > swipeDetectTriggerLine && !isRightLock)
                    {
                        TriggerInput(InputType.Right);
                    }
                    else
                    if (dir.x < -swipeDetectTriggerLine && !isLeftLock)
                    {
                        TriggerInput(InputType.Left);
                    }
                    else
                    if (dir.y > swipeDetectTriggerLine && !isUpLock)
                    {
                        TriggerInput(InputType.Up);
                    }
                    else
                    if (dir.y < -swipeDetectTriggerLine && !isDownLock)
                    {
                        TriggerInput(InputType.Down);
                    }
                }
            }
        }
    }

    private void GameManagerOnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Play)
        {
            isInputLock = false;
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
