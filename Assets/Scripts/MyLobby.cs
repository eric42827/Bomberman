using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class MyLobby : NetworkLobbyManager
{
    // Start is called before the first frame update
    void Start()
    {
        MMStart();
        MMListMatches();
    }

    // Update is called once per frame
    void MMStart(){
        Debug.Log("At MMStart");
        this.StartMatchMaker();
    }

    void MMListMatches(){
        Debug.Log("At MMListMatches");
        this.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
    }
    public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList){
        Debug.Log("At OnMatchList");

        base.OnMatchList(success, extendedInfo, matchList);

        if(!success){
            Debug.Log("List failed: "+extendedInfo);
        }
        else{
            if(matchList.Count > 0){
                Debug.Log("Successfully list matched" + matchList[0]);
                MMJoinMatch(matchList[0]);
            }
            else
                MMCreateMatch();
        }
    }    
    void MMJoinMatch(MatchInfoSnapshot firstMatch){
        Debug.Log("At MMJoinMatch");

        this.matchMaker.JoinMatch(firstMatch.networkId, "", "", "", 0, 0, OnMatchJoin);
    }

    public override void OnMatchJoin(bool success, string extendedInfo, MatchInfo matchInfo){
        Debug.Log("At OnMatchJoin");

        base.OnMatchJoin(success, extendedInfo, matchInfo);

        if(!success){
            Debug.Log("Failed to join match: "+extendedInfo);
        }
        else
            Debug.Log("Successfully joined match: "+matchInfo.networkId);
    }

    void MMCreateMatch(){
        Debug.Log("At MMCreateMatch");

        this.matchMaker.CreateMatch("Bomberman", 15, true, "", "", "", 0, 0, OnMatchCreate);
    }

    public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo){
        Debug.Log("At OnMatchCreate");

        base.OnMatchCreate(success, extendedInfo, matchInfo);

        if(!success){
            Debug.Log("Failed to create match: "+extendedInfo);
        }
        else
            Debug.Log("Successfully created match: "+matchInfo.networkId);
    }
}
