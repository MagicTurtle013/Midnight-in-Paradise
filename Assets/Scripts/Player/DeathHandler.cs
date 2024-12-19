using System.Collections;
using System.Collections.Generic;
using MpPlayerMovement;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void HandleDeath()
    {
        GetComponent<PlayerMovement>().enabled = false;
        gameOverCanvas.enabled=true;
        Time.timeScale = 0f;
        FindObjectOfType<WeaponSwitcher>().enabled= false;
        FindObjectOfType<WeaponZoom>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
