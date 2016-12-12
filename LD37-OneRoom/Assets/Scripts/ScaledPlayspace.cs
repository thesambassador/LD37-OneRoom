using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScaledPlayspace : MonoBehaviour {

    public static RoomParameters playSpace;

    public SteamVR_PlayArea playArea;
    public BoxCollider areaCollider;
    public LevelGrid levelGridRef;

    public Renderer floorRenderer;

    public int gridX = -1;
    public int gridZ = -1;

    public StartingSpace startingLocation;

    private bool _playerInSpace = false;
    public bool playerInSpace { 
        get { return _playerInSpace; }
        set
        {
            _playerInSpace = value;
        }
    }

	// Use this for initialization
	void Awake () {
        if (playSpace == null)
            InitializeEdges();
        areaCollider = GetComponent<BoxCollider>();

	}

    public void InitializeEdges()
    {
        print("initializing edges");
        if (playArea == null)
        {
            playArea = FindObjectOfType<SteamVR_PlayArea>();
        }

        playSpace = new RoomParameters();
        playSpace.playspaceEdges = new Edge[4];
        for (int i = 0; i < 4; i++)
        {
            if (i != 3)
            {
                playSpace.playspaceEdges[i] = new Edge(playArea.vertices[i], playArea.vertices[i + 1]);
            }
            else
            {
                playSpace.playspaceEdges[i] = new Edge(playArea.vertices[i], playArea.vertices[0]);
            }

            if (playArea.vertices[i].x > 0 && playArea.vertices[i].z < 0)
            {
                playSpace.bottomRight = playArea.vertices[i];
            }
            else if (playArea.vertices[i].x < 0 && playArea.vertices[i].z < 0)
            {
                playSpace.bottomLeft = playArea.vertices[i];
            }
            else if (playArea.vertices[i].x > 0 && playArea.vertices[i].z > 0)
            {
                playSpace.topRight = playArea.vertices[i];
            }
            else if (playArea.vertices[i].x < 0 && playArea.vertices[i].z > 0)
            {
                playSpace.topLeft = playArea.vertices[i];
            }
        }

        playSpace.topEdge = new Edge(playSpace.topLeft, playSpace.topRight);
        playSpace.bottomEdge = new Edge(playSpace.bottomLeft, playSpace.bottomRight);
        playSpace.leftEdge = new Edge(playSpace.bottomLeft, playSpace.topLeft);
        playSpace.rightEdge = new Edge(playSpace.bottomRight, playSpace.topRight);

        playSpace.width = Vector3.Distance(playSpace.bottomLeft, playSpace.bottomRight);
        playSpace.length = Vector3.Distance(playSpace.bottomLeft, playSpace.topLeft);

    }

    public void Highlight()
    {
        floorRenderer.material.color = Color.green;
    }

    public void UnHighlight()
    {
        floorRenderer.material.color = Color.grey;
    }

    public void UpdateRelativePositionScale()
    {
        RoomRelativeObject[] allObjectsInRoom = GetComponentsInChildren<RoomRelativeObject>();
        foreach (RoomRelativeObject obj in allObjectsInRoom)
        {
            obj.UpdateForPlayspace();
        }
        Vector3 newSize = new Vector3(ScaledPlayspace.playSpace.width, 1.25f, ScaledPlayspace.playSpace.length);

        if(areaCollider != null)
            areaCollider.size = newSize;

    }

    //when player teleports to this one
    public void SetRoomActive()
    {
        
        playerInSpace = true;
        SetCollisionBoxActive(false);
        //set only adjacent rooms coliders to be active
    }

    public void SetCollisionBoxActive(bool val)
    {
        if (gridX == 1 && gridZ == 2)
        {
            print("this one");
        }
        if(areaCollider != null)
            areaCollider.enabled = val;
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
