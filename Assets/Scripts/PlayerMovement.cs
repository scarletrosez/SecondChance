using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Camera mainCamera;
    private Vector2 moveDirection;
    private float x;
    private PlayerTeleport playerTeleport;

    // Camera follow and zoom settings
    private float cameraLerpSpeed = 5f;
    private float roomCameraZoom = 4f;  // Zoom level when in the room
    private float outsideCameraZoom = 10f; // Zoom level when outside the room

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        playerTeleport = FindObjectOfType<PlayerTeleport>();
    }

    void Update()
    {
        // Get horizontal input (A/D or left/right arrow keys)
        x = Input.GetAxis("Horizontal");

        // Determine the direction and movement
        moveDirection = new Vector2(x, 0f);

        // Flip the sprite based on the direction of movement
        if (x < 0)
        {
            sr.flipX = true;
        }
        else if (x > 0)
        {
            sr.flipX = false;
        }

        // Adjust the camera based on the player's position
        if (playerTeleport.isInRoom)
        {
            // Follow the player and zoom in when in the room
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z), Time.deltaTime * cameraLerpSpeed);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, roomCameraZoom, Time.deltaTime * cameraLerpSpeed);
        }
        else
        {
            // Follow the player and zoom out when outside
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(transform.position.x, transform.position.y + 3, mainCamera.transform.position.z), Time.deltaTime * cameraLerpSpeed);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, outsideCameraZoom, Time.deltaTime * cameraLerpSpeed);
        }
    }

    void FixedUpdate()
    {
        // Stop movement during dialogue or teleportation
        if (DialogueManager.GetInstance().dialogueIsPlaying || playerTeleport.isTeleporting)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // Apply movement to the Rigidbody2D in the FixedUpdate method
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }
}
