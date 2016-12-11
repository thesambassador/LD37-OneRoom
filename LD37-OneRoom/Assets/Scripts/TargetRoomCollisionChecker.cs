using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRoomCollisionChecker : MonoBehaviour {

    public Transform playerCamera;
    public ScaledPlayspace targetRoom;

    public bool isColliding = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (targetRoom != null)
        {
            Vector3 playerLocalPos = playerCamera.localPosition;
            transform.position = targetRoom.transform.position + playerLocalPos;
        }
	}

    public void OnCollisionStay(Collision collision)
    {
        isColliding = true;
    }



    
}
