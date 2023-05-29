using System.Collections.Generic;
using UnityEngine;

public class BaseSubject : MonoBehaviour, ISubject
{
    protected List<IObserver> _observers;

    void Awake()
    {
        _observers = new List<IObserver>();
    }

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void NotifyObservers(GameEventEnum gameEvent, params object[] args)
    {
        foreach (IObserver observer in _observers)
            observer.OnNotify(gameEvent, args);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }
       
}
