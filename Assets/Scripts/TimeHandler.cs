using UnityEngine;
using UnityEngine.InputSystem;

public class TimeHandler : MonoBehaviour
{
    [SerializeField] private float _slowedTime;
    public void OnTimeTogggled(InputAction.CallbackContext context)
    {
        Time.timeScale = Time.timeScale == _slowedTime ? 1f : _slowedTime;
    }
}
