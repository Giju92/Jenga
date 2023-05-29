using UnityEngine;

public interface IObserver 
{
    public void OnNotify(GameEventEnum gameEvent, params object[] args);
    public void Notify(GameEventEnum gameEvent, params object[] args);
    
}
