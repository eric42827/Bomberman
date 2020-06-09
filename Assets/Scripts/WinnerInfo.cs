using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class WinnerInfo : NetworkBehaviour
{
    [SyncVar]
    public int char_id;
    [SyncVar]
    public string name;
}
