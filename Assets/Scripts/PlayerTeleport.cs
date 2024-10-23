using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;
    private GameObject transitionObject;
    private Animator transitionAnim;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        transitionObject = GameObject.Find("AreaTransition");
        transitionAnim = GameObject.Find("TransitionEffect").GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentTeleporter != null)
        {
            playerMovement.enabled = false;
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
        transitionObject.SetActive(true);
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        transform.position = currentTeleporter.GetComponent<Waypoint>().GetDestination().position;
        transitionAnim.SetTrigger("Start");
        transitionObject.SetActive(false);
        yield return new WaitForSeconds(1);
        playerMovement.enabled = true;
    }
}
