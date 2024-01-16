using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableItem : Flyweight
{
    new PickupableItemSettings settings => (PickupableItemSettings) base.settings;

    private void OnEnable()
    {
        StartCoroutine(DespawnAfterDelay(settings.despawnDelay));
    }

    IEnumerator DespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PickupableFactory.ReturnToPool(this);
    }
}
