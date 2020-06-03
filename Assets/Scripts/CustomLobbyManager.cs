using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Networking;

public class CustomLobbyManager : NetworkLobbyManager
{
    private int numSprites = 10;
    /*
    public Tilemap tilemap;
    public int numPlayers = 0;
    private int numSprites = 10;


    //public override void On
    public override void OnClientConnect(NetworkConnection conn)
    {
        ClientScene.AddPlayer(conn, 0);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
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

        int idx = Random.Range(0, numSprites - 1); // TODO: choose player
        //int idx = chosenIdx;
        Debug.Log(idx);
        player.GetComponent<Player>().spriteIdx = idx;
        // ADDED
        // clearTilemapCells(); // clear tilemap cells at previous bomb locations
        player.GetComponent<DropBomb>().tilemap = FindObjectOfType<MapDestroyer>().tilemap;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        numPlayers += 1;
        Debug.Log(string.Format("Num players: {0}", numPlayers));
    }
    */
    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        Debug.Log("lobby to player");
        Debug.Log(gamePlayer.GetComponent<Player>());
        //int idx = Random.Range(0, numSprites - 1); 
        gamePlayer.GetComponent<Player>().char_id = lobbyPlayer.GetComponent<LobbyPlayer>().char_id;
        gamePlayer.GetComponent<Player>().name = lobbyPlayer.GetComponent<LobbyPlayer>().name;
        return true;
    }
}