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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && this.isLocalPlayer)
        {
            CmdDropBomb();
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
