using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.SearchService;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private bool isTyping;
    private AutoStartDialogue autoStartDialogue;
    private AutoStartDialogue activeAutoDialogue; // Track the currently active AutoStartDialogue
    private static DialogueManager instance;
    private int currentChoiceIndex; // Track current choice index
    private int totalChoices; // Track total number of choices
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string REPUTATION_TAG = "reputation";
    private const string MENTAL_TAG = "mental";
    private const string FINANCE_TAG = "finance";

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene.");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if(!dialogueIsPlaying)
        {
            return;
        }
        if((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && !isTyping)
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON, AutoStartDialogue autoDialogue = null)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        activeAutoDialogue = autoDialogue; // Set the active AutoStartDialogue correctly
        ContinueStory();
    }


    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        if (activeAutoDialogue != null)
        {
            activeAutoDialogue.DialogueEnded(); // Inform AutoStartDialogue it’s finished
            activeAutoDialogue = null; // Reset the active AutoStartDialogue
        }
        StoryManager.Instance.DialogueCompleted();
    }

    private IEnumerator TypeDialogue(Story currentStory)
    {
        isTyping = true;
        string currStory = currentStory.Continue();
        dialogueText.text = "";
        foreach(char letter in currStory.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.001f);
        }
        yield return new WaitForSeconds(0.001f);
        isTyping = false;
    }

    private void ContinueStory()
    {
        if(currentStory.canContinue)
        {
            StopAllCoroutines();
            // set text for the current dialogue line
            StartCoroutine(TypeDialogue(currentStory));
            // display choices, if any, for this dialogue line
            DisplayChoices();
            // handle tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        // Loop through each tag and handle it accordingly
        foreach(string tag in currentTags)
        {
            // Split the tag into key and value
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
                continue; // Skip this tag if it's malformed
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch(tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    Debug.Log("layout="+tagValue);
                    break;
                case REPUTATION_TAG:
                case MENTAL_TAG:
                case FINANCE_TAG:
                    StatusManager.Instance.UpdateStatus(tagKey, tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: "+tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        totalChoices = currentChoices.Count; // Update total number of choices
        currentChoiceIndex = 0; // Reset to the first choice

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        UpdateChoiceSelection();
    }

    private void UpdateChoiceSelection()
    {
        if(currentChoiceIndex <= totalChoices)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(choices[currentChoiceIndex].gameObject);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if(!isTyping)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
        }
    }
}
