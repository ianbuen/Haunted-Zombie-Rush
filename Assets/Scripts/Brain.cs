using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : Platform {

    [SerializeField] protected Vector3 topPosition;
    [SerializeField] protected Vector3 bottomPosition;

    [SerializeField] private AudioClip sfxCollect;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        moveToRandomAltitude();
    }
	
	// Update is called once per frame
	protected override void Update () {
        if (GameManager.instance.PlayerActive) {
            base.Update();
            
            if (Mathf.Abs(transform.localPosition.x - resetPosition) <= 0.05f) {
            //if (transform.localPosition.x > resetPosition) { 
                //transform.position = new Vector3(startPosition, SpawnAltitude(), transform.position.z);
                moveToRandomAltitude();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Player") {
            audioSource.PlayOneShot(sfxCollect);
            transform.position = new Vector3(resetPosition, RandomAltitude(), transform.position.z);
            GameManager.instance.AddScore();
        }
    }

    protected void moveToRandomAltitude() {
        transform.position = new Vector3(transform.position.x, RandomAltitude(), transform.position.z);
    }

    private float RandomAltitude() {
        return Random.Range(bottomPosition.y, topPosition.y);
    }
}
