using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camerashake : MonoBehaviour
{
    public static Camerashake Instance{get;private set;}

    private CinemachineVirtualCamera CinemachineVirtualCamera;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;   

    private CinemachineBasicMultiChannelPerlin _cbmcp;

    private void Awake()
    {
        Instance=this;
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    
    public void ShakeCamera(float intensity,float time)
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = intensity;

        shakeTimerTotal=time;
        startingIntensity=intensity;
        shakeTimer = time;
    }
    // Update is called once per frame
    private void Update()
    {
        if(shakeTimer>0)
        {
            shakeTimer-=Time.deltaTime;
            if(shakeTimer<=0f)
            {
                CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                //_cbmcp.m_AmplitudeGain = Mathf.Lerp(startingIntensity,0f,shakeTimer/shakeTimerTotal);

                _cbmcp.m_AmplitudeGain=0f;

                
            }
        }
    }

    
  
}