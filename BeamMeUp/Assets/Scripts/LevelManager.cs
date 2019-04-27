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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReachedEnd(){
        points++;
        if(points >= pointsToWin){
            Debug.Log("You have won");
            int scene = SceneManager.GetSceneByName("MainMenu").buildIndex;
            sceneFader.ChangeScene(scene);
            return;
        }
        startBlock.GetComponent<StartBlock>().SpawnPlayer();
    }
}
