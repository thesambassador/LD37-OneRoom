using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour {

    public ScaledPlayspace[,] gridOfPlayspaces;

    public ScaledPlayspace wallPrefab;

    public ScaledPlayspace startingSpace;
    public ScaledPlayspace activeRoom;
    public ScaledPlayspace calibrationSpace;

    public float xSpacing = 4;
    public float zSpacing = 3;

    public int numX = -1;
    public int numZ = -1;

    public SteamVR_PlayArea playArea;

    public PlayerTeleporter player;

	// Use this for initialization
	void Start () {
        print("level initializing");
        PopulateGrid();
        SnapSpacesTogether();

        playArea = PlayerRig.playArea;
        player = PlayerRig.playerTeleporter;
        PlayerRig.currentLevel = this;
        PlayerRig.playerTeleporter.currentLevel = this;

        print("player selected is " + player.gameObject.name);

        PutPlayerInCorrectRoom();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PopulateGrid()
    {
        ScaledPlayspace[] allGridSquares = GetComponentsInChildren<ScaledPlayspace>();
        float largestX = 0;
        float largestZ = 0;

        foreach (ScaledPlayspace space in allGridSquares) 
        {
            if (space.transform.position.x > largestX)
                largestX = space.transform.position.x;
            if (space.transform.position.z > largestZ)
                largestZ = space.transform.position.z;
        }

        numX = (Mathf.RoundToInt(largestX) / (int)xSpacing) + 3;
        numZ = (Mathf.RoundToInt(largestZ) / (int)zSpacing) + 3;

        print("num x : " + numX + ", num z : " + numZ);

        gridOfPlayspaces = new ScaledPlayspace[numX, numZ];

        foreach (ScaledPlayspace space in allGridSquares)
        {
            int x = Mathf.RoundToInt(space.transform.position.x) / (int)xSpacing + 1;
            int z = Mathf.RoundToInt(space.transform.position.z) / (int)zSpacing + 1;

            gridOfPlayspaces[x, z] = space;
            space.gridX = x;
            space.gridZ = z;
            space.levelGridRef = this;
            if (space.startingLocation != null)
            {
                startingSpace = space;
            }

        }

        for (int x = 0; x < numX; x++)
        {
            for (int z = 0; z < numZ; z++)
            {
                if (gridOfPlayspaces[x, z] == null)
                {
                    ScaledPlayspace space = Instantiate(wallPrefab);
                    space.transform.parent = this.transform;
                    gridOfPlayspaces[x, z] = space;
                    space.levelGridRef = this;
                    space.gridX = x;
                    space.gridZ = z;
                }
            }
        }

    }

    void SnapSpacesTogether()
    {
        for (int x = 0; x < numX; x++)
        {
            for (int z = 0; z < numZ; z++)
            {
                ScaledPlayspace space = gridOfPlayspaces[x, z];

                space.UpdateRelativePositionScale();
                space.SetCollisionBoxActive(false);

                if (space != null)
                {
                    Vector3 newPos = new Vector3();
                    newPos.x = ScaledPlayspace.playSpace.width * x;
                    newPos.y = 0;
                    newPos.z = ScaledPlayspace.playSpace.length * z;
                    space.transform.position = newPos;
                }
            }
        }
        calibrationSpace.UpdateRelativePositionScale();
    }

    void PutPlayerInCorrectRoom()
    {
        Vector2 playerPos = new Vector2(player.playerHead.localPosition.x, player.playerHead.localPosition.z);
        Vector2 startingPos = new Vector2(startingSpace.startingLocation.transform.localPosition.x, startingSpace.startingLocation.transform.localPosition.z);

        if (startingSpace != null && Vector2.Distance(playerPos, startingPos) <= startingSpace.startingLocation.radius)
        {
            player.SetRoom(startingSpace);
            SetRoomActive(startingSpace);
        }
        else
        {
            
            player.SetRoom(calibrationSpace);
           
        }
        
    }


    public void PutPlayerInStartingRoom()
    {
        player.SetRoom(startingSpace);
    }

    public void SetRoomActive(ScaledPlayspace room)
    {
        print(room.gridX + ", " + room.gridZ);
        //first, turn all rooms to the original space's collision boxes off
        if (activeRoom != null && activeRoom.gridX != -1)
        {
            int ax = activeRoom.gridX;
            int az = activeRoom.gridZ;

            if (ax - 1 >= 0)
                gridOfPlayspaces[ax-1, az].SetCollisionBoxActive(false);
            if (ax + 1 < gridOfPlayspaces.GetLength(0))
                gridOfPlayspaces[ax + 1, az].SetCollisionBoxActive(false);
            if (az - 1 >= 0)
                gridOfPlayspaces[ax, az - 1].SetCollisionBoxActive(false);
            if (az + 1 < gridOfPlayspaces.GetLength(1))
                gridOfPlayspaces[ax, az + 1].SetCollisionBoxActive(false);
        }
        activeRoom = room;
        room.SetRoomActive();

        int x = activeRoom.gridX;
        int z = activeRoom.gridZ;
        if (x != -1)
        {
            if (x - 1 >= 0)
                gridOfPlayspaces[x - 1, z].SetCollisionBoxActive(true);
            if (x + 1 < gridOfPlayspaces.GetLength(0))
                gridOfPlayspaces[x + 1, z].SetCollisionBoxActive(true);
            if (z - 1 >= 0)
                gridOfPlayspaces[x, z - 1].SetCollisionBoxActive(true);
            if (z + 1 < gridOfPlayspaces.GetLength(1))
                gridOfPlayspaces[x, z + 1].SetCollisionBoxActive(true);
        }
    }

}
