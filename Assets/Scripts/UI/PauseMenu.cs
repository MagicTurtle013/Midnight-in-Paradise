using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    private bool wasMouseLockedBeforePause;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = wasMouseLockedBeforePause ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !wasMouseLockedBeforePause;
        GetComponent<StarterAssets.FirstPersonController>().enabled = true;
        FindObjectOfType<WeaponSwitcher>().enabled = true;
        FindObjectOfType<WeaponZoom>().enabled = true;
    }

    void Pause()
    {
        wasMouseLockedBeforePause = Cursor.lockState == CursorLockMode.Locked;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GetComponent<StarterAssets.FirstPersonController>().enabled = false;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        FindObjectOfType<WeaponZoom>().enabled = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
