using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class FadeViewWhenColliding : MonoBehaviour {

    VRTK_HeadsetFade headsetFade;
    public Transform jointObject;

    public float startFadeDist = .05f;
    public float fullFadeDist = .15f;

	// Use this for initialization
	void Start () {
        headsetFade = GetComponent<VRTK_HeadsetFade>();
	}
	
	// Update is called once per frame
	void Update () {

        float dist = Vector3.Distance(transform.position, jointObject.position);
        //print(dist);

        if (dist > startFadeDist)
        {
            if(dist > fullFadeDist)
                dist = fullFadeDist;
            
            float normDist = (dist - startFadeDist) / (fullFadeDist - startFadeDist);

            Color fadeCol = Color.black;
            fadeCol.a = normDist;

            headsetFade.Fade(fadeCol, 0);
        }
        else
        {
            headsetFade.Unfade(0);
        }

	}
}
