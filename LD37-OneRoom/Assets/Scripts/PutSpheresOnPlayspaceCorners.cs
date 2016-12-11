using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutSpheresOnPlayspaceCorners : MonoBehaviour {
    public GameObject vertexPrefab;
    public SteamVR_PlayArea playArea;
    public ScaledPlayspace scaledPlayspace;

    public bool initialized = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (initialized == false)
        {
            if (ScaledPlayspace.playSpace != null)
            {
                ScaledPlayspace spc = GetComponent<ScaledPlayspace>();

                Color[] colors = new Color[10];
                colors[0] = Color.red;
                colors[1] = Color.blue;
                colors[2] = Color.green;
                colors[3] = Color.yellow;
                colors[4] = Color.white;
                colors[5] = Color.black;

                GameObject vertex = Instantiate(vertexPrefab, this.transform);
                vertex.transform.localPosition = ScaledPlayspace.playSpace.leftEdge.center;
                vertex.GetComponent<MeshRenderer>().material.color = colors[0];

                vertex = Instantiate(vertexPrefab, this.transform);
                vertex.transform.localPosition = ScaledPlayspace.playSpace.topEdge.center;
                vertex.GetComponent<MeshRenderer>().material.color = colors[1];

                vertex = Instantiate(vertexPrefab, this.transform);
                vertex.transform.localPosition = ScaledPlayspace.playSpace.rightEdge.center;
                vertex.GetComponent<MeshRenderer>().material.color = colors[2];

                vertex = Instantiate(vertexPrefab, this.transform);
                vertex.transform.localPosition = ScaledPlayspace.playSpace.bottomEdge.center;
                vertex.GetComponent<MeshRenderer>().material.color = colors[3];
                initialized = true;
            }
        }
	
	}
}
