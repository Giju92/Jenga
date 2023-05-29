using UnityEngine;

// Used to allow unselect an exam
public class Ground : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        ApplicationController.GetInstance().Notifier.NotifyObservers(GameEventEnum.EXAM_SELECTED, new object[] { null });
    }
}
