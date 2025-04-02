using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KnowledgeCheckManager : MonoBehaviour
{
    public Text timerText;
    public GameObject knowledgeCheckPanel;  // The UI panel that displays the question
    public Text questionText;  // Text field for the question
    public Button[] answerButtons;  // Buttons for the answers
    private float timer = 0f;
    private float knowledgeCheckTime = 10f;  // Time until the knowledge check

    private bool isKnowledgeCheckActive = false;
    private string correctAnswer = "Correct Answer";  // Replace with your correct answer logic

    void Update()
    {
        if (!isKnowledgeCheckActive)
        {
            timer += Time.deltaTime;
            timerText.text = Mathf.Ceil(knowledgeCheckTime - timer).ToString();

            // When timer reaches the check time, trigger the knowledge check
            if (timer >= knowledgeCheckTime)
            {
                TriggerKnowledgeCheck();
            }
        }
    }

    void TriggerKnowledgeCheck()
    {
        isKnowledgeCheckActive = true;
        timer = 0f;  // Reset timer for next potential check

        // Show the knowledge check UI and ask a random question
        questionText.text = "Why is consistency and standards an important heuristic for accessibility?";  // Replace with dynamic question logic

        knowledgeCheckPanel.SetActive(true);  // Enable the UI panel for the question
    }

    public void CheckAnswer(string selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            // Correct answer, proceed
            knowledgeCheckPanel.SetActive(false);
            isKnowledgeCheckActive = false;
            Debug.Log("Correct!");
        }
        else
        {
            // Wrong answer, let the player try again or give feedback
            Debug.Log("Incorrect. Try again!");
        }
    }
}

