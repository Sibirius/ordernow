﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManagerScript : MonoBehaviour {

    public GameObject restaurantButton;
    public GameObject restaurantButtonsContainer;
    public GameObject profilePage;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void showProfile(Restaurant restaurant)
    {
        Debug.Log("show profile for " + restaurant.name);
        GameObject profile = Instantiate(profilePage, GameObject.Find("PhoneOverlay").transform);
        profile.GetComponent<RestaurantProfileScript>().SetRestaurant(restaurant);
    }

    public void showRestaurants(Restaurant[] restaurants)
    {
        foreach (Restaurant restaurant in restaurants)
        {
            GameObject newButton = Instantiate(restaurantButton, restaurantButtonsContainer.transform);
            RestaurantButtonScript newScript = newButton.GetComponent<RestaurantButtonScript>();
            newScript.SetRestaurant(restaurant);
        }
    }
}
