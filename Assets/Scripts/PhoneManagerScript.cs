﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManagerScript : MonoBehaviour {

    public GameObject restaurantButton;
    public GameObject restaurantButtonsContainer;
    public GameObject profilePage;
    public GameObject pizzaEntry;

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

        foreach(Pizza pizza in restaurant.pizzas)
        {
            Debug.Log("Pizza" + pizza.name);
            GameObject entry = Instantiate(pizzaEntry, GameObject.Find("PizzaList").transform);
            entry.GetComponentInChildren<Text>().text = pizza.name;
            RawImage[] images = entry.GetComponentsInChildren<RawImage>();
            images[0].texture = pizza.texture;
            images[1].texture = pizza.ingredients[0].texture;
        }

    }

    public void addRestaurant(Restaurant restaurant)
    {

        GameObject newButton = Instantiate(restaurantButton, restaurantButtonsContainer.transform);
        RestaurantButtonScript newScript = newButton.GetComponent<RestaurantButtonScript>();
        newScript.SetRestaurant(restaurant);
        
    }
}
