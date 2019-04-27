using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    private SceneFader fader;
    public void StartLevel(){
        string levelIndex = GetComponentInChildren<Text>().text;
       
        fader = GameObject.Find("SceneFader").GetComponent<SceneFader>();
        fader.ChangeScene("Level" + levelIndex);
    }
}
