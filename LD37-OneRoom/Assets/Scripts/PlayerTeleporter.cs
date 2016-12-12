using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour {

    public LevelGrid currentLevel;

    public SteamVR_TrackedController leftController;
    public SteamVR_TrackedController rightController;
    public Transform playerHead;
    public FadeViewWhenColliding currentCollisionFader;

    public TargetRoomCollisionChecker collisionCheck;

    public LayerMask teleportRaycastMask;

    public ScaledPlayspace currentRoom;
    public ScaledPlayspace selectedRoom;

    bool leftPressed = false;
    bool rightPressed = false;

    public float dashTime = .1f;
    private bool _dashing = false;
    private float _dashTimer = -1f;
    private Vector3 targetLocation;
    private Vector3 originalLocation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        HandleTeleportationInput();
        HandleDash();
	}

    void HandleTeleportationInput()
    {
        if (!_dashing && rightController.padTouched)
        {
            ScaledPlayspace room = GetRoomControllerIsPointingAt();
            if (room != null)
            {
                HighlightSelectedRoom(room);


                if (rightController.padPressed && rightPressed == false)
                {
                    if (!collisionCheck.isColliding && !currentCollisionFader.isFaded)
                    {
                        SetRoom(room, false);
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

        if (selectedRoom == null)
            collisionCheck.gameObject.SetActive(false);

        if (rightController.padPressed == false)
        {
            rightPressed = false;
        }
    }

    void HandleDash()
    {
        if (_dashing)
        {
            _dashTimer += Time.deltaTime;

            float dashAmount = _dashTimer / dashTime;
            transform.position = Vector3.Lerp(originalLocation, targetLocation, dashAmount);

            if (dashAmount > 1)
                _dashing = false;

        }
    }

    ScaledPlayspace GetRoomControllerIsPointingAt()
    {
        RaycastHit[] hits = Physics.RaycastAll(rightController.transform.position, rightController.transform.forward, 50, teleportRaycastMask, QueryTriggerInteraction.Collide);
        if (hits.Length > 0)
        {
            RaycastHit hit = hits[0];
            ScaledPlayspace room = hit.transform.GetComponent<ScaledPlayspace>();
            return room;
        }
        return null;
    }

    void HighlightSelectedRoom(ScaledPlayspace newRoom)
    {
        //unhighlight whatever was the last selected room
        if (selectedRoom != null && selectedRoom != newRoom)
        {
            selectedRoom.UnHighlight();
        }

        selectedRoom = newRoom;
        newRoom.Highlight();
        collisionCheck.SetRoom(newRoom);
        collisionCheck.gameObject.SetActive(true);
    }

    public void SetRoom(ScaledPlayspace room, bool instant = true)
    {
        
        if(currentRoom != null)
            currentRoom.playerInSpace = false;

        if (instant)
        {
            transform.position = room.transform.position;
        }
        else
        {
            print("dashing");
            originalLocation = currentRoom.transform.position;
            targetLocation = room.transform.position;
            _dashTimer = 0;
            _dashing = true;
        }

        currentLevel.SetRoomActive(room);
        currentRoom = room;
    }
}
