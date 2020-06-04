using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Networking;
using System;

public class MapDestroyer : NetworkBehaviour {

	public Tilemap tilemap;

	public Tile wallTile;
	public Tile destructibleTile;

	public GameObject explosionPrefab;
	bool flag;
	public SyncListInt map = new SyncListInt();
	[SyncVar]
	bool serversetup;
	//int[,] map = new int[24, 24];
	void Awake()
	{
		Debug.Log("Map destroyer awake");
		if (!isClient && isServer)
        {
			serversetup = false;
		}

	}
	void Start()
	{
		flag = false;
		if (!isClient && isServer)
		{
			for (int x = 0; x < 24; x++)
			{
				for (int y = 0; y < 24; y++)
				{
					int rand = UnityEngine.Random.Range(0, 2);
					if (rand == 1)
					{
						Debug.Log("1");
						tilemap.SetTile(new Vector3Int(-10 + x, -4 + y, 0), destructibleTile);
						map.Add(1);
                        //RpcBuildMap(x, y);
                    }
                    else
                    {
						map.Add(0);
                    }
				}
			}
			serversetup = true;
		}

	}
	void Update()
    {
		if (isClient && !flag)
		{
			for (int x = 0; x < 24; x++)
			{
				for (int y = 0; y < 24; y++)
				{
					if (map[24 * x + y] == 1)
					{
						tilemap.SetTile(new Vector3Int(-10 + x, -4 + y, 0), destructibleTile);
					}
				}
			}
			flag = true;
		}
	}

	/*[Command]
	public void CmdBuildMap(int x, int y)
	{
		if(map[25*x + y] == 1)
        {
			Debug.Log("666");
			RpcBuildMap(x, y);
		}
			
	}

	[ClientRpc]
	public void RpcBuildMap(int x, int y)
    {
		Debug.Log("1212");
		//map[x, y] = 1;
		tilemap.SetTile(new Vector3Int(-10 + x, -4 + y, 0), destructibleTile);
	}*/

	[ClientRpc]
	public void RpcExplode(Vector3 cell)
	{
		ExplodeCell(cell);
		ExplodeCell(cell + new Vector3(1, 0, 0));
		ExplodeCell(cell + new Vector3(2, 0, 0));
		ExplodeCell(cell + new Vector3(0, 1, 0));
		ExplodeCell(cell + new Vector3(0, 2, 0));
		ExplodeCell(cell + new Vector3(-1, 0, 0)); 
		ExplodeCell(cell + new Vector3(-2, 0, 0));
		ExplodeCell(cell + new Vector3(0, -1, 0));
		ExplodeCell(cell + new Vector3(0, -2, 0));
	}

	public void Explode(Vector3 cell)
	{
		ExplodeCell(cell);
		ExplodeCell(cell + new Vector3(1, 0, 0));
		ExplodeCell(cell + new Vector3(2, 0, 0));
		ExplodeCell(cell + new Vector3(0, 1, 0));
		ExplodeCell(cell + new Vector3(0, 2, 0));
		ExplodeCell(cell + new Vector3(-1, 0, 0)); 
		ExplodeCell(cell + new Vector3(-2, 0, 0));
		ExplodeCell(cell + new Vector3(0, -1, 0));
		ExplodeCell(cell + new Vector3(0, -2, 0));
	}


	void ExplodeCell(Vector3 cell)
	{
		Vector3Int floor_cell = Vector3Int.FloorToInt(cell);
		Vector3 pos = tilemap.GetCellCenterWorld(floor_cell);
		Tile tile = tilemap.GetTile<Tile>(floor_cell);

		if (tile == wallTile)
		{
			return;
		}

		Instantiate(explosionPrefab, pos, Quaternion.identity);
		if (tile == destructibleTile)
		{
			Debug.Log("tt:" + floor_cell);
			tilemap.SetTile(floor_cell, null);
			// ADDED
		}
		return;
	}

}
