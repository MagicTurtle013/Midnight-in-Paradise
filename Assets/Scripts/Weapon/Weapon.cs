using System.Collections;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Funtional Settings")]
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 50f;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] Animator gunAnimator;
    [SerializeField] TextMeshProUGUI ammoText;

    bool canShoot = true;

    private bool isRecoiling = false;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        canShoot = true;
    }

    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            gunAnimator.SetTrigger("Fire");
            StartCoroutine(Shoot());
        }

        if (isRecoiling)
        {
            Recoil();
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    public void PlayFireAnimation()
    {
        gunAnimator.SetTrigger("Fire");
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            ProcessRaycast();
            CinemachineShake.Instance.ShakeCamera(.3f, .1f);
            ammoSlot.ReduceCurrentAmmo(ammoType);

            if (!isRecoiling)
            {
                StartCoroutine(Recoil());
            }
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;

            if (hit.collider == target.headCollider)
            {
                target.TakeHeadshot(damage);
            }
            else if (hit.collider == target.bodyCollider)
            {
                target.TakeDamage(damage);
            }
        }
        else
        {
            return;
        }
    }


    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }

    IEnumerator Recoil()
    {
        isRecoiling = true;

        Vector3 recoilTranslation = new Vector3(0f, 0f, -0.1f);
        Quaternion recoilRotation = Quaternion.Euler(-10f, 0f, 0f);

        yield return new WaitForSeconds(0.06f);
        transform.Translate(recoilTranslation);
        yield return new WaitForSeconds(0.06f);
        transform.rotation *= recoilRotation;

        yield return new WaitForSeconds(0.1f);

        transform.localPosition = initialPosition;
        transform.localRotation = Quaternion.identity;

        isRecoiling = false;
    }
}