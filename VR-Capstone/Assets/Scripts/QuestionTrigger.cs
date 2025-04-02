using UnityEngine;
using TMPro;

public class QuestionTrigger : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject questionUI;
    public TMP_Text questionText;
    public TMP_Text[] answerTexts; // Text for each button

    [Header("Question Data")]
    [TextArea]
    public string question;
    public string[] answers;
    public int correctAnswerIndex;

    private bool hasBeenAnswered = false;

    void Start()
    {
        questionUI.SetActive(false);
        SetupQuestion();
    }

    void SetupQuestion()
    {
        questionText.text = question;
        for (int i = 0; i < answerTexts.Length; i++)
        {
            if (i < answers.Length)
                answerTexts[i].text = answers[i];
            else
                answerTexts[i].text = "";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenAnswered)
        {
            questionUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questionUI.SetActive(false);
        }
    }

    public void AnswerQuestion(int index)
    {
        if (index == correctAnswerIndex)
        {
            Debug.Log("Correct!");
            hasBeenAnswered = true;
            questionUI.SetActive(false);
            GameManager.Instance.QuestionAnswered();
        }
        else
        {
            Debug.Log("Wrong answer.");
        }
    }
}
