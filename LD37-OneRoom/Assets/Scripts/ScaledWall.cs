using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaledWall : RoomRelativeObject {
    const float roomHeight = 2.5f;

    [Range(0, 1.0f)]
    public float left = 0;
    [Range(0, 1.0f)]
    public float right = 0;
    [Range(0, 1.0f)]
    public float bottom = 0;
    [Range(0, 1.0f)]
    public float top = 0;

    [Range(0, 1.0f)]
    public float height = .5f;

    [Range(0, 1.0f)]
    public float heightOffGround = 0;

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnValidate()
    {
        if(Application.isEditor)
            UpdateDimensions();
    }

    void UpdateDimensions()
    {
        RoomParameters roomParams = ScaledPlayspace.playSpace;

        //use defaults in the editor
        if (roomParams == null)
        {
            roomParams = RoomParameters.DefaultParameters();
        }

        ValidateValues();
        if (transform.parent != null)
        {

            float newWidth = (right - left) * roomParams.width;
            float newLength = (top - bottom) * roomParams.length;

            float newVerticleWidth = height * roomParams.roomHeight;
            if (newVerticleWidth == 0)
                newVerticleWidth = 1;

            transform.localScale = new Vector3(newWidth, newVerticleWidth, newLength);

            Vector3 newPos = roomParams.bottomLeft;
            newPos.x += (left + ((right - left) / 2)) * roomParams.width;
            newPos.z += (bottom + ((top - bottom) / 2)) * roomParams.length;
            newPos.y = (height * roomParams.roomHeight) / 2 + heightOffGround * roomParams.roomHeight;

            transform.localPosition = newPos;

        }


    }

    void ValidateValues()
    {
        if (left < 0) left = 0;
        else if (left > 1) left = 1;

        if (right < 0) right = 0;
        else if (right > 1) right = 1;

        if (bottom < 0) bottom = 0;
        else if (bottom > 1) bottom = 1;

        if (top < 0) top = 0;
        else if (top > 1) top = 1;
    }

    public override void UpdateForPlayspace()
    {
        UpdateDimensions();
    }
}
