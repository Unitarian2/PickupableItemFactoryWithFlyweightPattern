using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flyweight/Flyweight Settings")]
public class FlyweightSettings : ScriptableObject
{
    public FlyweightType type;
    public GameObject prefab;
    public float despawnDelay = 15f;
    public Vector3 spawnPos = new Vector3(0f,20f,0f);

    public Flyweight Create()
    {
        GameObject go = Instantiate(prefab);
        go.SetActive(false);
        go.name = prefab.name;

        var flyweight = go.AddComponent<Flyweight>();
        flyweight.settings = this;

        return flyweight;
    }
}

public enum FlyweightType
{
    Health,
    Mana
}
