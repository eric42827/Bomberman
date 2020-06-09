using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public string[] names;

    void Start(){
        GameObject.Find("InputName").SetActive(false);
        names = new string [15] { "Mask", "Frog", "Pink", "Blue", "Purple", "Pumpkin", "Skeleton", "Soldier", "White ghost", "Alien", "Black ghost", "Prince", "Princess", "Blue ghost", "Evil man" };
        //GameObject.Find("InputName").GetComponent<InputField>().characterLimit = 7;
        Instantiate(spriteList, new Vector3(0, 0, 0), Quaternion.identity);
        setBtnSprite();
    }

    public void SetEnabled(bool flag)
    {
        enabled = flag;
    }
    
    public void Update(){
        //DisplayName();
    }
    
    public void OnSelectChar(int idx){
        Debug.Log("In OnSelectChar");
        Debug.Log(idx);
        Debug.Log(names.Length);

        char_id = idx;
        FindObjectOfType<CustomLobbyManager>().char_id = char_id;
        GameObject.Find("DisplayName").GetComponent<Text>().text = names[idx];
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