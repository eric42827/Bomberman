﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneHandler : MonoBehaviour
{
    public GameObject spriteList;
    public string name;
    public int char_id;
    
    // Start is called before the first frame update
    void Start(){
        Debug.Log("@ Start");
        Instantiate(spriteList, new Vector3(0,0,0), Quaternion.identity);
        displayWinner();
    }

    // Update is called once per frame
    void Update(){
        
    }
    void displayWinner(){
        Debug.Log("@ DisplayWinner");
        char_id = FindObjectOfType<WinnerInfo>().char_id;
        name = FindObjectOfType<WinnerInfo>().name;
        Debug.Log("Name: "+name);
        GameObject.Find("WinnerName").GetComponent<Text>().text = name;
        GameObject.Find("WinnerSprite").GetComponent<Image>().sprite = spriteList.GetComponent<SpriteList>().sprites[char_id];
    }
}
