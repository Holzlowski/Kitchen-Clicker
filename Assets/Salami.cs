using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salami : MonoBehaviour
{
    private Collider collider;
    private Rigidbody rigidbody;
    bool isOnPizza = false;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {

        if(other.transform.tag.Equals("Pizza")){
            isOnPizza = true;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        if(other.transform.tag.Equals("Ingredient") && isOnPizza == false){
            Destroy(gameObject);
        }
    }
}
