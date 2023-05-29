public interface ISubject
{
    public void NotifyObservers(GameEventEnum gameEvent, params object[] args);
    public void AddObserver(IObserver observer);
    public void RemoveObserver(IObserver observer);
}
