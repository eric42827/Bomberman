using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Networking;
public class MapRandom : NetworkBehaviour
{
    // Start is called before the first frame update
    int[,] map = new int[24, 24];
    
    public Tilemap tilemap;

    public Tile wallTile;
    public Tile destructibleTile;
    void Start()
    {
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                int rand = Random.Range(0, 1);
                if (rand == 1)
                {
                    tilemap.SetTile(new Vector3Int(-10+x, -4+y, 0), destructibleTile);
                }
                
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
