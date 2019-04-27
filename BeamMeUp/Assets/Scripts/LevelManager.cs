using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static int points = 0, pointsToWin = 3;
    public SceneFader sceneFader;
    private GameObject startBlock;
    public CameraController freeCam;
    // Start is called before the first frame update
    void Start()
    {
        startBlock = GameObject.FindGameObjectWithTag("StartBlock");
    }

    public void ReachedEnd(){
        points++;
        if(points >= pointsToWin){
            Debug.Log("You have won");
            //int scene = SceneManager.GetSceneByName("MainMenu").buildIndex;
            sceneFader.ChangeScene("MainMenu");
            return;
        }
        startBlock.GetComponent<StartBlock>().SpawnPlayer();
    }

    public void ResetLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneFader.ChangeScene(scene.name);
    }
}
