using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendScript : MonoBehaviour {

    private Rigidbody rigidbody;
    public Friend friend;


	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        InvokeRepeating("jump", Random.Range(0.2f, 1.0f), Random.Range(0.8f, 1.2f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setFriend(Friend friend)
    {
        this.friend = friend;
        GetComponentInChildren<FriendOverlay>().setFriend(friend);
    }

    void jump()
    {
        Vector3 direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(2.0f, 8.0f), Random.Range(-1.0f, 1.0f));
        float force = 1000.0f;
        rigidbody.AddForce(force * direction);
        GetComponent<AudioSource>().Play();
    }
}
