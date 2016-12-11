using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour {

    public ScaledPlayspace playspace;

	// Use this for initialization
	void Awake() {
        if(transform.parent != null)
            playspace = transform.parent.GetComponent<ScaledPlayspace>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
