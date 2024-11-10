using UnityEngine;

public class SleepTrigger : MonoBehaviour
{
    [SerializeField] private GameObject saveIcon;
    private bool playerInRange;
    // private StoryManager storyManager;

    private void Start()
    {
        // storyManager = FindObjectOfType<StoryManager>();
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(StoryManager.Instance.TrySleep());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            saveIcon.SetActive(true);
            playerInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            saveIcon.SetActive(false);
            playerInRange = false;
        }
    }
}
