using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantProfileScript : MonoBehaviour {

	public Restaurant restaurant;

	private Text restaurantNameLabel;
	private Text restaurantDescriptionLabel;

    private void Awake()
    {
        Text[] labels = GetComponentsInChildren<Text>();

        restaurantNameLabel = labels[1];
        restaurantDescriptionLabel = labels[2];
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SetRestaurant(Restaurant restaurant)
    {
        this.restaurant = restaurant;
        restaurantNameLabel.text = restaurant.name;
        restaurantDescriptionLabel.text = restaurant.description;
    }

    public void OrderButtonPressed() {
        Utils.getGameManager().OrderAtRestaurant(this.restaurant);
	}

	public void BackButtonPressed() {
        Destroy(gameObject);
	}
}
