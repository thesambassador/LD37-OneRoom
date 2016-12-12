using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;
    public int calibrationLevelIndex = 1;

    public int currentLevel = 1;

    void Awake()
    {
        if(instance == null)
            instance = this;

    }

    void Update()
    {

    }

    public static void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelNum);
    }

    public static void PlayerIsCalibrated()
    {
        PlayerRig.currentLevel.PutPlayerInStartingRoom();
    }

    public static void CompleteLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        
        if(currentLevel < SceneManager.sceneCountInBuildSettings - 1)
            LoadLevel(currentLevel + 1);
    }


	
	
}
