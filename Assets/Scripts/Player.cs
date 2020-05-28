using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


class Player : NetworkBehaviour
{
    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    public List<Sprite> sprites;
    public int numSprites = 10;
    
    void Start()
    {
        Debug.Log(spriteIdx);
        GetComponent<SpriteRenderer>().sprite = sprites[spriteIdx]; // important!
    }

    [SyncVar(hook = nameof(OnSpriteIndexChanged))]
    public int spriteIdx = -1;

    [ClientCallback]
    private void OnSpriteIndexChanged(int newIdx)
    {
        spriteIdx = newIdx;
        GetComponent<SpriteRenderer>().sprite = sprites[spriteIdx];
    }
}