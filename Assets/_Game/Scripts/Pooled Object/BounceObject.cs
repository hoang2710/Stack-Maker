using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: class not finish for use
public class BounceObject : CornerObject
{
    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        
    }
    private void SetBounceDirection(){

    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                
            }

            
            InputManager.Instance.UpdateInputLock(true);
        }
    }
}
