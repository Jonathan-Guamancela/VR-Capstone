using UnityEngine;
using TMPro;

public class QuestionTrigger : MonoBehaviour
{
    public GameObject questionUI;  // Assign the UI panel with the question
    public TMP_Text questionText;
    public string question = "What is 2 + 2?";
    public string[] answers = { "3", "4", "5" };
    public int correctAnswerIndex = 1;

    private bool isPlayerNearby = false;

    void Start()
    {
        questionUI.SetActive(false);
        questionText.text = question;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questionUI.SetActive(true);
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questionUI.SetActive(false);
            isPlayerNearby = false;
        }
    }

    public void AnswerQuestion(int index)
    {
        if (index == correctAnswerIndex)
        {
            Debug.Log("Correct Answer!");
            questionUI.SetActive(false);
            GameManager.Instance.QuestionAnswered(); // Notify game manager
        }
        else
        {
            Debug.Log("Wrong Answer! Try Again.");
        }
    }
}

