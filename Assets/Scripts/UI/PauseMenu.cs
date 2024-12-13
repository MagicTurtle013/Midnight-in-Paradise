using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    
    private bool _wasMouseLockedBeforePause;
    private static bool _gameIsPaused;
    private bool _playerController;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameIsPaused)
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
        _gameIsPaused = false;
        Cursor.lockState = _wasMouseLockedBeforePause ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !_wasMouseLockedBeforePause;
        //GetComponent<StarterAssets.FirstPersonController>().enabled = true;
        FindObjectOfType<WeaponSwitcher>().enabled = true;
        FindObjectOfType<WeaponZoom>().enabled = true;
    }

    void Pause()
    {
        _wasMouseLockedBeforePause = Cursor.lockState == CursorLockMode.Locked;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //gameObject.GetComponent<StarterAssets.FirstPersonController>().enabled = false;
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