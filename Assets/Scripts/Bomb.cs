using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Bomb : NetworkBehaviour {

	public float countdown;
	
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
	
	void DestroyBomb()
	{
		Debug.Log("Destroying bomb");
		FindObjectOfType<MapDestroyer>().RpcExplode(transform.position); // client rpc
		FindObjectOfType<MapDestroyer>().Explode(transform.position); // server destroy
		Destroy(this.gameObject);
		NetworkServer.Destroy(this.gameObject);
	}
}
