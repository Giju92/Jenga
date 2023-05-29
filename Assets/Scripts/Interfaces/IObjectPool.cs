public interface IObjectPool<T> where T : IObjectPoolItem
{
    public T Get();

    public void Return(T item);
    
}
