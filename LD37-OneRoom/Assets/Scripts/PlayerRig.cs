using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRig : MonoBehaviour {

    public static PlayerRig currentRig;
    public static PlayerTeleporter playerTeleporter;
    public static SteamVR_PlayArea playArea;
    public static LevelGrid currentLevel;
    public static VRTK.VRTK_HeadsetFade fade;

    // Use this for initialization
    void Awake()
    {
        print("checking rig");
        if (currentRig != null)
        {
            print("rig exists");
            Destroy(this.gameObject);
        }
        else
        {
            print("first rig yeah");
            currentRig = this;
            playerTeleporter = GetComponent<PlayerTeleporter>();
            playArea = GetComponent<SteamVR_PlayArea>();
            fade = GetComponent<VRTK.VRTK_HeadsetFade>();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("startArea 1");
        }
    }
}
