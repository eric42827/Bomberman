using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Networking;

public class CustomLobbyManager : NetworkLobbyManager
{
    private int numSprites = 15;
    //private int m_MaxPlayers = 20; // force
    public int char_id;
    public string name;
    public GameObject winnerPrefab;
    public Dictionary<NetworkConnection, GameObject> lobbyPlayers = new Dictionary<NetworkConnection, GameObject>();
    public Dictionary<NetworkConnection, NetworkMessage> playerInfo = new Dictionary<NetworkConnection, NetworkMessage>();
    public Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    private const short PLAYER_INFO = 1;
    public int numPlayers = 0;

    public class NetworkMessage: MessageBase
    {
        public int char_id;
        public string name;
        //public NetworkConnection conn;
    }

    /*
    void Start()
    {
        if (NetworkServer.active)
        {
            //registering the server handler
            NetworkServer.RegisterHandler(PLAYER_INFO, OnServerMsg);
        }
    }
    

    private void OnServerMsg(NetworkMessage message)
    {
        char_id = message.char_id;
        playerInfo[message.conn] = message;
    }
    */

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
        lobbyPlayer.GetComponent<LobbyPlayer>().name = playerInfo[conn].name;
        return lobbyPlayer;
    }

    public override void OnLobbyClientConnect(NetworkConnection conn)
    {
        for (int i = 0 ; i < numSprites ; ++i){
            string btn = "Button"+i;
            GameObject.Find(btn).SetActive(false);
        }
        //GameObject.Find("InputName").SetActive(false);
        GameObject.Find("CharSceneHandler").GetComponent<CharSceneHandler>().SetEnabled(false);

        NetworkMessage message = new NetworkMessage();
        //base.OnLobbyClientConnect(conn);
        message.char_id = char_id;
        message.name = name;
        //message.conn = conn;
        //NetworkManager.singleton.client.Send(PLAYER_INFO, message);
        ClientScene.AddPlayer(conn, 0, message);
    }

    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        Debug.Log("lobby to player");
        Debug.Log(lobbyPlayer.GetComponent<LobbyPlayer>());
       
        gamePlayer.GetComponent<Player>().char_id = lobbyPlayer.GetComponent<LobbyPlayer>().char_id;
        gamePlayer.GetComponent<Player>().name = lobbyPlayer.GetComponent<LobbyPlayer>().name;
        string uuid = System.Guid.NewGuid().ToString();
        gamePlayer.GetComponent<Player>().uuid = uuid;
        players[uuid] = gamePlayer;
        return true;
    }

    public void removePlayer(GameObject gamePlayer)
    {
        players.Remove(gamePlayer.GetComponent<Player>().uuid);
        if(players.Count == 1)
        {
            var player = players.FirstOrDefault().Value;
            GameObject winner = Instantiate(winnerPrefab, new Vector3(0,0,0), Quaternion.identity);
            winner.GetComponent<WinnerInfo>().char_id = player.GetComponent<Player>().char_id;
            winner.GetComponent<WinnerInfo>().name = player.GetComponent<Player>().name;
            NetworkServer.Spawn(winner);
            players.Clear();
            this.ServerChangeScene("EndScene");
        }
    }
}