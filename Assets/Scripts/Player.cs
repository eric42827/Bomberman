using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


class Player : NetworkBehaviour
{
    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    public List<Sprite> sprites;
    public int numSprites = 4;
    
    

    [SyncVar(hook = nameof(OnSpriteIndexChanged))]
    public int spriteIdx = -1;

    [ClientCallback]
    private void OnSpriteIndexChanged(int newIdx)
    {
        spriteIdx = newIdx;
        GetComponent<SpriteRenderer>().sprite = sprites[spriteIdx];
    }
}