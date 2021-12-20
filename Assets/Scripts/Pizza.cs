using System.Collections.Generic;
using MiniGames;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    [SerializeField] private float spawnRadius = 1;
    [SerializeField] private int slotCount = 5;
    [SerializeField] private float minDist;
    [SerializeField] private int maxAttempts;
    [SerializeField] private Slot slotPrefab;
    [SerializeField] private List<Vector3> slotCoords = new List<Vector3>();
    [SerializeField] private List<Slot> slots = new List<Slot>();
    [SerializeField] private Recipe recipe;

    private int slotHits = 0;

    // Start is called before the first frame update
    private void Start() => SlotSpawns();

    private void Update()
    {
        if (slotHits != slotCount)
            return;

        Debug.Log("finished");
        Wallet.AddMoney(recipe.Bonus);
        // TODO: Fix looping for infinite bonus
    }

    public void AddHit()
    {
        slotHits++;
    }

    private void SlotSpawns()
    {
        for (int i = 0; i < slotCount; i++)
        {
            var spawnPos = default(Vector3);
            var attempts = 0;
            while (attempts < maxAttempts)
            {
                var circlePos = Random.insideUnitCircle * spawnRadius;
                spawnPos = new Vector3(circlePos.x, 0.175f, circlePos.y);
                var ok = true;
                foreach (var slot in slotCoords)
                {
                    var dist = (slot - spawnPos).magnitude;
                    if (dist < minDist)
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    break;
                }

                attempts++;
            }

            slotCoords.Add(spawnPos);
            var slotInstance = Instantiate(slotPrefab, spawnPos, Quaternion.identity, transform);
            slotInstance.Initialize(recipe.GetRandomIngredient());
            slotInstance.gameObject.SetActive(true);
            slots.Add(slotInstance);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}