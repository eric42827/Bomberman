using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

class CharSceneHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public int char_id = 0;
    public string usr_name;
    public GameObject spriteList;
    public GameObject lobbyPlayer;

    void Start(){
        GameObject.Find("InputName").GetComponent<InputField>().characterLimit = 7;
        Instantiate(spriteList, new Vector3(0, 0, 0), Quaternion.identity);
        setBtnSprite();
    }
    public void Update(){
        DisplayName();
    }
    public void OnSelectChar(int idx){
        Debug.Log("In OnSelectChar");
        char_id = idx;
        lobbyPlayer.GetComponent<LobbyPlayer>().char_id = char_id;
        GameObject.Find("ChosenChar").GetComponent<Image>().sprite = spriteList.GetComponent<SpriteList>().sprites[char_id];
    }
    public void OnRegister(){
        Debug.Log("In OnRegister");
        // StaticClass.setPlayerInfo(usr_name, char_id);
    }
    public void DisplayName(){
        usr_name = GameObject.Find("InputName").GetComponent<InputField>().text;
        lobbyPlayer.GetComponent<LobbyPlayer>().name = usr_name;
        GameObject.Find("DisplayName").GetComponent<Text>().text = usr_name;
    }
    public void setBtnSprite(){
        for (int i = 0 ; i < 15 ; ++i){
            string btn = "Button"+i;
            GameObject.Find(btn).GetComponent<Image>().sprite = spriteList.GetComponent<SpriteList>().sprites[i];
        }
        GameObject.Find("ChosenChar").GetComponent<Image>().sprite = spriteList.GetComponent<SpriteList>().sprites[0];
    }
}