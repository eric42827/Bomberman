using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomSceneHandler : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public List<Sprite> sprites;
    public List<Text> players_name;
    public List<int> players_id;
    void Start(){
        if(!isServer){
            HideStartBtn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHostStartGame(){
        
    }
    private void HideStartBtn(){
        GameObject.Find("Start").SetActive(false);
    }
}
