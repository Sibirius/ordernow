using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendScript : MonoBehaviour {

    private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        InvokeRepeating("jump", 1.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {

		
	}

    void jump()
    {
        Vector3 direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.0f, 3.0f), Random.Range(-1.0f, 1.0f));
        float force = 1000.0f;
        rigidbody.AddForce(force * direction);
    }
}
