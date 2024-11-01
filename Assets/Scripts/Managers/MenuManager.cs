using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsCanvas;

    // private const string SaveKey = "SavedScene"; // Key to check for saved data

    // Function to handle the Play button
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    // Function to handle the Settings button
    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
    }

    // Function to close settings
    public void CloseSettings()
    {
        settingsCanvas.SetActive(false);
    }

    // Function to handle the Quit button
    public void QuitGame()
    {
        Debug.Log("Quit button clicked - quitting the game.");
        Application.Quit();
    }

    // // Function to simulate saving the current scene (for testing purposes)
    // public void SaveGame(string sceneName)
    // {
    //     PlayerPrefs.SetString(SaveKey, sceneName);
    //     PlayerPrefs.Save();
    //     Debug.Log("Game saved: " + sceneName);
    // }
}
