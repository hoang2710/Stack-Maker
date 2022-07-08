using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: class not used
public class Cam : MonoBehaviour
{
    private Transform playerTrans;
    private Vector3 defaultPosOffset;
    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        defaultPosOffset = Camera.main.transform.position - playerTrans.position;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = playerTrans.position + defaultPosOffset;
    }
}
