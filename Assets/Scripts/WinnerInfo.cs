using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WinnerInfo : NetworkBehaviour
{
    [SyncVar]
    public int char_id;
    [SyncVar]
    public string name;
    void Start(){
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("On DontDestroyOnLoad");
    }
}
