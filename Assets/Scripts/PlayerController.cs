using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask stopMovementLayer;
    public Tilemap tilemap;
	public GameObject bombPrefab;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, movePoint.position) <= .05f) 
        {
            if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                if(!Physics2D.OverlapCircle(movePoint.position + dir, .2f, stopMovementLayer))
                {
                    movePoint.position += dir;
                }
            }
            else if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                Vector3 dir = new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                if(!Physics2D.OverlapCircle(movePoint.position + dir, .2f, stopMovementLayer))
                {
                    movePoint.position += dir;
                }
            }
        }
        if (Input.GetKeyDown("space"))
		{
			Vector3 playerPos = transform.position;
			Vector3Int cell = tilemap.WorldToCell(playerPos);
			Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

			Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
		}
    }
}
