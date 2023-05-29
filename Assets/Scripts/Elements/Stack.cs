using System.Collections.Generic;
using UnityEngine;

public class Stack : BaseObserver
{
    private StackInfo _info;

    public void Set(StackInfo info, List<NetworkExam> exams)
    {
        _info = info;
        transform.parent = _info.spawnPoint;
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        transform.name = $"Stack {_info.grade.GetString()}";

        SpawnPieces(exams);
    }

    private void SpawnPieces(List<NetworkExam> exams)
    {
        for (int i = 0; i < exams.Count; i++)
        {
            var block = BlockPoolManager.GetInstance().Get() as Block;
            block.Set(exams[i], i, _info.grade, transform);
        }
    }

}
