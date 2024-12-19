using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] float shakeIntensity = 1.0f;
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] TextMeshProUGUI HealthText;
    [SerializeField] float healthTextIncreaseFactor = 1.5f;
    [SerializeField] float healthTextAnimationDuration = 0.3f;
    [SerializeField] float flashDuration = 0.1f;
    [SerializeField] Color flashColor = Color.white;
    private Color originalTextColor;
    private bool isFlashing = false;

    private void Start()
    {
        originalTextColor = HealthText.color;
    }

    private void Update()
    {
        displayHealth();

        if (hitPoints <= 20 && !isFlashing)
        {
            StartCoroutine(FlashHealthText());
        }
    }

    public void displayHealth()
    {
        float currentHealth = hitPoints;
        HealthText.text = currentHealth.ToString();
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
            HealthText.enabled = false;
        }
        else
        {
            StartCoroutine(AnimateHealthText());
            StartCoroutine(FlashHealthTextOnDamage());
            CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeDuration);
        }
    }

    private IEnumerator AnimateHealthText()
    {
        Vector3 originalScale = HealthText.transform.localScale;
        Vector3 targetScale = originalScale * healthTextIncreaseFactor;

        HealthText.transform.localScale = targetScale;

        float timer = 0f;
        while (timer < healthTextAnimationDuration)
        {
            timer += Time.deltaTime;
            float scale = Mathf.Lerp(healthTextIncreaseFactor, 1f, timer / healthTextAnimationDuration);
            HealthText.transform.localScale = originalScale * scale;
            yield return null;
        }

        HealthText.transform.localScale = originalScale;
    }

    private IEnumerator FlashHealthTextOnDamage()
    {
        isFlashing = true;
        HealthText.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        HealthText.color = originalTextColor;
        isFlashing = false;
    }

    private IEnumerator FlashHealthText()
    {
        isFlashing = true;
        Color originalColor = HealthText.color;
        Color targetColor = Color.white;

        float halfFlashDuration = flashDuration / 2f;

        for (float t = 0; t < halfFlashDuration; t += Time.deltaTime)
        {
            HealthText.color = Color.Lerp(originalColor, targetColor, t / halfFlashDuration);
            yield return null;
        }

        // Swap the colors for the next iteration
        Color temp = originalColor;
        originalColor = targetColor;
        targetColor = temp;

        for (float t = 0; t < halfFlashDuration; t += Time.deltaTime)
        {
            HealthText.color = Color.Lerp(originalColor, targetColor, t / halfFlashDuration);
            yield return null;
        }

        HealthText.color = originalColor;
        isFlashing = false;
    }

    public void RestoreHealthToMax()
    {
        hitPoints = 100f;
    }

    public bool IsHealthFull()
    {
        return hitPoints >= 100f;
    }
}
