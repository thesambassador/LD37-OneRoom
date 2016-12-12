using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativePositionedSquare : RoomRelativeObject {

    public bool snapToLeft = true;
    public bool snapToBottom = true;

    public float squareSize = .5f;
    [Range(0, 1.0f)]
    public float horizontalDist = 0;
    [Range(0, 1.0f)]
    public float verticleDist = 0;
    public float height = .01f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnValidate()
    {
        UpdateForPlayspace();
    }

    public override void UpdateForPlayspace()
    {
        RoomParameters roomParams = ScaledPlayspace.playSpace;
        if (roomParams == null)
            roomParams = RoomParameters.DefaultParameters();

        Vector3 newPosition = new Vector3(0, height, 0);
        float halfSize = squareSize * .5f;

        if (snapToLeft)
        {
            newPosition.x = roomParams.bottomLeft.x + halfSize + horizontalDist * roomParams.width;
        }
        else
        {
            newPosition.x = roomParams.bottomRight.x - halfSize - horizontalDist * roomParams.width;
        }

        if (snapToBottom)
        {
            newPosition.z = roomParams.bottomLeft.z + halfSize + verticleDist * roomParams.length;
        }
        else
        {
            newPosition.z = roomParams.topLeft.z - halfSize - verticleDist * roomParams.length;
        }


        transform.localPosition = newPosition;
    }

    

}
