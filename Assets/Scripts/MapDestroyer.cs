using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Networking;
using System;

public class MapDestroyer : NetworkBehaviour {

	public int MAP_SIZE = 25;
	public int ANCHOR_X = -10;
	public int ANCHOR_Y = -4;

	public Tilemap tilemap;

	public Tile wallTile;
	public Tile destructibleTile;

	public GameObject explosionPrefab;

	public SyncListInt tilePositions = new SyncListInt();
	public SyncListInt emptyPositions = new SyncListInt();

	[SyncVar]
	public bool serverSetup;

	void Awake()
	{
		Debug.Log("Map destroyer awake");
		if (!isClient && isServer)
        {
			serverSetup = false;
		}

	}

	void Start()
	{
		if (!isClient && isServer)
		{
			for (int x = 0; x < MAP_SIZE; x++)
			{
				for (int y = 0; y < MAP_SIZE; y++)
				{
					int rand = UnityEngine.Random.Range(0, 2);
					if (rand == 1)
					{
						Debug.Log("1");
						tilemap.SetTile(new Vector3Int(ANCHOR_X + x, ANCHOR_Y + y, 0), destructibleTile);
						tilePositions.Add(MAP_SIZE * x + y);
                    }
					else
					{
						emptyPositions.Add(MAP_SIZE * x + y);
					}
				}
			}
			serverSetup = true;
		}
	}

	void Update()
    {
		if(serverSetup)
		{
			if (isClient)
			{
				foreach(int pos in tilePositions)
				{
					int x = (int)(pos / MAP_SIZE);
					int y = pos % MAP_SIZE;
					tilemap.SetTile(new Vector3Int(ANCHOR_X + x, ANCHOR_Y + y, 0), destructibleTile);
				}
				enabled = false;
			}
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
