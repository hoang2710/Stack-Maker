using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 upDir = Vector3.forward;
    private Vector3 downDir = Vector3.back;
    private Vector3 leftDir = Vector3.left;
    private Vector3 rightDir = Vector3.right;
    private Vector3 moveDir = Vector3.zero;
    public Vector3 MoveDir
    {
        get
        {
            return moveDir;
        }
        set
        {
            moveDir = value;
        }
    }
    [SerializeField]
    private float moveSpeed = 20f;
    [SerializeField]
    private float moveSpeedPreEnd = 12f;
    public Transform PlayerTrans;
    public Transform StackRoot;
    public Transform StackParent;
    public Transform CharacterTrans;
    private Vector3 stackDirection;
    public Stack<GameObject> StackList = new Stack<GameObject>();
    public Animator Anim;
    private IEnumerator coroutine;
    private const string ANIM = "renwu";

    void Start()
    {
        InputManager.OnInputUpdate += InputManagerOnInputUpdate;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;

        stackDirection = StackRoot.forward;

        StackRoot.position = transform.position;
        StackParent.position = StackRoot.position;
        StackParent.parent = StackRoot;

        coroutine = SlowSetAnimValue();
        StartCoroutine(coroutine);
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    void OnDestroy()
    {
        InputManager.OnInputUpdate -= InputManagerOnInputUpdate;
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void MovePlayer()
    {
        PlayerTrans.position = Vector3.MoveTowards(PlayerTrans.position, PlayerTrans.position + moveDir, moveSpeed * Time.deltaTime);
    }
    private void InputManagerOnInputUpdate(InputManager.InputType type)
    {
        if (type == InputManager.InputType.Up)
        {
            moveDir = upDir;
            PlayerTrans.rotation = Quaternion.LookRotation(upDir);
            StackRoot.rotation = Quaternion.LookRotation(stackDirection);
        }
        else
        if (type == InputManager.InputType.Down)
        {
            moveDir = downDir;
            PlayerTrans.rotation = Quaternion.LookRotation(downDir);
            StackRoot.rotation = Quaternion.LookRotation(stackDirection);
        }
        else
        if (type == InputManager.InputType.Left)
        {
            moveDir = leftDir;
            PlayerTrans.rotation = Quaternion.LookRotation(leftDir);
            StackRoot.rotation = Quaternion.LookRotation(stackDirection);
        }
        else
        if (type == InputManager.InputType.Right)
        {
            moveDir = rightDir;
            PlayerTrans.rotation = Quaternion.LookRotation(rightDir);
            StackRoot.rotation = Quaternion.LookRotation(stackDirection);
        }
    }

    private void GameManagerOnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.EndLevel)
        {
            StopCoroutine(coroutine);
            Anim.SetInteger(ANIM, 2);
        }

        if (state == GameManager.GameState.PreEndLevel)
        {
            moveSpeed = moveSpeedPreEnd;
        }

    }

    IEnumerator SlowSetAnimValue()
    {
        while (true)
        {
            for (int i = 0; i < 2; i++) yield return null;
            Anim.SetInteger(ANIM, 0);
        }
    }
}
