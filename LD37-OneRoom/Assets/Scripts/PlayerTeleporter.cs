using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour {

    public LevelGrid grid;

    public SteamVR_TrackedController leftController;
    public SteamVR_TrackedController rightController;
    public Transform playerHead;

    public Transform headProjectionLeft;
    public Transform headProjectionRight;

    public LayerMask teleportRaycastMask;

    public ScaledPlayspace currentRoom;
    public ScaledPlayspace selectedRoom;

    bool leftPressed = false;
    bool rightPressed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (rightController.padTouched)
        {
            RaycastHit[] hits = Physics.RaycastAll(rightController.transform.position, rightController.transform.forward, 50, teleportRaycastMask, QueryTriggerInteraction.Collide);
            if (hits.Length > 0)
            {
                RaycastHit hit = hits[0];
                ScaledPlayspace room = hit.transform.GetComponent<ScaledPlayspace>();

                if (room != null)
                {
                    if (selectedRoom != null && selectedRoom != room)
                    {
                        selectedRoom.UnHighlight();
                    }
                    selectedRoom = room;
                    room.Highlight();

                    if (rightController.padPressed && rightPressed == false)
                    {
                        SetRoom(room);
                        rightPressed = true;
                    }
                }
            }
            else
            {
                if (selectedRoom != null)
                {
                    selectedRoom.UnHighlight();
                    selectedRoom = null;
                }
            }
        }
        else
        {
            if (selectedRoom != null)
            {
                selectedRoom.UnHighlight();
                selectedRoom = null;
            }
        }

        if (rightController.padPressed == false)
        {
            rightPressed = false;
        }
        
	}

    public void SetRoom(ScaledPlayspace room)
    {
        if(currentRoom != null)
            currentRoom.playerInSpace = false;

        transform.position = room.transform.position;
        room.playerInSpace = true;
        currentRoom = room;
    }
}
