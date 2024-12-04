using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 20f;
    [SerializeField] float bobbingSpeed = 1f;
    [SerializeField] float bobbingHeight = 0.1f;

    private Transform cachedTransform;
    private Vector3 initialPosition;

    private void Start()
    {
        cachedTransform = transform;
        initialPosition = cachedTransform.position;
    }

    private void Update()
    {
        // Rotation
        cachedTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Bobbing
        Vector3 bobbingOffset = Vector3.up * Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
        cachedTransform.position = initialPosition + bobbingOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("Player health full: " + playerHealth.IsHealthFull());
                if (!playerHealth.IsHealthFull())
                {
                    playerHealth.RestoreHealthToMax();
                    Destroy(gameObject);
                }
            }
        }
    }
}
