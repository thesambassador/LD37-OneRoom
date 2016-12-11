using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour {

    public ScaledPlayspace[,] gridOfPlayspaces;

    public ScaledPlayspace wallPrefab;

    public ScaledPlayspace startingSpace;

    public float xSpacing = 4;
    public float zSpacing = 3;

    public int numX = -1;
    public int numZ = -1;

    public SteamVR_PlayArea playArea;

    public PlayerTeleporter player;

	// Use this for initialization
	void Start () {
        PopulateGrid();
        SnapSpacesTogether();
        if (playArea == null)
            playArea = FindObjectOfType<SteamVR_PlayArea>();

        player = FindObjectOfType<PlayerTeleporter>();

        player.SetRoom(startingSpace);
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
            if (space.containsStartingPoint)
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

                if (space == null)
                {


                }

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
    }
}
