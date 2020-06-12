using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class CameraFollow : NetworkBehaviour
{

    public Transform playerTransform;
    public int depth = -20;
    public Camera m_OrthographicCamera;
    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + new Vector3(0, 0, depth);
        }
        else if (!isClient && isServer)
        {
            transform.position = new Vector3((float)2.64, (float)8.31, depth);
            m_OrthographicCamera.orthographic = true;
            //Set the size of the viewing volume you'd like the orthographic Camera to pick up (5)
            m_OrthographicCamera.orthographicSize = 13.0f;
        }
        else if (playerTransform == null)
        {
            transform.position = new Vector3((float)2.64, (float)8.31, depth);
            m_OrthographicCamera.orthographic = true;
            //Set the size of the viewing volume you'd like the orthographic Camera to pick up (5)
            m_OrthographicCamera.orthographicSize = 13.0f;
        }
    }

    public void setTarget(Transform target)
    {
        playerTransform = target;
    }
}