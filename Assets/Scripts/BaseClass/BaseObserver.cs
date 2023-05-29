using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseObserver : MonoBehaviour, IObserver
{
    static ISubject subject => ApplicationController.GetInstance()?.Notifier;

    void Start()
    {
        subject.AddObserver(this);
        Initialize();
    }

    void OnDisable()
    {
        subject?.RemoveObserver(this);
    }

    public void Notify(GameEventEnum gameEvent, params object[] args)
    {
        subject.NotifyObservers(gameEvent, args);
    }

    protected virtual void Initialize() 
    {
    }

    protected virtual void OnStartEvent() { }
    protected virtual void OnNetworkResponsReadyEvent(string response) { }
    protected virtual void OnStackSelectedEvent(StackInfo info) { }
    protected virtual void OnExamSelectedEvent(NetworkExam exam) { }
    protected virtual void OnTestStackEvent(GradeEnum grade) { }


    public void OnNotify(GameEventEnum gameEvent, params object[] args)
    {
        switch (gameEvent)
        {
            case GameEventEnum.START:
                OnStartEvent();
                break;
            case GameEventEnum.NETWORK_RESPONSE_READY:
                OnNetworkResponsReadyEvent((string) args[0]);
                break;
            case GameEventEnum.EXAM_SELECTED:
                OnExamSelectedEvent((NetworkExam) args[0]);
                break;
            case GameEventEnum.STACK_SELECTED:
                OnStackSelectedEvent((StackInfo) args[0]);
                break;
            case GameEventEnum.TEST_STACK:
                OnTestStackEvent((GradeEnum) args[0]);
                break;
        }
    }
        
}
