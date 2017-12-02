using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppScript : MonoBehaviour {

	public Restaurant[] restaurants = {
		new Restaurant("Marios", "Pizza"), new Restaurant("Luigis", "More pizza")
	};

	public Friend[] friends = {
		new Friend("Ross"), new Friend("Pikachu"), new Friend("Barb")
	}

	public GameObject restaurantButton;
	public GameObject restaurantButtonsContainer;

	// Use this for initialization
	void Start () {
		foreach (Restaurant restaurant in restaurants) {
			GameObject newButton = Instantiate (restaurantButton, restaurantButtonsContainer.transform) as GameObject;
			RestaurantButtonScript newScript = newButton.GetComponent<RestaurantButtonScript> ();
			newScript.SetRestaurant(restaurant);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// decrease satisfaction progressively
	}

	public void OrderAtRestaurant(Restaurant restaurant) {
		// calculate how satisfied your friends are

	}
}

public class Restaurant {
	public string name;
	public string description;

	public Restaurant(string name, string description) {
		this.name = name;
		this.description = description;
	}
}

public class Friend {
	public string name;
	public int satisfaction;

	public Friend(string name) {
		this.name = name;
		this.satisfaction = 100;
	}
}
