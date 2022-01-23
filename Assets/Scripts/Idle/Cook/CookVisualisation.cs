using System.Collections;
using System.Collections.Generic;
using PizzaGame;
using UnityEngine;
using Util;

public class CookVisualisation : MonoBehaviour
{
    [SerializeField] private GameObject pizza;
    [SerializeField] private Transform throwPlace;
    private Stack<Ingredient> rightIngredients = new Stack<Ingredient>();
    [SerializeField] private List<ParticleSystem> particleEffects = new List<ParticleSystem>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //RotatePizza();
    }

    //private void RotatePizza() => pizza.transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime);

    public void pizzaComplete()
    {
        while (rightIngredients.Count > 0)
        {
            Ingredient ingredient = rightIngredients.Pop();
            if(ingredient != null){
                Destroy(ingredient.gameObject);
            }
        }
        particleEffects.Random().Play();
    }

    public void showHit(IngredientType ingredient)
    {
        Vector3 randomOffset = new Vector3(Random.Range(-0.75f,0.75f),0,Random.Range(-0.75f,0.75f));
        Ingredient rightIngredient = Instantiate(ingredient.Prefab, throwPlace.position + randomOffset, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        rightIngredient.gameObject.tag = "Untagged";
        rightIngredients.Push(rightIngredient);
    }

    public void missHit(IngredientType ingredient)
    {
        Ingredient flyingIngredient = Instantiate(ingredient.Prefab, throwPlace.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        flyingIngredient.GetComponent<Rigidbody>().AddForce(Random.Range(-5, 5), Random.Range(0, 0.3f), -5, ForceMode.Impulse);
        Destroy(flyingIngredient.gameObject, 60);
    }
}
