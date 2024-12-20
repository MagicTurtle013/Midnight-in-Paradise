using System.Collections;
using TMPro;
using MpPlayerValues;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [Header("Functional Settings")]
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 50f;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] protected Animator weaponAnimator;
    [SerializeField] TextMeshProUGUI ammoText;

    [Tooltip("The gun resets between each shot")]
    private bool _isGunReset = true;

    private bool isRecoiling;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnEnable()
    {
        _isGunReset = true;
    }

    private void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButtonDown(0) && CanShoot())
        {
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

    private void PlayFireAnimation()
    {
        weaponAnimator?.SetTrigger("Fire");
    }

    /// <summary>
    /// Weapon must have ammo
    /// </summary>
    /// <returns></returns>
    private bool CanShoot()
    {
        return ammoSlot.GetCurrentAmmo(ammoType) > 0
            && _isGunReset;
    }

    private IEnumerator Shoot()
    {
        _isGunReset = false;

        ProcessRaycast();
        PlayFireAnimation();
        WeaponEffects();
        SpawnProjectile();
        CinemachineShake.Instance.ShakeCamera(.3f, .1f);
        ammoSlot.ReduceCurrentAmmo(ammoType);

        /*if (!isRecoiling)
        {
            StartCoroutine(Recoil());
        }*/

        yield return new WaitForSeconds(timeBetweenShots);
        _isGunReset = true;
    }

    protected virtual void SpawnProjectile()
    {
        Debug.LogWarning("SpawnProjectile not implemented");
    }

    protected virtual void WeaponEffects()
    {
        Debug.LogWarning("Weapon Effects not implemented");
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(PlayerValues.Instance.FirstPersonCamera.transform.position, PlayerValues.Instance.FirstPersonCamera.transform.forward, out hit, range))
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