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
    private int gold = 0;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            UIManager.Instance.UpdatePlayerGoldUI(value);
        }
    }

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

        UIManager.Instance.UpdatePlayerGoldUI(gold);
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
        switch (type)
        {
            case InputManager.InputType.Up:

                moveDir = upDir;
                PlayerTrans.rotation = Quaternion.LookRotation(upDir);
                StackRoot.rotation = Quaternion.LookRotation(stackDirection);
                break;
            case InputManager.InputType.Down:
                moveDir = downDir;
                PlayerTrans.rotation = Quaternion.LookRotation(downDir);
                StackRoot.rotation = Quaternion.LookRotation(stackDirection);
                break;
            case InputManager.InputType.Left:
                moveDir = leftDir;
                PlayerTrans.rotation = Quaternion.LookRotation(leftDir);
                StackRoot.rotation = Quaternion.LookRotation(stackDirection);
                break;
            case InputManager.InputType.Right:
                moveDir = rightDir;
                PlayerTrans.rotation = Quaternion.LookRotation(rightDir);
                StackRoot.rotation = Quaternion.LookRotation(stackDirection);
                break;
            default:
                break;
        }
    }

    private void GameManagerOnGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.EndLevel:
                StopCoroutine(coroutine);
                Anim.SetInteger(ConstValue.PLAYER_ANIM, 2);
                break;
            case GameManager.GameState.PreEndLevel:
                moveSpeed = moveSpeedPreEnd;
                break;
            case GameManager.GameState.ResultPhase:
                int goldBonus = LevelManager.Instance.GetGoldBonus();
                gold += goldBonus;
                Debug.Log(gold);
                break;
            default:
                break;
        }
    }

    IEnumerator SlowSetAnimValue()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++) yield return null;
            Anim.SetInteger(ConstValue.PLAYER_ANIM, 0);
        }
    }
}
