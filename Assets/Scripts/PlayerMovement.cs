using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    private Vector2 moveDirection;
    private float x;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
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
    }

    void FixedUpdate()
    {
        if(DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        // Apply movement to the Rigidbody2D in the FixedUpdate method
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }
}
