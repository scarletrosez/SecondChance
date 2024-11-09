using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign Pause Menu Panel in the Inspector
    public GameObject optionsMenuUI; // Assign Options Panel in the Inspector
    private bool isPaused = false;

    void Update()
    {
        // Toggle pause when pressing the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true); // Show pause menu
        optionsMenuUI.SetActive(false); // Ensure options menu is hidden
        Time.timeScale = 0f; // Freeze game time
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false); // Hide pause menu
        optionsMenuUI.SetActive(false); // Hide options menu if open
        Time.timeScale = 1f; // Resume game time
    }

    public void OpenOptions()
    {
        pauseMenuUI.SetActive(false); // Hide pause menu
        optionsMenuUI.SetActive(true); // Show options menu
    }

    public void CloseOptions()
    {
        pauseMenuUI.SetActive(true); // Show pause menu
        optionsMenuUI.SetActive(false); // Hide options menu
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f; // Ensure game time is normal in main menu
        SceneManager.LoadScene("MainMenu"); // Load your Main Menu scene
    }
}
