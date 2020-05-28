using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    public Tilemap tilemap;
    private int numSprites = 10;
    private int chosenIdx;

    public class NetworkMessage: MessageBase
    {
        public int chosenIdx;
    }

    public void clearTilemapCells()
    {
        List<Vector3Int> prevBombLocations = FindObjectOfType<MapDestroyer>().prevBombLocations;
        
        Debug.Log(string.Format("bombs: {0}", prevBombLocations.Count));

        foreach(Vector3Int pos in prevBombLocations)
        {
            tilemap.SetTile(pos, null);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage message = new NetworkMessage();
        message.chosenIdx = chosenIdx;
        ClientScene.AddPlayer(conn, 0, message);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int idx = message.chosenIdx;

        GameObject player;
        Transform startPos = GetStartPosition(); // TODO: change spawn position.
        if (startPos != null)
        {
            player = (GameObject)Instantiate(playerPrefab, startPos.position, startPos.rotation);
        }
        else
        {
            player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }

        idx = Random.Range(0, numSprites - 1); // TODO: choose player
        //int idx = chosenIdx;
        Debug.Log(idx);
        player.GetComponent<Player>().spriteIdx = idx;
        clearTilemapCells(); // clear tilemap cells at previous bomb locations
        player.GetComponent<DropBomb>().tilemap = tilemap;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}