using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantButtonScript : MonoBehaviour {

	public Restaurant restaurant;

	public string restaurantName;
	public string restaurantDescription;

	private Text buttonText;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Clicked () {
        Utils.getPhoneManager().showProfile(restaurant);
	}

	public void SetRestaurant(Restaurant restaurant) {
		this.restaurant = restaurant;
		buttonText = GetComponentInChildren<Text> ();
		buttonText.text = restaurant.name;
	}
}
