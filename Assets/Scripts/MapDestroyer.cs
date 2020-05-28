using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Networking;


public class MapDestroyer : NetworkBehaviour {

	[SyncVar]
	float destroyX;
	[SyncVar]
	float destroyY;
	[SyncVar]
	bool globalFlag = false;
	
	bool localFlag = false;

	public Tilemap tilemap;

	public Tile wallTile;
	public Tile destructibleTile;
	public List<Vector3Int> prevBombLocations; // 

	public GameObject explosionPrefab;

	void Update()
	{
		if(globalFlag != localFlag)
		{
			Vector3 originCell = new Vector3(destroyX, destroyY, 0);
			ExplodeCell(originCell);
			if(ExplodeCell(originCell + new Vector3(1, 0, 0)))
			{
				ExplodeCell(originCell + new Vector3(2, 0, 0));
			}
			if(ExplodeCell(originCell + new Vector3(0, 1, 0))) 
			{
				ExplodeCell(originCell + new Vector3(0, 2, 0));
			}
			if(ExplodeCell(originCell + new Vector3(-1, 0, 0))) 
			{
				ExplodeCell(originCell + new Vector3(-2, 0, 0));
			}
			if(ExplodeCell(originCell + new Vector3(0, -1, 0))) 
			{
				ExplodeCell(originCell + new Vector3(0, -2, 0));
			}
			localFlag = globalFlag;
		}
		
	}

	[Command]
	public void CmdExplode(Vector2 worldPos)
	{
		if(NetworkServer.active)
		{
			Debug.Log("Inside CmdExplode");
			Vector3 originCell = tilemap.WorldToCell(worldPos);

			if(isServer)
			{
				destroyX = originCell.x;
				destroyY = originCell.y;
				globalFlag = !globalFlag;
			}

			prevBombLocations.Add(new Vector3Int((int)destroyX, (int)destroyY, 0));

			CmdSpawnExplosion(originCell);
			CmdSpawnExplosion(originCell + new Vector3(1, 0, 0));
			CmdSpawnExplosion(originCell + new Vector3(2, 0, 0));
			CmdSpawnExplosion(originCell + new Vector3(0, 1, 0));
			CmdSpawnExplosion(originCell + new Vector3(0, 2, 0));
			CmdSpawnExplosion(originCell + new Vector3(-1, 0, 0));
			CmdSpawnExplosion(originCell + new Vector3(-2, 0, 0));
			CmdSpawnExplosion(originCell + new Vector3(0, -1, 0));
			CmdSpawnExplosion(originCell + new Vector3(0, -2, 0));
		}
	}
	
	bool ExplodeCell(Vector3 cell)
	{
		Vector3Int floor_cell = Vector3Int.FloorToInt(cell);
		Tile tile = tilemap.GetTile<Tile>(floor_cell);

		if (tile == wallTile)
		{
			return false;
		}

		if (tile == destructibleTile)
		{
			tilemap.SetTile(floor_cell, null);
		}
		return true;
	}

	[Command]
	void CmdSpawnExplosion (Vector3 cell)
	{
		Vector3Int floor_cell = Vector3Int.FloorToInt(cell);
		Vector3 pos = tilemap.GetCellCenterWorld(floor_cell);
		Tile tile = tilemap.GetTile<Tile>(floor_cell);

		GameObject explosion = Instantiate(explosionPrefab, pos, Quaternion.identity) as GameObject;
		NetworkServer.Spawn(explosion);
		return;
	}

}
