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

    void Update()
    {
        var map = FindObjectOfType<MapDestroyer>();
        if(isServer && !isClient)
        {
            enabled = false;
        }
        if(this.isLocalPlayer)
        {
            if(map.serverSetup)
            {
                Debug.Log(map.tilePositions.Count);
                Debug.Log(map.emptyPositions.Count);

                int idx = UnityEngine.Random.Range(0, map.emptyPositions.Count);
                int x = (int)(map.emptyPositions[idx] / map.MAP_SIZE) + map.ANCHOR_X;
                int y = map.emptyPositions[idx] % map.MAP_SIZE + map.ANCHOR_Y;
                int z = (int)transform.position.z;
                Vector3Int cell = new Vector3Int(x, y, z);
                Vector3 cellCenterPos = map.tilemap.GetCellCenterWorld(cell);
                transform.position = cellCenterPos;
                enabled = false;
            }
        }
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