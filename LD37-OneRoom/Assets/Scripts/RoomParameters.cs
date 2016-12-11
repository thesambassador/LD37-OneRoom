using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomParameters {

    public float roomHeight = 2.5f;
    public Edge[] playspaceEdges;

    public SteamVR_PlayArea playArea;

    public float width = 3.5f;
    public float length = 2.5f;

    public Vector3 bottomRight = new Vector3(1.75f, 0, -1.25f);
    public Vector3 bottomLeft = new Vector3(-1.75f, 0, -1.25f);
    public Vector3 topLeft = new Vector3(-1.75f, 0, 1.25f);
    public Vector3 topRight = new Vector3(1.75f, 0, 1.25f);

    public Edge topEdge;
    public Edge bottomEdge;
    public Edge leftEdge;
    public Edge rightEdge;


    public static RoomParameters DefaultParameters()
    {
        RoomParameters result = new RoomParameters();
        return result;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
