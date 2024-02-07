using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{

    public Camera cameraToLookAt;
    private Vector3 offset;
    public int distanceAbovePlayer;
    public GameObject player; 

    private void Start()
    {
        offset = new Vector3 (0, distanceAbovePlayer, 0);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}
