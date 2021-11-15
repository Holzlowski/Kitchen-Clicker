using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private int slotsHit;
    public Pizza pizza;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ingredient")) {
            Debug.Log("nice!");
            slotsHit++;

            if (slotsHit == pizza.slotCount) {
                Debug.Log("Congratulations!");
            }
        } else {
            Destroy(other);
        }
    }
}
