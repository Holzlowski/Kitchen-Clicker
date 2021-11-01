using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salami : MonoBehaviour
{
    Collider collider;
    Rigidbody rigidbody;
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

            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        if(other.transform.tag == "Ingredient"){
            Destroy(gameObject);
        }
    }
}
