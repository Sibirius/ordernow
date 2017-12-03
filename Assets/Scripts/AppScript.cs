using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppScript : MonoBehaviour {

    private static string[] restaurantNames =
    {
        "Marios", "Luigis", "Warios", "WaLuigis"
    };

    private static Pizza[] availablePizzas =
    {
        new Pizza("Pizza Tuna", new List<Ingredient> {Ingredient.Tuna}),
        new Pizza("Pizza Cheese", new List<Ingredient> {Ingredient.Cheese}),
        new Pizza("Pizza Peperoni", new List<Ingredient> {Ingredient.Peperoni}),
    };


    private Restaurant[] restaurants;

	private Friend[] friends = {
		new Friend("Ross"), new Friend("Pikachu"), new Friend("Barb")
	};

    public float failTimer;
    public int failAmount;

    public int pizzasPerRestaurant;
    public int numberOfRestaurants;

    public int satisfiedBonus;
    public int dissatisfiedMalus;

    public GameObject friendPrefab;

    private PhoneManagerScript phoneManager;

	// Use this for initialization
	void Start () {

        restaurants = createRestaurantsForFriends(friends);

        phoneManager = GetComponent<PhoneManagerScript>();
        phoneManager.showRestaurants(restaurants);

        foreach (Friend friend in friends) {
            //TODO position friends correctly in the scene
            //GameObject newFriend = Instantiate(friendPrefab);
        }

        InvokeRepeating("ReduceFriendSatisfaction", failTimer, failTimer);
	}

    void ReduceFriendSatisfaction()
    {
        foreach(Friend friend in friends)
        {
            friend.satisfaction -= failAmount;
            Debug.Log(friend.name + " is at " + friend.satisfaction);
        }
    }
	
	// Update is called once per frame
	void Update () {
		// decrease satisfaction progressively
	}

    private void alterFriendSatifaction(Friend friend, int change)
    {
        friend.satisfaction = Mathf.Clamp(friend.satisfaction + change, 0, 100);
        if (friend.satisfaction == 0) {
            //TODO lose the game
        }
    }

    private Restaurant[] createRestaurantsForFriends(Friend[] friends)
    {
        List<Restaurant> restaurants = new List<Restaurant>();
        for (int i = 0; i < numberOfRestaurants; i++)
        {
            List<Pizza> pizzas = new List<Pizza>();
            for (int p = 0; p < pizzasPerRestaurant; p++)
            {
                Pizza pizza = availablePizzas[Random.Range(0,availablePizzas.Length)];
                pizzas.Add(pizza);
            }
            string name = restaurantNames[Random.Range(0, restaurantNames.Length)];
            Restaurant restaurant = new Restaurant(name, pizzas);
            restaurants.Add(restaurant);
        }

        return restaurants.ToArray();
    }

	public void OrderAtRestaurant(Restaurant restaurant) {
        // calculate how satisfied your friends are

        Debug.Log("Ordering at " + restaurant.name);

        foreach (Friend friend in friends)
        {
            bool satisfied = false;
            foreach (Pizza pizza in restaurant.pizzas)
            {
                if (pizza.ingredients.Contains(friend.requirement))
                {
                    satisfied = true;
                }
            }
            if (satisfied)
            {
                Debug.Log(friend.name + " is satisfied");
                friend.satisfaction += satisfiedBonus;
            }
            else
            {
                friend.satisfaction -= dissatisfiedMalus;
            }
        }

	}


}

public class Restaurant {
	public string name;
	public string description;
    public List<Pizza> pizzas;

	public Restaurant(string name, string description) {
		this.name = name;
		this.description = description;
	}

    public Restaurant(string name, List<Pizza> pizzas)
    {
        this.name = name;
        this.pizzas = pizzas;
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

public class Pizza
{
    public string name;
    public List<Ingredient> ingredients;

    public Pizza(string name, List<Ingredient> ingredients)
    {
        this.name = name;
        this.ingredients = ingredients;
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

    public static AppScript getGameManager()
    {
        return GameObject.Find("GameManager").GetComponent<AppScript>();
    }

    public static PhoneManagerScript getPhoneManager()
    {
        return GameObject.Find("GameManager").GetComponent<PhoneManagerScript>();
    }
}