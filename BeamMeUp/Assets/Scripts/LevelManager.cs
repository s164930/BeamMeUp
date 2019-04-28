using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public static int points = 0, pointsToWin = 3;
    public SceneFader sceneFader;
    private GameObject startBlock;
    private List<GameObject> pointsUI;
    public CameraController freeCam;
    // Start is called before the first frame update
    void Start()
    {
        startBlock = GameObject.FindGameObjectWithTag("StartBlock");
        pointsUI = new List<GameObject>();
        pointsUI.Add(GameObject.Find("Point1"));
        pointsUI.Add(GameObject.Find("Point2"));
        pointsUI.Add(GameObject.Find("Point2"));
        points = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ResetLevel();
        }
    }
    public void ReachedEnd(){
        pointsUI[points].GetComponent<Image>().color = Color.cyan;
        points++;
        if(points >= pointsToWin){
            Debug.Log("You have won");
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if(sceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                sceneFader.ChangeScene("MainMenu");
            }
            sceneFader.ChangeScene("Level" + sceneIndex);
            points = 0;
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
