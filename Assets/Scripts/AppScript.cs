using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AppScript : MonoBehaviour {

    private static string[] restaurantNames =
    {
        "Triominos", "Momma Johanna", "Pizza Fedora", "Wario's", "Pizza Can Dough", "Grate Pizza", "The Last of Crust", "Jesus Crust", "R.I.Pizza"
    };

    private int currentLevel;

    private List<string> availableRestaurantNames;
    private List<Ingredient> availableRequirements;

    private List<Restaurant> restaurants;

    private List<Friend> friends;
    private List<GameObject> friendObjects;

    public float failTimer;
    public int failAmount;

    public int numberOfFriends;
    public int pizzasPerRestaurant;
    public int numberOfRestaurants;

    public int satisfiedBonus;
    public int dissatisfiedMalus;

    public GameObject friendOverlay;

    public GameObject[] friendPrefabs;
    public Texture[] ingredientTextures;

    private List<Ingredient> ingredients;
    private List<Pizza> pizzas;

    private PhoneManagerScript phoneManager;

	// Use this for initialization
	void Start () {

        currentLevel = 0;
        phoneManager = GetComponent<PhoneManagerScript>();

        List<GameObject> shuffled = Utils.shuffleList(new List<GameObject>(friendPrefabs));
        friendPrefabs = shuffled.ToArray();

        ingredients = createIngredients();

        availableRestaurantNames = new List<string>(restaurantNames);
        availableRequirements = new List<Ingredient>(ingredients);

        pizzas = createPizzas();

        friendObjects = new List<GameObject>();
        friends = new List<Friend>();
        for (int i = 0; i < numberOfFriends; i++)
        {
            spawnFriend();
        }

        restaurants = new List<Restaurant>();
        for (int i = 0; i < numberOfRestaurants; i++)
        {
            createRestaurant();
        }


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
            SceneManager.LoadScene("LoseScene");
        }
    }

    private List<Ingredient> createIngredients()
    {
        ingredients = new List<Ingredient>();
        foreach (Texture texture in ingredientTextures)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.name = texture.name;
            ingredient.texture = texture;
            ingredients.Add(ingredient);
        }
        return ingredients;
    }

    private List<Pizza> createPizzas()
    {
        List<Pizza> pizzas = new List<Pizza>();
        pizzas.Add(createPizza("Wraperoni", "pizza_peperoni", new List<string> { "peperoni" }));
        pizzas.Add(createPizza("Boring Pizza", "pizza_mushroom", new List<string> { "mushroom" }));
        pizzas.Add(createPizza("Pizza Popeye", "pizza_spinach", new List<string> { "spinach" }));
        pizzas.Add(createPizza("Pizza Pidgey", "pizza_chicken", new List<string> { "chicken" }));
        pizzas.Add(createPizza("The Seaman", "pizza_fish", new List<string> { "fish" }));
        pizzas.Add(createPizza("Baconator", "pizza_pork", new List<string> { "pork" }));
        pizzas.Add(createPizza("Beef Bomber", "pizza_beef", new List<string> { "beef" }));
        pizzas.Add(createPizza("Zola the Gorgon", "pizza_cheese", new List<string> { "cheese" }));
        pizzas.Add(createPizza("Pineapple?!", "pizza_pineapple", new List<string> { "pineapple" }));
        pizzas.Add(createPizza("Pizza Uranus", "pizza_planet", new List<string> { "planet" }));
        pizzas.Add(createPizza("Cookie Clicker", "pizza_cookie", new List<string> { "cookie" }));
        pizzas.Add(createPizza("Star Wars", "pizza_star", new List<string> { "star" }));
        pizzas.Add(createPizza("Ze German", "pizza_sausage", new List<string> { "sausage" }));
        pizzas.Add(createPizza("Pizza Magicka", "pizza_magic", new List<string> { "magic" }));


        return pizzas;
    }

    private Pizza createPizza(string name, string textureName, List<string> ingredientNames)
    {
        Pizza pizza = new Pizza(name);
        pizza.texture = Resources.Load<Texture2D>("Pizzas/"+textureName);
        Debug.Log("texture " + pizza.texture);
        foreach (Ingredient ingredient in ingredients)
        {
            if (ingredientNames.Contains(ingredient.name))
            {
                pizza.ingredients.Add(ingredient);
            }
        }
        return pizza;
    }

    private void spawnFriend()
    {
        Vector3 position = new Vector3(Random.Range(-1.5f, 2.5f), 1.0f, Random.Range(-0.5f, -2.0f));
        Quaternion rotation = Quaternion.Euler(0.0f, Random.Range(150.0f, 210.0f), 0.0f);
        GameObject prefab = friendPrefabs[friends.Count];
        GameObject gameObject = Instantiate(prefab, position, rotation);
        GameObject overlay = Instantiate(friendOverlay, gameObject.transform);
        overlay.GetComponentInChildren<Text>().text = prefab.name;
        Friend friend = new Friend();

        friend.requirement = availableRequirements[Random.Range(0, availableRequirements.Count)];
        availableRequirements.Remove(friend.requirement);
        gameObject.GetComponent<FriendScript>().setFriend(friend);

        friendObjects.Add(gameObject);
        friends.Add(friend);
    }

    private List<Pizza> neededPizzas()
    {
        List<Ingredient> needed = new List<Ingredient>();
        foreach (Friend friend in friends)
        {
            needed.Add(friend.requirement);
        }

        foreach (Restaurant rest in restaurants)
        {
            foreach (Pizza pizza in rest.pizzas)
            {
                needed.Remove(pizza.ingredients[0]);
            }
        }

        List<Pizza> neededPizzas = new List<Pizza>();

        foreach (Ingredient ing in needed) {
            foreach (Pizza pizza in pizzas)
            {
                if (pizza.ingredients.Contains(ing))
                {
                    neededPizzas.Add(pizza);
                }
            }
        }
        return neededPizzas;
        

    }

    private void createRestaurant()
    {
        List<Pizza> pizzas = neededPizzas();
        int randomCount = friends.Count - pizzas.Count;
        for (int p = 0; p < randomCount; p++)
        {
            int pizzaStart = Random.Range(0, this.pizzas.Count);
            for (int offset = 0; offset < this.pizzas.Count; offset++)
            {
                Pizza pizza = this.pizzas[pizzaStart + offset];
                if (!pizzas.Contains(pizza))
                {
                    pizzas.Add(pizza);
                    break;
                }
            }
        }
        string name = availableRestaurantNames[Random.Range(0, availableRestaurantNames.Count)];
        availableRestaurantNames.Remove(name);
        Restaurant restaurant = new Restaurant(name, pizzas);
        restaurants.Add(restaurant);
        phoneManager.addRestaurant(restaurant);
    }

    private void extendMenus()
    {

        var shuffled = Utils.shuffleList<Restaurant>(restaurants);

        foreach (Restaurant restaurant in shuffled)
        {
            if (neededPizzas().Count > 0)
            {
                restaurant.pizzas.Add(neededPizzas()[0]);
            }
            else
            {
                int pizzaStart = Random.Range(0, this.pizzas.Count);
                for (int offset = 0; offset < this.pizzas.Count; offset++)
                {
                    Pizza pizza = this.pizzas[pizzaStart + offset];
                    if (!restaurant.pizzas.Contains(pizza))
                    {
                        restaurant.pizzas.Add(pizza);
                        break;
                    }
                }
            }
            
        }
    }

    private void nextLevel()
    {
        if (currentLevel >= 12)
        {
            SceneManager.LoadScene("WinScreen");
        }

        int levelState = (currentLevel % 3);
        switch (levelState)
        {
            case 0:
                spawnFriend();
                break;
            case 1:
                extendMenus();
                break;
            case 2:
                createRestaurant();
                break;
        }
        currentLevel++;
    }



    public void OrderAtRestaurant(Restaurant restaurant) {

        foreach (GameObject friendObject in friendObjects)
        {
            Friend friend = friendObject.GetComponent<FriendScript>().friend;
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
                var main = friendObject.GetComponent<ParticleSystem>().main;
                main.startColor = Color.green;
                friendObject.GetComponent<ParticleSystem>().Play();
                alterFriendSatisfaction(friend, satisfiedBonus);
            }
            else
            {
                var main = friendObject.GetComponent<ParticleSystem>().main;
                main.startColor = Color.red;
                friendObject.GetComponent<ParticleSystem>().Play();
                alterFriendSatisfaction(friend, -dissatisfiedMalus);
            }
        }

        nextLevel();

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
    public Texture texture;
    public List<Ingredient> ingredients;

    public Pizza(string name)
    {
        this.name = name;
        this.ingredients = new List<Ingredient>();
    }

}

public class Ingredient
{
    public string name;
    public Texture texture;
}

public class Utils
{

    public static List<T> shuffleList<T>(List<T> source)
    {
        var shuffled = new List<T>(source);
        shuffled.Sort((a, b) => 1 - 2 * Random.Range(0, 1));
        return shuffled;
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