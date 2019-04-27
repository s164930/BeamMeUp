using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : MonoBehaviour
{

    public GameObject player;
    private Transform grid;
    // Start is called before the first frame update
    void Awake(){
        grid = transform.parent;
        Instantiate(player, transform.position + new Vector3(0,1f,0), player.transform.rotation, grid);    
    }

    public void SpawnPlayer(){
        Instantiate(player, transform.position + new Vector3(0,1f,0), player.transform.rotation, grid);    
    }

    
}
