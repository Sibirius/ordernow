using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendOverlay : MonoBehaviour {

    private Text nameLabel;
    private Transform cameraTransform;

    // Use this for initialization
    void Start () {
        nameLabel = GetComponentInChildren<Text>();
        cameraTransform = GameObject.FindObjectOfType<Camera>().transform;
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
    }
}
