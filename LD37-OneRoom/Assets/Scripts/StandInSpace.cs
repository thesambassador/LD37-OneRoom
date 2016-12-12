using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandInSpace : MonoBehaviour {

    float radius = .25f;
    ScaledPlayspace space;

    public float timeToStand = .3f;
    public float _timer = -1f;

    public bool isCalibrationSpace;
    public bool isEndSpace;

    public bool playerinspace;

	// Use this for initialization
	void Start () {
        space = transform.parent.GetComponent<ScaledPlayspace>();
	}
	
	// Update is called once per frame
	void Update () {

        if (IsPlayerInsideRadius())
        {
            playerinspace = true;
            if(_timer < 0)
                _timer = timeToStand;
        }
        else
        {
            playerinspace = false;
            _timer = -1;
        }

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                Activate();
            }
        }
	}

    public bool IsPlayerInsideRadius()
    {
        Vector2 playerPos = new Vector2(PlayerRig.playerTeleporter.playerHead.localPosition.x, PlayerRig.playerTeleporter.playerHead.localPosition.z);
        Vector2 startingPos = new Vector2(transform.localPosition.x, transform.localPosition.z);

        if (!PlayerRig.playerTeleporter.currentCollisionFader.isFaded && space.playerInSpace && Vector2.Distance(playerPos, startingPos) <= radius)
        {
            return true;
        }
        else {
            return false;
        }
    }

    public virtual void Activate()
    {
        print("activate");
        if (isCalibrationSpace)
        {
            LevelManager.PlayerIsCalibrated();
        }
        else if (isEndSpace)
        {
            LevelManager.CompleteLevel();
        }
    }
}
