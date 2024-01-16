using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flyweight/PickupableItem Settings")]
public class PickupableItemSettings : FlyweightSettings
{
    public float despawnDelay = 15f;
    public Vector3 spawnPos = new Vector3(0f, 20f, 0f);

    public override Flyweight Create()
    {
        GameObject go = Instantiate(prefab);
        go.SetActive(false);
        go.name = prefab.name;

        var flyweight = go.AddComponent<PickupableItem>();
        flyweight.settings = this;

        return flyweight;
    }

}
