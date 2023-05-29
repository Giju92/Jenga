using System;
using UnityEngine;
using System.Collections.Concurrent;

public class BlockPoolManager : BaseSingleton<BlockPoolManager>, IObjectPool<IObjectPoolItem>
{
    [SerializeField]
    Block blockPrefab;

    private ConcurrentBag<IObjectPoolItem> _objects;
    private Func<IObjectPoolItem> _objectGenerator;
    private Action<IObjectPoolItem> _onReturnFunction;

    void Start()
    {
        SetBaseObjectPool(
            () => Instantiate(blockPrefab, transform),
            (t) => t.Reset());
    }

    public void SetBaseObjectPool(Func<IObjectPoolItem> objectGenerator, Action<IObjectPoolItem> onReturnFunction)
    {
        _objectGenerator = objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));
        _onReturnFunction = onReturnFunction ?? throw new ArgumentNullException(nameof(onReturnFunction));
        _objects = new ConcurrentBag<IObjectPoolItem>();
    }

    public IObjectPoolItem Get()
    {
        if (_objects.TryTake(out IObjectPoolItem item))
            return item;

        IObjectPoolItem newObject = _objectGenerator();
        newObject.SetPool(this);
        return newObject;
    }

    public void Return(IObjectPoolItem item)
    {
        _onReturnFunction(item);
        _objects.Add(item);
    }
}
