using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float bobbingSpeed = 1f;
    [SerializeField] float bobbingHeight = 0.5f;
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;

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
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            Destroy(gameObject);
        }
    }
}