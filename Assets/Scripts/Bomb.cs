using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Bomb : NetworkBehaviour {

	public float countdown = 10f;
	
	// Update is called once per frame
	void Update () {
		countdown -= Time.deltaTime;
		if (countdown <= 0f)
		{
			CmdDestroyBomb();
		}
	}

	[Command]
	void CmdDestroyBomb()
	{
		if(NetworkServer.active)
		{
			Debug.Log("Destroying bomb");
			FindObjectOfType<MapDestroyer>().CmdExplode(transform.position);
			Destroy(this.gameObject);
			NetworkServer.Destroy(this.gameObject);
		}
	}
}
