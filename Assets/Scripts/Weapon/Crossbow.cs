using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MpCrossbow
{
    public class Crossbow : Weapon
    {
        [Header("Visual Settings")]
        public GameObject arrowPrefab;
        public Transform arrowLocation;
        public float shotPower = 100f;

        protected override void SpawnProjectile()
        {
            Instantiate(arrowPrefab, arrowLocation.position, arrowLocation.rotation).GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * shotPower);
        }
    }
}