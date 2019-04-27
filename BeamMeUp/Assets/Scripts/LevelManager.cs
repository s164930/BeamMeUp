using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int points = 0, pointsToWin = 3;
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
            return;
        }
        startBlock.GetComponent<StartBlock>().SpawnPlayer();
    }
}
