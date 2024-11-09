using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsCanvas;
    public Image transitionImage;  // Ensure this is assigned in the editor
    public float transitionTime = 1.0f;

    AudioCollection audioCollection;

    private void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
    }

    private void Start()
    {
        // Start with the transition image hidden
        transitionImage.gameObject.SetActive(false);
        StartCoroutine(TransitionOpening());
        audioCollection.PlayBGM(audioCollection.mainMenu);
    }

    public void PlayGame()
    {
        transitionImage.gameObject.SetActive(true);
        StartCoroutine(TransitionToScene());
    }

    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        StartCoroutine(TransitionAndQuit());
    }

    private IEnumerator TransitionToScene()
    {
        // Fade to black
        yield return StartCoroutine(FadeToBlack());

        // Load the new scene
        SceneManager.LoadScene(1);

        // Fade from black
        yield return StartCoroutine(FadeFromBlack());
    }

    private IEnumerator TransitionOpening()
    {
        yield return StartCoroutine(FadeFromBlack());
    }

    private IEnumerator TransitionAndQuit()
    {
        // Fade to black
        yield return StartCoroutine(FadeToBlack());

        // Quit the game
        Application.Quit();
    }

    private IEnumerator FadeToBlack()
    {
        // Activate the transition image before fading
        transitionImage.gameObject.SetActive(true);  
        
        for (float t = 0.0f; t < transitionTime; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0, 1, t / transitionTime);
            SetImageAlpha(alpha);
            yield return null;
        }
        
        // Ensure the alpha is set to 1 (fully black)
        SetImageAlpha(1);
    }

    private IEnumerator FadeFromBlack()
    {
        for (float t = 0.0f; t < transitionTime; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / transitionTime);
            SetImageAlpha(alpha);
            yield return null;
        }
        
        // Ensure the alpha is set to 0 (fully transparent)
        SetImageAlpha(0);
        
        // Deactivate the transition image after fading
        transitionImage.gameObject.SetActive(false);  
    }

    // Helper method to set the alpha (transparency) of the image
    private void SetImageAlpha(float alpha)
    {
        if (transitionImage != null)
        {
            Color color = transitionImage.color;
            color.a = alpha;
            transitionImage.color = color;
        }
        else
        {
            Debug.LogError("Transition Image is not assigned!");
        }
    }
}
