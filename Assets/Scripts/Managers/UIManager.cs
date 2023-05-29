using TMPro;
using UnityEngine;

public class UIManager : BaseObserver
{
    [SerializeField]
    TextMeshProUGUI examText;

    [SerializeField]
    Animator animator;

    protected override void OnExamSelectedEvent(NetworkExam exam)
    {
        base.OnExamSelectedEvent(exam);
        
        if(exam == null)
        {
            animator.SetBool("Entry", false);
            animator.SetBool("Exit", true);
            return;
        }

        examText.text = $"{exam.grade}: {exam.domain} \n {exam.cluster} \n {exam.standardid}: {exam.standarddescription}";
        animator.SetBool("Exit", false);
        animator.SetBool("Entry", true);

    }
}