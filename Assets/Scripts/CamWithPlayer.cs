using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamWithPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    // Update is called once per frame
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void LateUpdate()
    {
        Vector3 temp;
        if (playerTransform.position == null)
        {
            transform.position = new Vector3((float)0.0, (float)0.0, (float)0.0);
        }else
        {
            //Debug.Log(playerTransform.position);
            temp = playerTransform.position;
            temp.z = playerTransform.position.z - (float)10.0;
            transform.position = temp;
        }

    }
}
