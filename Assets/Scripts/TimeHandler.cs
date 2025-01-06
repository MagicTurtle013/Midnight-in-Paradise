using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeHandler : MonoBehaviour
{
    [SerializeField] private AnimationCurve _timeSlowedCurve;
    private Coroutine _timeChangeCoroutine;

    public void OnTimeToggled(InputAction.CallbackContext context)
    {
        if (_timeChangeCoroutine != null)
        {
            StopCoroutine(_timeChangeCoroutine);
            _timeChangeCoroutine = null;
        }
        _timeChangeCoroutine = StartCoroutine(ChangeTime());
    }

    /// <summary>
    /// Changes the games timescale based on the curve values
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeTime()
    {
        float elapsed = 0f;
        
        // Loop while timescale has not returned to the initial timescale
        while(true)
        {
            Time.timeScale = _timeSlowedCurve.Evaluate(elapsed);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
