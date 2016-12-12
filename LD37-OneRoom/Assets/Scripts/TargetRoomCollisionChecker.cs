using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRoomCollisionChecker : MonoBehaviour {

    public Transform playerCamera;
    public ScaledPlayspace targetRoom;

    Color teleportColor = Color.green;
    Color noTeleportColor = Color.red;

    public bool isColliding = false;

    public Renderer renderer;

    public int collisionCount = 0;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
	}

    void OnEnable()
    {
        collisionCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (targetRoom != null)
        {
            Vector3 playerLocalPos = playerCamera.localPosition;
            transform.position = targetRoom.transform.position + playerLocalPos;
        }

        if (collisionCount > 0)
            isColliding = true;
        else
            isColliding = false;

        if (isColliding)
        {
            renderer.material.color = noTeleportColor;
        }
        else
        {
            renderer.material.color = teleportColor;
        }
	}

    public void SetRoom(ScaledPlayspace room){
        targetRoom = room;
        Vector3 playerLocalPos = playerCamera.localPosition;
        transform.position = targetRoom.transform.position + playerLocalPos;
    }


    public void OnTriggerExit(Collider other)
    {
        collisionCount--;
    }

    public void OnTriggerEnter(Collider other)
    {
        collisionCount++;
    }



    
}
