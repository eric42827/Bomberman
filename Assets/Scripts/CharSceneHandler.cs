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
    public List<Sprite> sprites;

    void Start(){
        GameObject.Find("InputName").GetComponent<InputField>().characterLimit = 7;
        setBtnSprite();
    }
    public void Update(){
        DisplayName();
    }
    public void OnSelectChar(int idx){
        Debug.Log("In OnSelectChar");
        char_id = idx;
        GameObject.Find("ChosenChar").GetComponent<Image>().sprite = sprites[char_id];
    }
    public void OnRegister(){
        Debug.Log("In OnRegister");
        // StaticClass.setPlayerInfo(usr_name, char_id);
        PlayerPrefs.SetString("usrname", usr_name);
        PlayerPrefs.SetInt("charid", char_id);
        SceneManager.LoadScene("GameRoom");
    }
    public void DisplayName(){
        usr_name = GameObject.Find("InputName").GetComponent<InputField>().text;
        GameObject.Find("DisplayName").GetComponent<Text>().text = usr_name;
    }
    public void setBtnSprite(){
        for (int i = 0 ; i < 15 ; ++i){
            string btn = "Button"+i;
            GameObject.Find(btn).GetComponent<Image>().sprite = sprites[i];
        }
    }
}