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
        new Pizza("Pizza Tuna"),
        new Pizza("Pizza Cheese"),
        new Pizza("Pizza Peperoni"),
    };


    private Restaurant[] restaurants;

    private List<Friend> friends;

    public float failTimer;
    public int failAmount;

    public int numberOfFriends;
    public int pizzasPerRestaurant;
    public int numberOfRestaurants;

    public int satisfiedBonus;
    public int dissatisfiedMalus;

    public GameObject[] friendPrefabs;
    public Texture[] ingredientTextures;

    private List<Ingredient> ingredients;

    private PhoneManagerScript phoneManager;

	// Use this for initialization
	void Start () {

        ingredients = createIngredients(ingredientTextures);
        friends = createFriends(numberOfFriends);
        restaurants = createRestaurantsForFriends(friends);

        phoneManager = GetComponent<PhoneManagerScript>();
        phoneManager.showRestaurants(restaurants);

        InvokeRepeating("ReduceFriendSatisfaction", failTimer, failTimer);
	}

    void ReduceFriendSatisfaction()
    {
        foreach(Friend friend in friends)
        {
            alterFriendSatisfaction(friend, -failAmount);
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void alterFriendSatisfaction(Friend friend, int change)
    {
        friend.satisfaction = Mathf.Clamp(friend.satisfaction + change, 0, 100);
        if (friend.satisfaction == 0) {
            //TODO lose the game
        }
    }

    private List<Ingredient> createIngredients(Texture[] textures)
    {
        ingredients = new List<Ingredient>();
        foreach (Texture texture in textures)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.name = texture.name;
            ingredient.texture = texture;
            ingredients.Add(ingredient);
        }
        return ingredients;
    }

    private List<Friend> createFriends(int numberOfFriends)
    {
        List<Friend> friends = new List<Friend>();
        for (int i = 0; i < numberOfFriends; i++)
        {
            Vector3 position = new Vector3(Random.Range(-0.7f, 1.5f), 0.0f, Random.Range(-0.7f, -2.0f));
            Quaternion rotation = Quaternion.Euler(0.0f, Random.Range(150.0f, 210.0f), 0.0f);
            //GameObject prefab = friendPrefabs[Random.Range(0, friendPrefabs.Length)];
            GameObject prefab = friendPrefabs[0];
            GameObject gameObject = Instantiate(prefab, position, rotation);
            Friend friend = new Friend();
            friend.requirement = ingredients[Random.Range(0,ingredients.Count)];
            gameObject.GetComponent<FriendScript>().setFriend(friend);
            friends.Add(friend);
        }
        return friends;
    } 

    private Restaurant[] createRestaurantsForFriends(List<Friend> friends)
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
	public int satisfaction;
    public Ingredient requirement;

	public Friend() {
		this.satisfaction = 100;
	}
}

public class Pizza
{
    public string name;
    public List<Ingredient> ingredients;

    public Pizza(string name)
    {
        this.name = name;
        //this.ingredients = ingredients;
    }

}

public class Ingredient
{
    public string name;
    public Texture texture;
}

public class Utils
{

    public static AppScript getGameManager()
    {
        return GameObject.Find("GameManager").GetComponent<AppScript>();
    }

    public static PhoneManagerScript getPhoneManager()
    {
        return GameObject.Find("GameManager").GetComponent<PhoneManagerScript>();
    }
}