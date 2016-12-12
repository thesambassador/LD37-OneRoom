using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRelativeObject : MonoBehaviour {
    [HideInInspector]
    public ScaledPlayspace room;

	// Use this for initialization
	void Awake () {
        if (transform.parent != null)
            room = transform.parent.GetComponent<ScaledPlayspace>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void UpdateForPlayspace()
    {

    }
}
