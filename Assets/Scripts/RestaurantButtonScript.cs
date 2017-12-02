using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantButtonScript : MonoBehaviour {

	public Restaurant restaurant;

	public string restaurantName;
	public string restaurantDescription;

	private GameObject profile;

	private Text buttonText;

	// Use this for initialization
	void Start () {
		profile = GameObject.Find ("RestaurantProfile");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Clicked () {
		Debug.Log (restaurant);
		profile.GetComponent<RestaurantProfileScript> ().restaurant = restaurant;
		profile.SetActive (true);
	
	}

	public void SetRestaurant(Restaurant restaurant) {
		this.restaurant = restaurant;
		buttonText = GetComponentInChildren<Text> ();
		buttonText.text = restaurant.name;
	}
}
