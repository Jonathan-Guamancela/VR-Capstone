using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText; // The actual question
        public string[] answers;    // List of possible answers
        public int correctAnswerIndex; // The correct answer's index (0-3)
    }

    public Question[] questions; // Array of all questions
    public TextMeshProUGUI questionText; // UI text that displays the question
    public Button[] answerButtons; // Array of buttons for the answers
    public TextMeshProUGUI[] answerTexts; // Text for each answer button

    private int currentQuestionIndex = 0;
    private bool isAnsweringQuestion = true; // Flag to check if the player is answering a question

    void Start()
    {
        ShuffleQuestions(); // Shuffle questions at the start
        DisplayQuestion();
        SetDefaultButtonStyles(); // Set the default button styles when the game starts
    }

    // Shuffle the order of the questions
    void ShuffleQuestions()
    {
        questions = questions.OrderBy(a => Random.Range(0f, 1f)).ToArray();
    }

    // Display the current question and shuffle the answers
    void DisplayQuestion()
    {
        if (questions.Length == 0)
        {
            Debug.LogError("No questions available.");
            return;
        }

        Question q = questions[currentQuestionIndex];
        questionText.text = q.questionText;

        // Shuffle the answers
        ShuffleAnswers(q);

        // Assign shuffled answers to buttons
        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].text = q.answers[i];
        }

        // Reset button colors to default (white) and black text
        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white; // White color for the button
            btn.GetComponentInChildren<TextMeshProUGUI>().color = Color.black; // Black text
            btn.interactable = true; // Ensure buttons are interactable again
        }

        // Re-enable the answering flag to allow selection
        isAnsweringQuestion = true;
    }

    // Shuffle the answers of a given question
    void ShuffleAnswers(Question question)
    {
        // Shuffle the answers
        int correctIndex = question.correctAnswerIndex;
        question.answers = question.answers.OrderBy(a => Random.Range(0f, 1f)).ToArray();

        // Find the new index of the correct answer
        for (int i = 0; i < question.answers.Length; i++)
        {
            if (question.answers[i] == question.answers[correctIndex])
            {
                question.correctAnswerIndex = i;
                break;
            }
        }
    }

    void SetDefaultButtonStyles()
    {
        // Set default styles for buttons
        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white; // White color for the button
            btn.GetComponentInChildren<TextMeshProUGUI>().color = Color.black; // Black text
        }
    }

    public void CheckAnswer(int index)
    {
        Debug.Log("Button clicked with index: " + index); // Debugging to see if this is triggered

        if (!isAnsweringQuestion) return; // If we are not answering the question, do nothing

        Question q = questions[currentQuestionIndex];

        // Reset button colors (optional)
        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white; // Reset to white
            btn.GetComponentInChildren<TextMeshProUGUI>().color = Color.black; // Reset text to black
        }

        // Check if the answer is correct
        if (index == q.correctAnswerIndex)
        {
            Debug.Log("Correct Answer");
            // Change the button color to green for the correct answer
            answerButtons[index].GetComponent<Image>().color = Color.green;

            // Move to the next question after a brief delay
            Invoke("NextQuestion", 1f);
        }
        else
        {
            Debug.Log("Wrong Answer");
            // Change the wrong answer button color to a dampened gray/red
            answerButtons[index].GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f); // Light red to indicate wrong answer

            // Optionally, add feedback for wrong answer (e.g., "Try again" message)
            Debug.Log("Wrong answer, try again!");

            // Prevent moving to the next question until the correct answer is selected
            isAnsweringQuestion = false;
        }
    }

    void NextQuestion()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            // End of quiz or any other action you want (like showing score)
            Debug.Log("Quiz Complete!");
        }
    }
}