using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppScript : MonoBehaviour {

    public static AppScript instance;

	public static Restaurant[] restaurants = {
		new Restaurant("Marios", "Pizza"), 
		new Restaurant("Marios", "Pizza"), 
		new Restaurant("Marios", "Pizza"), 
		new Restaurant("Marios", "Pizza"), 
		new Restaurant("Marios", "Pizza"), 
		new Restaurant("Marios", "Pizza"), 
		new Restaurant("Marios", "Pizza"), 
		new Restaurant("Marios", "Pizza"), 
		new Restaurant("Marios", "Pizza"), 
		new Restaurant("Marios", "Pizza"), 
		new Restaurant("Marios", "Pizza"), 
		new Restaurant("Marios", "Pizza"), 
		new Restaurant("Marios", "Pizza"), 
		new Restaurant("Luigis", "More pizza")
	};

	public static Friend[] friends = {
		new Friend("Ross"), new Friend("Pikachu"), new Friend("Barb")
	};

	public GameObject restaurantButton;
	public GameObject restaurantButtonsContainer;
    public GameObject profilePage;
    public GameObject friendPrefab;

	// Use this for initialization
	void Start () {
        instance = this;
		foreach (Restaurant restaurant in restaurants) {
			GameObject newButton = Instantiate (restaurantButton, restaurantButtonsContainer.transform);
			RestaurantButtonScript newScript = newButton.GetComponent<RestaurantButtonScript> ();
			newScript.SetRestaurant(restaurant);
		}

        foreach (Friend friend in friends) {
            //TODO position friends correctly in the scene
            //GameObject newFriend = Instantiate(friendPrefab);
        }
	}
	
	// Update is called once per frame
	void Update () {
		// decrease satisfaction progressively
	}

	public void OrderAtRestaurant(Restaurant restaurant) {
		// calculate how satisfied your friends are

	}

    public void showProfile(Restaurant restaurant) {
        Instantiate(profilePage, GameObject.Find("PhoneOverlay").transform);
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
    public Ingredient requirement;

	public Friend(string name) {
		this.name = name;
		this.satisfaction = 100;
        this.requirement = Utils.randomIngredient();
        Debug.Log(name+" likes "+requirement);
	}
}

public enum Ingredient
{
    Peperoni, Tuna, Cheese
}

public class Utils
{

    private static System.Random random;

    private static System.Random getRandom()
    {
        if (random == null) {
            random = new System.Random();
        }
        return random;
    }

    public static Ingredient randomIngredient()
    {
        System.Array ingredients = System.Enum.GetValues(typeof(Ingredient));
        return (Ingredient)ingredients.GetValue(getRandom().Next(ingredients.Length));
    }
}