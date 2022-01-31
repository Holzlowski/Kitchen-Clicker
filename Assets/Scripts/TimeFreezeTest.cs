using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator Test(){
        Debug.Log("I started " + Time.time);
        yield return new WaitForSeconds(10f);
        Debug.Log("I ended " + Time.time);
    }
}
