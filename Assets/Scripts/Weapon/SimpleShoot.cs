using UnityEngine;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab References")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location References")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Visual Settings")]
    [Tooltip("Specify time to destroy the casing object")]
    [SerializeField] private float destroyTimer = 0.5f;
    [Tooltip("Specify time to destroy the flash object")]
    [SerializeField] private float flashTimer = 0.5f;
    [Tooltip("Bullet Speed")]
    [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")]
    [SerializeField] private float ejectPower = 150f;

    private Weapon _weapon;
    private Ammo _ammo;
    private AmmoType _ammoType;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        _weapon = transform.parent.GetComponent<Weapon>(); // Get the reference to the 'Weapon' component
        _ammo = GetComponent<Ammo>(); // Get the reference to the 'Ammo' component
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _weapon.PlayFireAnimation();
        }
    }

    public void SetFireAnimationTrigger()
    {
        gunAnimator.SetTrigger("Fire");
    }

    // This function creates the bullet behavior
    public void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            // Create the muzzle flash
            GameObject tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            // Add a random rotation on the z-axis
            float randomRotation = Random.Range(0f, 360f);
            tempFlash.transform.Rotate(Vector3.forward, randomRotation);

            // Attach the muzzle flash to the weapon
            tempFlash.transform.parent = transform;

            // Destroy the muzzle flash object after flashTimer seconds
            Destroy(tempFlash, flashTimer);
        }

        // Cancels if there's no bullet prefab
        if (!bulletPrefab)
        {
            return;
        }

        // Create a bullet and add force to it in the direction of the barrel
        GameObject bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
    }

    // This function creates a casing at the ejection slot
    void CasingRelease()
    {
        // Cancels function if the ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        {
            return;
        }

        // Create the casing
        GameObject tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;

        // Add force to the casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);

        // Add torque to make the casing spin in a random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        // Destroy the casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }
}