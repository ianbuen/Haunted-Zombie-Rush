using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour {

    // SFX
    private AudioSource audioSource;
    [SerializeField] private AudioClip sfxJump;
    [SerializeField] private AudioClip sfxDeath;

    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float maxJumpHeight = 20f;

    private Rigidbody rigidBody;
    private Animator animator;

    private bool jump = false;

    // Check for files before start
    private void Awake() {
        Assert.IsNotNull(sfxJump);
        Assert.IsNotNull(sfxDeath);
    }

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if (!GameManager.instance.GameOver && GameManager.instance.GameStarted) {
            if (Input.GetMouseButtonDown(0)) {
                GameManager.instance.PlayerStartedGame();
                jump = true;
                rigidBody.useGravity = true;
                animator.Play("Jump");
                audioSource.PlayOneShot(sfxJump);
            }
        }
	}

    // Used for working with RigidBody
    private void FixedUpdate() {
        
        if (jump) {
            jump = false;
            rigidBody.velocity = new Vector2(0, 0);

            // Limit to only jump up to top edge of screen
            if (maxJumpHeight - transform.localPosition.y > 0.95f) {
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
    
        if (collision.gameObject.tag == "Obstacle") {
            animator.Play("Die");
            rigidBody.AddForce(new Vector2(50, 20), ForceMode.Impulse);
            rigidBody.detectCollisions = false;
            audioSource.PlayOneShot(sfxDeath);
            GameManager.instance.PlayerCollided();
        }
    }

    private void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.tag == "Brain") {
            animator.Play("Bite");
        }
    }
}
