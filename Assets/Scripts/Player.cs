using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 upDir;
    private Vector3 downDir;
    private Vector3 leftDir;
    private Vector3 rightDir;
    private Vector3 moveDir;
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
    private float moveSpeed = 2f;
    public Transform StackRoot;
    public Transform StackParent;
    private Collider col;
    public Transform CharacterTrans;
    private Vector3 stackDirection;
    public Stack<GameObject> StackList;
    public Animator anim;
    private IEnumerator coroutine;

    void Awake()
    {
        InputManager.OnInputUpdate += InputManagerOnInputUpdate;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    void Start()
    {
        upDir = new Vector3(0, 0, 1);
        downDir = new Vector3(0, 0, -1);
        leftDir = new Vector3(-1, 0, 0);
        rightDir = new Vector3(1, 0, 0);
        moveDir = new Vector3(0, 0, 0);

        col = GetComponent<Collider>();

        stackDirection = StackRoot.forward;

        StackRoot.position = transform.position;
        StackParent.position = StackRoot.position;
        StackParent.parent = StackRoot;

        coroutine = SlowSetAnimValue();
        StartCoroutine(coroutine);

        StackList = new Stack<GameObject>();
    }
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDir, moveSpeed * Time.deltaTime);
    }
    void OnDestroy()
    {
        InputManager.OnInputUpdate -= InputManagerOnInputUpdate;
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void InputManagerOnInputUpdate(InputManager.InputType type)
    {
        if (type == InputManager.InputType.Up)
        {
            moveDir = upDir;
            transform.rotation = Quaternion.LookRotation(upDir);
            StackRoot.rotation = Quaternion.LookRotation(stackDirection);
        }
        else
        if (type == InputManager.InputType.Down)
        {
            moveDir = downDir;
            transform.rotation = Quaternion.LookRotation(downDir);
            StackRoot.rotation = Quaternion.LookRotation(stackDirection);
        }
        else
        if (type == InputManager.InputType.Left)
        {
            moveDir = leftDir;
            transform.rotation = Quaternion.LookRotation(leftDir);
            StackRoot.rotation = Quaternion.LookRotation(stackDirection);
        }
        else
        if (type == InputManager.InputType.Right)
        {
            moveDir = rightDir;
            transform.rotation = Quaternion.LookRotation(rightDir);
            StackRoot.rotation = Quaternion.LookRotation(stackDirection);
        }
    }

    private void GameManagerOnGameStateChanged(GameManager.GameState state){
        if(state == GameManager.GameState.EndLevel){
            StopCoroutine(coroutine);
            anim.SetInteger("renwu", 2);
        }
    }

    IEnumerator SlowSetAnimValue()
    {
        while (true)
        {
            for (int i = 0; i < 4; i++) yield return null;
            anim.SetInteger("renwu", 0);
        }
    }
}
