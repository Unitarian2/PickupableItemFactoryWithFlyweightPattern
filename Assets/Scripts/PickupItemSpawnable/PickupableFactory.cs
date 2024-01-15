using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PickupableFactory : MonoBehaviour
{
    [SerializeField] bool collectionCheck = true;
    [SerializeField] int defaultCapacity = 10;
    [SerializeField] int maxPoolSize = 100;

    static PickupableFactory instance;
    readonly Dictionary<FlyweightType, IObjectPool<Flyweight>> pools = new();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Flyweight Spawn(FlyweightSettings s) => instance.GetRelatedPool(s)?.Get();
    public static void ReturnToPool(Flyweight f) => instance.GetRelatedPool(f.settings)?.Release(f);

    IObjectPool<Flyweight> GetRelatedPool(FlyweightSettings settings)
    {
        IObjectPool<Flyweight> pool;

        if (pools.TryGetValue(settings.type, out pool)) return pool;
        pool = new ObjectPool<Flyweight>(
            settings.Create,
            settings.OnGet,
            settings.OnRelease,
            settings.OnDestroyPoolObject,
            collectionCheck,
            defaultCapacity,
            maxPoolSize
            );

        pools.Add(settings.type, pool);
        return pool;
    }
}
