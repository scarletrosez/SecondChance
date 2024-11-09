using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;
    private GameObject transitionObject;
    private Animator transitionAnim;
    public bool isTeleporting;
    public bool isInRoom = false;
    private AudioCollection audioCollection;

    private void Start()
    {
        transitionObject = GameObject.Find("AreaTransition");
        transitionAnim = GameObject.Find("TransitionEffect").GetComponent<Animator>();
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && currentTeleporter != null)
        {
            if(isInRoom == true)
            {
                isInRoom = false;
            }
            else
            {
                isInRoom = true;
            }
            NextLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Waypoint"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Waypoint"))
        {
            if(collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }

    private void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        audioCollection.PlaySFX(audioCollection.doorSound);
        isTeleporting = true;
        transitionObject.SetActive(true);
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        transform.position = currentTeleporter.GetComponent<Waypoint>().GetDestination().position;
        transitionAnim.SetTrigger("End");
        Debug.Log("Passed this line");
        yield return new WaitForSeconds(1);
        transitionObject.SetActive(false);
        isTeleporting = false;
    }
}
