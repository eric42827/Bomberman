using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Networking;
public class ItemSpawn : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject itemPrefab;
    private float countdown;
    public Tilemap tilemap;

    void Start()
    {
        countdown = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
        {
            //若不是Server則跳出
            return;
        }
        else if (isServer && !isClient && countdown <= 0f)
        {
            countdown = 0.5f;
            ItemAdd();
        }
        else
        {
            countdown -= Time.deltaTime;
        }
       
    }

    void ItemAdd()
    {
        Vector3Int rand = new Vector3Int(Random.Range(-10, 14), Random.Range(-4, 20), 0);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(rand);
        Tile tile = tilemap.GetTile<Tile>(rand);
        if(tile== null)
        {
            GameObject item = Instantiate(itemPrefab, cellCenterPos, Quaternion.identity);
            NetworkServer.Spawn(item);
        }

    }
}
