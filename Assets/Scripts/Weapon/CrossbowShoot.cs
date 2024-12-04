using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nokobot.Assets.Crossbow
{
    public class CrossbowShoot : MonoBehaviour
    {
        [Header("Visual Settings")]
        public GameObject arrowPrefab;
        public Transform arrowLocation;
        public float shotPower = 100f;
        public float delayBetweenShots = 1f;

        private bool canShoot = true;
        private Ammo ammo;
        private AmmoType ammoType;

        private void Start()
        {
            if (arrowLocation == null)
                arrowLocation = transform;

            // Get the Ammo component from the current game object or its parent
            ammo = GetComponent<Ammo>();
            if (ammo == null)
                ammo = GetComponentInParent<Ammo>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1") && canShoot)
            {
                if (ammo != null && ammo.GetCurrentAmmo(ammoType) > 0)
                {
                    StartCoroutine(ShootWithDelay());
                }
            }
        }

        private IEnumerator ShootWithDelay()
        {
            canShoot = false;
            Instantiate(arrowPrefab, arrowLocation.position, arrowLocation.rotation).GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * shotPower);
            yield return new WaitForSeconds(delayBetweenShots);
            canShoot = true;
        }
    }
}