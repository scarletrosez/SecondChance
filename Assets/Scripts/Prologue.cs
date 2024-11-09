using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with player");
            if(StoryManager.Instance.canSleep == true)
            {
                Debug.Log("Trying to sleep");
                StartCoroutine(StoryManager.Instance.TrySleep());
            }
        }
    }
}
