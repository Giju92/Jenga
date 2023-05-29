/// <summary>
/// "Subject" in a Observer pattern environment 
/// </summary>
public class ApplicationController : BaseSingleton<ApplicationController>
{
    BaseSubject subject;
    public BaseSubject Notifier { get { return subject; } }

    private void Awake()
    {
        SetInstance(this);
        subject = gameObject.AddComponent<BaseSubject>();
    }
}
