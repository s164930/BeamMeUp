﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    // Start is called before the first frame update
    void Start(){
        freeLookCamera.m_LookAt = GameObject.FindGameObjectWithTag("Player").transform;
        freeLookCamera.m_Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){
            float mouseMovement = Input.GetAxis("Mouse X");
            freeLookCamera.m_XAxis.Value = mouseMovement;
        }
    }
}
