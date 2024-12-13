using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera fpsCamera;
    [SerializeField] StarterAssets.FirstPersonController fpsController;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 40f;
    [SerializeField] float zoomedOutSensitivity = 1.0f;
    [SerializeField] float zoomedInSensitivity = 0.5f;

    void Start()
    {
        fpsCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        fpsController = GetComponentInChildren<StarterAssets.FirstPersonController>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            fpsCamera.m_Lens.FieldOfView = zoomedInFOV;
            fpsController.RotationSpeed = zoomedInSensitivity;
        }
        else
        {
            fpsCamera.m_Lens.FieldOfView = zoomedOutFOV;
            fpsController.RotationSpeed = zoomedOutSensitivity;
        }
    }
}