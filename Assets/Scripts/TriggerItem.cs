using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        DropBomb player = collision.GetComponent<DropBomb>();
        if (player != null)
        {
            player.AddBomb(1);
        }
        Debug.Log(collision.name);
    }
}
