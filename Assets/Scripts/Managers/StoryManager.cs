using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance;
    public int currentDay = 0;
    private int totalDialogueTrigger;
    private int totalAutoDialogue;
    private int totalDialoguesForDay;
    private int dialoguesCompleted = 0;
    public bool canSleep = false;
    public GameObject transitionObject;
    public Animator transitionAnim;
    private AudioCollection audioCollection;

    private void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        transitionObject = GameObject.Find("AreaTransition");
        transitionAnim = GameObject.Find("TransitionEffect").GetComponent<Animator>();
        // Load the saved day progress, or start from Day 1 if no saved data exists
        currentDay = PlayerPrefs.GetInt("SavedDay", 0); // Default to Day1 if no saved day is found
        if(currentDay == 0)
        {
            Debug.Log("Play Inroom Audio");
            audioCollection.PlayBGM(audioCollection.inRoom);
        }
        else
        {
            Debug.Log(currentDay);
            audioCollection.PlayBGM(audioCollection.inCasino);
        }
        LoadDay();
    }

    public void LoadDay()
    {
        totalDialogueTrigger = FindObjectsOfType<DialogueTrigger>().Length; // Find all dialogue triggers in this scene
        totalAutoDialogue = FindObjectsOfType<AutoStartDialogue>().Length;
        totalDialoguesForDay = totalDialogueTrigger + totalAutoDialogue;
        dialoguesCompleted = 0;
        canSleep = false;
    }

    public void DialogueCompleted()
    {
        dialoguesCompleted++;
        if (dialoguesCompleted >= totalDialoguesForDay)
        {
            canSleep = true;
        }
    }

    public IEnumerator TrySleep()
    {
        Debug.Log("Entered TrySleep");
        if (canSleep)
        {
            Debug.Log("Current day : " + currentDay);
            currentDay++;
            SaveProgress();
            if (currentDay < 4) // Assuming Day1 to Day4 scenes
            {
                transitionObject.SetActive(true);
                transitionAnim.SetTrigger("Start");
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("Day" + currentDay);
                if(currentDay == 0)
                {
                    Debug.Log("Play Inroom Audio");
                    audioCollection.PlayBGM(audioCollection.inRoom);
                }
                else
                {
                    Debug.Log(currentDay);
                    audioCollection.PlayBGM(audioCollection.inCasino);
                }
                UpdateTransitionObjects();
                transitionAnim.SetTrigger("End");
                yield return new WaitForSeconds(3);
                LoadDay();
            }
            else
            {
                Debug.Log("Destoying StoryManager");
                transitionObject.SetActive(true);
                transitionAnim.SetTrigger("Start");
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("Epilogue");
                UpdateTransitionObjects();
                transitionAnim.SetTrigger("End");
                yield return new WaitForSeconds(3);
                audioCollection.PlayBGM(audioCollection.inCasino);
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("Complete all dialogues before sleeping.");
        }
    }

    private void UpdateTransitionObjects()
    {
        transitionObject = GameObject.Find("AreaTransition");
        transitionAnim = GameObject.Find("TransitionEffect").GetComponent<Animator>();
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("SavedDay", currentDay); // Save the current day
        // PlayerPrefs.Save(); // Ensure the data is written to disk
    }

}
