using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Networking;

public class DropBomb : NetworkBehaviour
{
    public Rigidbody2D rb;
    public Tilemap tilemap;
	public GameObject bombPrefab;
    private int currentBomb;
    public int MaxBomb;
    public int currentExplosion;
    public int MaxExplosion;
    private float countdown;
    private bool cool = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && this.isLocalPlayer)
        {
            if (currentBomb < MaxBomb)
            {
                currentBomb++;
                CmdDropBomb();
            } 
        }
        if (currentBomb == MaxBomb)
        {
            if (!cool)
            {
                countdown = 5f;
                cool = true;
            }
            else if (cool)
            {
                countdown -= Time.deltaTime;
                if(countdown <= 0f)
                {
                    cool = false;
                    currentBomb = 0;
                }
            }
            
        }
    }

    public void AddBomb(int num)
    {
        if (this.isLocalPlayer)
        {
            MaxBomb += num;
            cool = false;
            countdown = 0f;
            currentBomb = 0;
        }

    }

    [Command]
    void CmdDropBomb()
    {
        if(NetworkServer.active)
        {
            Vector3 playerPos = rb.position;
            Vector3Int cell = tilemap.WorldToCell(playerPos);
            Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);
            GameObject bomb = Instantiate(bombPrefab, cellCenterPos, Quaternion.identity) as GameObject;
            NetworkServer.Spawn(bomb);
        }
    }
}
