using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Networking;

public class CustomLobbyManager : NetworkLobbyManager
{
    private int numSprites = 15;
    public int char_id;
    public string name;
    public Dictionary<NetworkConnection, GameObject> lobbyPlayers = new Dictionary<NetworkConnection, GameObject>();
    public Dictionary<NetworkConnection, NetworkMessage> playerInfo = new Dictionary<NetworkConnection, NetworkMessage>();

    public class NetworkMessage: MessageBase
    {
        public int char_id;
        public string name;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        Debug.Log(message.char_id);
        Debug.Log(conn);
        playerInfo[conn] = message;
        //lobbyPlayers[conn].GetComponent<LobbyPlayer>().char_id = message.char_id;
    } 

    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject lobbyPlayer = (GameObject) Object.Instantiate((Object) this.lobbyPlayerPrefab.gameObject, Vector3.zero, Quaternion.identity);
        Debug.Log(lobbyPlayer);
        lobbyPlayer.GetComponent<LobbyPlayer>().char_id = playerInfo[conn].char_id;
        return lobbyPlayer;
    }

    public override void OnLobbyClientConnect(NetworkConnection conn)
    {
        for (int i = 0 ; i < numSprites ; ++i){
            string btn = "Button"+i;
            GameObject.Find(btn).SetActive(false);
        }
        GameObject.Find("InputName").SetActive(false);
        GameObject.Find("CharSceneHandler").GetComponent<CharSceneHandler>().SetEnabled(false);

        NetworkMessage message = new NetworkMessage();
        //base.OnLobbyClientConnect(conn);
        message.char_id = char_id;
        message.name = name;
        ClientScene.AddPlayer(conn, 0, message);
    }

    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        Debug.Log("lobby to player");
        Debug.Log(lobbyPlayer.GetComponent<LobbyPlayer>());
       
        gamePlayer.GetComponent<Player>().char_id = lobbyPlayer.GetComponent<LobbyPlayer>().char_id;
        gamePlayer.GetComponent<Player>().name = lobbyPlayer.GetComponent<LobbyPlayer>().name;
        return true;
    }
}