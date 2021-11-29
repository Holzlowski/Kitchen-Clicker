using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    [SerializeField] public int slotCount = 5;
    [SerializeField] private float minDist;
    [SerializeField] private int maxAttempts;
    [SerializeField] GameObject slotPrefab; // TODO add a specific type for this
    [SerializeField] List<Vector3> slotCoords = new List<Vector3>();
    [SerializeField] List<GameObject> slots = new List<GameObject>();

    private int slotHits = 0;

    // Start is called before the first frame update
    private void Start() => SlotSpawns();

    private void Update()
    {
        if (slotHits == slotCount)
        {
            Debug.Log("finished");
        }
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
                var circlePos = Random.insideUnitCircle * 0.25f;
                spawnPos = new Vector3(circlePos.x, 0.02f, circlePos.y);
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
            var slotGO = Instantiate(slotPrefab, spawnPos, Quaternion.identity, this.transform);
            slotGO.SetActive(true);
            slots.Add(slotGO);
        }
    }
}