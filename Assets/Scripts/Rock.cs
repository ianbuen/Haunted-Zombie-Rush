using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Brain {

    //[SerializeField] private Vector3 topPosition; // 21.55
    //[SerializeField] private Vector3 bottomPosition; // 5.75
    [SerializeField] private float v_speed = 1;
    [SerializeField] private float rotateSpeed = 1;

    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        moveToRandomAltitude();
        StartCoroutine(Move(bottomPosition));
	}

    protected override void Update() {
        base.Update();

        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }

    // IEnumerator is suitable for pausing and returning between iterations
    protected virtual IEnumerator Move(Vector3 target) {

        // Calculate distance between target and current position
        // Check each time if distance > stopping point, which is 0.20f units in distance
        while (Mathf.Abs((target - transform.localPosition).y) > 0.20f) {

            // Set the direction, depending on the target
            Vector3 direction = target.y == topPosition.y ? Vector3.up : Vector3.down;

            // Move the Rock towards the direction specified
            transform.localPosition += direction * Time.deltaTime * v_speed;

            yield return null;
        }

        // Pause for a half-sec after reaching target
        yield return new WaitForSeconds(0.5f);

        // Choose to go back to opposite direction
        Vector3 newTarget = target.y == topPosition.y ? bottomPosition : topPosition;
        StartCoroutine(Move(newTarget));
    }

    private void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.tag == "Player") {
            rigidBody.useGravity = true;
            rigidBody.isKinematic = false;
            rigidBody.AddForce(Vector3.left * 15, ForceMode.Impulse);
        }
    }
}
