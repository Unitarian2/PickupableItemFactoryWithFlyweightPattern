using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableSpawner : MonoBehaviour
{
    public List<FlyweightSettings> flyweightSettings;
    bool isSpawnActive;
    [SerializeField] private GameObject pickupableParent;
    // Start is called before the first frame update
    void Start()
    {
        StartSpawning();
    }

    private void StartSpawning()
    {
        isSpawnActive = true;
        SpawnSinglePickupable();
    }

    private void SpawnSinglePickupable()
    {
        if(isSpawnActive) StartCoroutine(PickupableSpawnProcess());
    }

    IEnumerator PickupableSpawnProcess()
    {
        yield return new WaitForSeconds(2);
        int chosenIndex = UnityEngine.Random.Range(0,flyweightSettings.Count);

        var flyweight = PickupableFactory.Spawn(flyweightSettings[chosenIndex]);
        flyweight.transform.parent = pickupableParent.transform;
        flyweight.transform.position = flyweightSettings[chosenIndex].spawnPos;
        SpawnSinglePickupable();
    }
}
