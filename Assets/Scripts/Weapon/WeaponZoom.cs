
using MpPlayerInput;
using MpPlayerMovement;
using UnityEngine;
using Unity.Cinemachine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] CinemachineCamera fpsCamera;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 40f;
    [SerializeField] float zoomedOutSensitivity = 1.0f;
    [SerializeField] float zoomedInSensitivity = 0.5f;

    /// <summary>
    /// Internal rotation multiplier to normalize UserInput
    /// </summary>
    private const float _rotationSpeedMultiplier = 10f;

    void Start()
    {
        fpsCamera = GetComponentInChildren<CinemachineCamera>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            fpsCamera.Lens.FieldOfView = zoomedInFOV;
            PlayerMovement.Instance.RotationSpeed = PlayerMovement.Instance.BaseRotationSpeed * zoomedInSensitivity * _rotationSpeedMultiplier;
        }
        else
        {
            fpsCamera.Lens.FieldOfView = zoomedOutFOV;
            PlayerMovement.Instance.RotationSpeed = PlayerMovement.Instance.BaseRotationSpeed * zoomedOutSensitivity * _rotationSpeedMultiplier;
        }
    }
}