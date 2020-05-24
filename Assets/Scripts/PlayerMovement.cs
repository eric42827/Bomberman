using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Tilemap tilemap;
	public GameObject bombPrefab;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown("space"))
		{
			Vector3 playerPos = rb.position;
			Vector3Int cell = tilemap.WorldToCell(playerPos);
			Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

			Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
		}
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
