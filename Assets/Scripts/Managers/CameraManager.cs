using Cinemachine;
using UnityEngine;

public class CameraManager : BaseObserver
{

    [SerializeField]
    CinemachineFreeLook cam;

    protected override void OnStackSelectedEvent(StackInfo info) 
    {
        cam.Follow = info.spawnPoint;
        cam.LookAt = info.spawnPoint;
    }


}
