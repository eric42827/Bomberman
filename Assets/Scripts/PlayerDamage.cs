using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerDamage : NetworkBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthBar;
    public HealthBar healthBarFollow;
    // Start is called before the first frame update
    void Start()
    {
        if(this.isLocalPlayer)
        {
            Debug.Log("Starting player damage");
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            Debug.Log(transform.GetChild(0));
            transform.GetChild(0).gameObject.SetActive(false);
            //healthBarFollow = transform.GetChild(0).transform.GetChild(0).GetComponent<HealthBar>();
            //healthBarFollow.SetMaxHealth(maxHealth);
        }
        // Health bar follow
        else
        {
            currentHealth = maxHealth;
            healthBarFollow = transform.GetChild(0).transform.GetChild(0).GetComponent<HealthBar>();
            healthBarFollow.SetMaxHealth(maxHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }*/
        if (this.isLocalPlayer && healthBar.GetHealth() <= 0)
        {
            //Debug.Log("Dead");
            CmdDestroyPlayer();
            
        }
    }

    public void TakeDamage(int damage)
    {
        if(this.isLocalPlayer)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            //healthBarFollow.SetHealth(currentHealth);
        }
        else
        {
            currentHealth -= damage;
            healthBarFollow.SetHealth(currentHealth);
        }
    }

    [Command]
    void CmdDestroyPlayer()
    {
        if(NetworkServer.active)
        {
            Destroy(gameObject);
            NetworkServer.Destroy(gameObject);
        }
    }
}
