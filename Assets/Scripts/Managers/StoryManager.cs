using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance;
    public int currentDay = 1;
    private int totalDialogueTrigger;
    private int totalAutoDialogue;
    private int totalDialoguesForDay;
    private int dialoguesCompleted = 0;
    private bool canSleep = false;
    private GameObject transitionObject;
    private Animator transitionAnim;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        transitionObject = GameObject.Find("AreaTransition");
        transitionAnim = GameObject.Find("TransitionEffect").GetComponent<Animator>();
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
            currentDay++;
            if (currentDay <= 4) // Assuming Day1 to Day4 scenes
            {
                transitionObject.SetActive(true);
                transitionAnim.SetTrigger("Start");
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("Day" + currentDay);
                UpdateTransitionObjects();
                transitionAnim.SetTrigger("End");
                yield return new WaitForSeconds(3);
                LoadDay();
            }
            else
            {
                Debug.Log("Story completed.");
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

}
