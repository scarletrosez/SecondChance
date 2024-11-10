using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Epilogue : MonoBehaviour
{
    public void GoToMainMenu()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save(); // Ensures all changes are saved
        SceneManager.LoadScene(0);
    }
}
