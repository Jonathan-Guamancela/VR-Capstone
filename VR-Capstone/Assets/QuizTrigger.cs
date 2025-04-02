using UnityEngine;
using UnityEngine.UI;

public class QuizTrigger : MonoBehaviour
{
    public GameObject quizPanel;  // Assign the UI panel in Inspector
    public Text questionText;
    public Button[] answerButtons;

    private string correctAnswer;

    private void Start()
    {
        quizPanel.SetActive(false); // Hide quiz at start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowQuiz();
        }
    }

    void ShowQuiz()
    {
        quizPanel.SetActive(true);
        Time.timeScale = 0; // Pause game

        questionText.text = "What is the capital of France?"; // Example
        correctAnswer = "Paris";

        string[] options = { "Paris", "London", "Berlin", "Madrid" };
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = options[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(options[i]));
        }
    }

    void CheckAnswer(string selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            Debug.Log("Correct! Proceed.");
        }
        else
        {
            Debug.Log("Wrong! Try again.");
            // Example: Teleport player back to a checkpoint
        }

        quizPanel.SetActive(false);
        Time.timeScale = 1; // Resume game
        Destroy(gameObject); // Remove trigger after use
    }
}

