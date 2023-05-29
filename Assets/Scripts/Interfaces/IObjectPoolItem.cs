public interface IObjectPoolItem
{
    public void ReturnToPool();
    public void SetPool(IObjectPool<IObjectPoolItem> pool);
    public void Reset();
}
