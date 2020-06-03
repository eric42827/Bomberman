using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DestroyExplosion : NetworkBehaviour
{
    public float delay = 0f;
 
     // Use this for initialization
    void Start () 
    {
        Destroy(this.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
    }
}
