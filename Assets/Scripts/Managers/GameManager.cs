using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : BaseObserver
{
    [SerializeField]
    private StackInfo[] stackInfos;

    [SerializeField]
    private Stack stackPrefab;

    private int currentStack = 1;

    protected override void OnNetworkResponsReadyEvent(string jsonResponse)
    {
        if (string.IsNullOrEmpty(jsonResponse))
        {
            Debug.LogError("EMPTY RESPONSE, SOMETHING WRONG HAPPEN");
            return;
        }

        var response = JsonUtility.FromJson<Response>("{\"exams\":" + jsonResponse + "}");

        for(int i = 0; i < stackInfos.Length; i++)
        {
            var stack = Instantiate(stackPrefab);
            stack.Set(stackInfos[i], GetOrderedExamsForGrade(response, stackInfos[i].grade.GetString()));
        }
    }

    // Ordering algorithm
    private List<NetworkExam> GetOrderedExamsForGrade(Response response, string grade)
    {
        return response.exams.Where(x => x.grade.Equals(grade)).
            OrderBy(x => x.domain).
            ThenBy(x => x.cluster).
            ThenBy(x => x.standardid).
            ToList();
    }

    public void SwitchNextStack()
    {
        currentStack++;
        currentStack = currentStack % stackInfos.Length;

        Notify(GameEventEnum.STACK_SELECTED, stackInfos[currentStack]);
    }

    public void SwitchPreviousStack()
    {
        currentStack--;
        if (currentStack < 0)
            currentStack = stackInfos.Length - 1;


        Notify(GameEventEnum.STACK_SELECTED, stackInfos[currentStack]);
    }

    public void TryStack()
    {
        Notify(GameEventEnum.TEST_STACK, stackInfos[currentStack].grade);
    }

}
