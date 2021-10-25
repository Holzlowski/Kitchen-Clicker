using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaTwister : MonoBehaviour
{
    [SerializeField] GameObject ingredientPrefab;

    [SerializeField] float distanceToCamera = 0.85f;
    [SerializeField] float degreesPerSecond = 20f;
    [SerializeField] float fallingDistance = 0.5f;


    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        mainCamera.transform.eulerAngles = new Vector3(90, 0, 0);
        mainCamera.transform.position = new Vector3(0, distanceToCamera, 0);

        ObjectToCenter();
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = new Vector3(0, distanceToCamera, 0);

        PizzaRotation();

        IngredientSpawnWithClick();

    }

    void ObjectToCenter (){
        transform.position = Vector3.zero;
    }

    void PizzaRotation(){
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
    }

    void IngredientSpawnWithClick(){

        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit)){
                if(hit.transform.tag == "Pizza"){
                    Vector3 fallingPosition = new Vector3 (hit.point.x, hit.point.y + fallingDistance, hit.point.z);
                    Instantiate(ingredientPrefab, fallingPosition, Quaternion.identity);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Ingredient"){
            other.transform.parent = transform;
        }
    }
}
