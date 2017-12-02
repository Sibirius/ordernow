using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantProfileScript : MonoBehaviour {

	public Restaurant restaurant;

	private Text restaurantNameLabel;
	private Text restaurantDescriptionLabel;

	// Use this for initialization
	void Start () {
		Text[] labels = GetComponentsInChildren<Text> ();

		restaurantNameLabel = labels [1];
		restaurantDescriptionLabel = labels [2];
	}
	
	// Update is called once per frame
	void Update () {
		if (restaurant != null) {
			restaurantNameLabel.text = restaurant.name;
			restaurantDescriptionLabel.text = restaurant.description;
		} else {
			restaurantNameLabel.text = "ooops";
		}
	}

	public void OrderButtonPressed() {
		
	}

	public void BackButtonPressed() {
		gameObject.SetActive(false);
	}
}
