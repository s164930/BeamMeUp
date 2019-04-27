using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBlock : MonoBehaviour
{
    public GameObject particleEffect;
    public StartBlock startBlock;
    public void Explode()
    {
        startBlock.Explode();
        Instantiate(particleEffect, transform.position, particleEffect.transform.rotation);
    }
    
}
