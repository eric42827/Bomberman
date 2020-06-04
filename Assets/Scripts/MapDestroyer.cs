﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Networking;


public class MapDestroyer : NetworkBehaviour {

	public Tilemap tilemap;

	public Tile wallTile;
	public Tile destructibleTile;

	public GameObject explosionPrefab;


	void Awake()
	{
		Debug.Log("Map destroyer awake");
	}
	
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
			tilemap.SetTile(floor_cell, null);
			// ADDED
		}
		return;
	}

}
