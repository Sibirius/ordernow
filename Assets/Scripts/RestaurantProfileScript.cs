using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantProfileScript : MonoBehaviour {

	public Restaurant restaurant;

	private Text restaurantNameLabel;

    private void Awake()
    {
        restaurantNameLabel = transform.Find("Title").Find("RestaurantName").GetComponent<Text>();

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
    }

    public void OrderButtonPressed() {
        Utils.getGameManager().OrderAtRestaurant(this.restaurant);
	}

	public void BackButtonPressed() {
        Destroy(gameObject);
	}
}
