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
    public string name;
    public GameObject spriteList;

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
        FindObjectOfType<CustomLobbyManager>().char_id = char_id;
        GameObject.Find("ChosenChar").GetComponent<Image>().sprite = spriteList.GetComponent<SpriteList>().sprites[char_id];
    }
    public void OnRegister(){
        Debug.Log("In OnRegister");
        // StaticClass.setPlayerInfo(usr_name, char_id);
    }
    public void DisplayName(){
        name = GameObject.Find("InputName").GetComponent<InputField>().text;
        GameObject.Find("DisplayName").GetComponent<Text>().text = name;
        FindObjectOfType<CustomLobbyManager>().name = name;
    }
    public void setBtnSprite(){
        for (int i = 0 ; i < 15 ; ++i){
            string btn = "Button"+i;
            GameObject.Find(btn).GetComponent<Image>().sprite = spriteList.GetComponent<SpriteList>().sprites[i];
        }
        GameObject.Find("ChosenChar").GetComponent<Image>().sprite = spriteList.GetComponent<SpriteList>().sprites[0];
    }
}