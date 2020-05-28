using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Tilemaps;

public class CustomNetworkManager : NetworkManager
{
    public Tilemap tilemap;
    public int numSprites = 4;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player;
        Transform startPos = GetStartPosition();
        if (startPos != null)
        {
            player = (GameObject)Instantiate(playerPrefab, startPos.position, startPos.rotation);
        }
        else
        {
            player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
        int idx = Random.Range(0, numSprites - 1);
        Debug.Log(idx);
        player.GetComponent<Player>().spriteIdx = idx;
        player.GetComponent<DropBomb>().tilemap = tilemap;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}