using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExplosion : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerDamage player = collision.GetComponent<PlayerDamage>();
        if (player != null)
        {
            player.TakeDamage(1); 
        }
        Debug.Log(collision.name);
    }
}
