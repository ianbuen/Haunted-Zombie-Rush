using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour {

    [SerializeField] private float h_speed;
    [SerializeField] protected float startPosition = -56.32f;
    [SerializeField] protected float resetPosition = 45.95f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (!GameManager.instance.GameOver) {
            transform.position += Vector3.right * (h_speed) * Time.deltaTime;

            if (transform.localPosition.x >= resetPosition) {
                Vector3 newPosition = new Vector3(startPosition, transform.position.y, transform.position.z);
                transform.position = newPosition;
            }
        }
	}
}
