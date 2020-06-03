using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


class Player : NetworkBehaviour
{
    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    public GameObject spriteList;
    public string name;
    [SyncVar(hook = nameof(OnSpriteIndexChanged))]
    public int char_id = -1;
    public string uuid;
    
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = spriteList.GetComponent<SpriteList>().sprites[char_id]; // important!
        GetComponent<DropBomb>().tilemap = FindObjectOfType<MapDestroyer>().tilemap;
    }

    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponent<CameraFollow>().setTarget(gameObject.transform);
        Debug.Log("Start local player");
        HealthBar healthBar = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<HealthBar>();
        GetComponent<PlayerDamage>().healthBar = healthBar;
        //GetComponent<DropBomb>().tilemap = FindObjectOfType<MapDestroyer>().tilemap;
    }
    

    [ClientCallback]
    private void OnSpriteIndexChanged(int newIdx)
    {
        char_id = newIdx;
        GetComponent<SpriteRenderer>().sprite = spriteList.GetComponent<SpriteList>().sprites[char_id];
    }
}