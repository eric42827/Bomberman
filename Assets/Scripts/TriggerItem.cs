using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class TriggerItem : NetworkBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        DropBomb player = collision.GetComponent<DropBomb>();
        if (player != null)
        {
            player.AddBomb(1);
        }
        Debug.Log(collision.name);
        Destroy(this.gameObject);
        NetworkServer.Destroy(this.gameObject);
    }
}
