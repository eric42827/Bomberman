using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WinnerInfo : NetworkBehaviour
{
    // [SyncVar(hook = "OnCharIdChange")]
    [SyncVar]
    public int char_id;
    // [SyncVar(hook = "OnNameChange")]
    [SyncVar]
    public string name;
    // Start is called before the first frame update
    void OnCharIdChange(int id){
        char_id = id;
    }
    void OnNameChange(string n){
        Debug.Log("@ OnNameChange");
        name = n;
    }
}
