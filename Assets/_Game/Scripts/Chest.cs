using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject DefaultChest;
    public GameObject OpenChest;
    public Transform StayPosition;
    void Start()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void GameManagerOnGameStateChanged(GameManager.GameState state)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            player.MoveDir = Vector3.zero;
            player.transform.position = StayPosition.position;

            GameManager.Instance.ChangeGameState(GameManager.GameState.EndLevel);
            StartCoroutine(DelayOpenChest());
        }
    }
    IEnumerator DelayOpenChest()
    {
        yield return new WaitForSeconds(2f);
        OpenChest.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
