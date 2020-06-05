using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class WinnerInfo : NetworkBehaviour
{
    [SyncVar]
    public int char_id;
    [SyncVar]
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
