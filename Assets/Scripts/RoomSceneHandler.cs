using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RoomSceneHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public List<Sprite> sprites;
    // public List<Text> players_name;
    // public List<int> players_id;
    public string usr_name;
    public int char_id;
    void Start(){
        usr_name = PlayerPrefs.GetString("username", "default");
        char_id = PlayerPrefs.GetInt("charid", 0);
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
