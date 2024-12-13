using Unity.Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
https://www.youtube.com/watch?v=ACf1I27I6Tk
*/

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance {get; private set; }

    private CinemachineCamera cinemachineCamera;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private void Awake()
    {
       Instance = this;
       cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;

        startingIntensity= intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer> 0) 
        { 
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f)
            {
                //Time over!
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.AmplitudeGain = 
                    Mathf.Lerp(startingIntensity, 0f, (1-(shakeTimer / shakeTimerTotal)));
            }
        }
    }

}