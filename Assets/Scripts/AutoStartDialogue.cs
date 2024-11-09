using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStartDialogue : MonoBehaviour
{
    [Header("Auto Start Object")]
    [SerializeField] private GameObject autoStart;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;
    public bool isDialogueFinished;

    private void Awake()
    {
        playerInRange = false;
    }

    private void Update()
    {
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if(isDialogueFinished)
            {
                autoStart.SetActive(false);
            }
            if(autoStart.CompareTag("AutoStart") && !isDialogueFinished)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    public void DialogueEnded()
    {
        if(playerInRange)
        {
            isDialogueFinished = true;
        }
    }
}
