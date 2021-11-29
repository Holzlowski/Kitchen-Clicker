using MiniGames;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Pizza pizza;

    private void OnTriggerEnter(Collider other)
    {
        var salami = other.GetComponent<Salami>();
        if (salami == null)
        {
            Destroy(other.gameObject);
            return;
        }

        if (salami.IsInPlace)
            return;

        Debug.Log("nice!");
        pizza.AddHit();
        salami.IsInPlace = true;
        Destroy(gameObject);
    }
}