using UnityEngine;

public class MatchQuestionHandler : MonoBehaviour
{
    public static MatchQuestionHandler Instance;

    [Header("References")]
    public QuizManager quizManager;
    public GameObject quizPanel;

    private bool isWaitingForAnswer = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        quizPanel.SetActive(false);
    }

    // Called when a match is found
    public void TriggerQuestion()
    {
        Debug.Log("Showing panel: " + quizPanel.name);
        quizPanel.SetActive(true);

        Debug.Log("TriggerQuestion called!");
        if (isWaitingForAnswer) return;

        isWaitingForAnswer = true;
        quizPanel.SetActive(true);
        quizManager.DisplayQuestion();
    }

    // Called after answering the question (correct or not)
    public void OnQuestionAnswered()
    {
        isWaitingForAnswer = false;
        quizPanel.SetActive(false);
    }

    // Used to block matching during quiz
    public bool IsWaiting()
    {
        return isWaitingForAnswer;
    }
}
