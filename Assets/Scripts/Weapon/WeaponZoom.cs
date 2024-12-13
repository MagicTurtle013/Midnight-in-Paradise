using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Unity.Cinemachine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] CinemachineCamera fpsCamera;
    [SerializeField] PlayerMovement fpsController;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 40f;
    [SerializeField] float zoomedOutSensitivity = 1.0f;
    [SerializeField] float zoomedInSensitivity = 0.5f;

    void Start()
    {
        fpsCamera = GetComponentInChildren<CinemachineCamera>();
        fpsController = GetComponentInChildren<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            fpsCamera.Lens.FieldOfView = zoomedInFOV;
            fpsController.RotationSpeed = zoomedInSensitivity;
        }
        else
        {
            fpsCamera.Lens.FieldOfView = zoomedOutFOV;
            fpsController.RotationSpeed = zoomedOutSensitivity;
        }
    }
}