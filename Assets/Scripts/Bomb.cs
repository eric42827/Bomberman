using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Bomb : NetworkBehaviour {

	public float countdown = 10f;
	
	// Update is called once per frame
	void Update () {
		{
			countdown -= Time.deltaTime;
			if (countdown <= 0f)
			{
				if(!isClient && isServer)
				{
					DestroyBomb();
				}
			}
		}
	}

	[Command]
	void CmdDestroyBomb()
	{
		Debug.Log("Destroying bomb");
		if(NetworkServer.active)
		{
			Debug.Log("Destroying bomb");
			FindObjectOfType<MapDestroyer>().RpcExplode(transform.position);
			Destroy(this.gameObject);
			NetworkServer.Destroy(this.gameObject);
		}
	}
	
	
	void DestroyBomb()
	{
		Debug.Log("Destroying bomb");
		FindObjectOfType<MapDestroyer>().RpcExplode(transform.position); // client rpc
		FindObjectOfType<MapDestroyer>().Explode(transform.position); // server destroy
		Destroy(this.gameObject);
		NetworkServer.Destroy(this.gameObject);
	}
}
