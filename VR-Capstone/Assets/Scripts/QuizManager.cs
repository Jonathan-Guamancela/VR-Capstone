using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using Unity.VisualScripting; // Required for scene management

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
    public GameObject retryMenu;

    private int currentQuestionIndex = 0;
    private bool isAnsweringQuestion = true; // Flag to check if the player is answering a question

    private int score = 0; // Track score
    public TextMeshProUGUI scoreText; // Assign in Unity Inspector

    void Start()
    {
        ShuffleQuestions(); // Shuffle questions at the start
        DisplayQuestion();
        SetDefaultButtonStyles(); // Set the default button styles when the game starts
        retryMenu.SetActive(false);
    }

    // Shuffle the order of the questions
    void ShuffleQuestions()
    {
        questions = questions.OrderBy(a => Random.Range(0f, 1f)).ToArray();
    }

    // Display the current question and shuffle the answers
    void DisplayQuestion()
    {
        retryMenu.SetActive(false); // Hide retry menu when loading a new question
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

            score++; // Increase score
            UpdateScoreUI(); // Update score display

            if (HorseGameManager.hr_IsQuestioning)
            {
                //go back to menu in horse race game
                BackToHRMenu();
                Invoke("BackToHRMenu", 1f);
            }
            else
            {
                // Move to the next question after a brief delay
                Invoke("NextQuestion", 1f);
            }
                
        }
        else
        {
            Debug.Log("Wrong Answer");
            // Change the wrong answer button color to a dampened gray/red
            answerButtons[index].GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f); // Light red to indicate wrong answer

            score = 0; // Reset score
            UpdateScoreUI(); // Update score display

            questionText.gameObject.SetActive(false);
            foreach (Button btn in answerButtons)
            {
                btn.gameObject.SetActive(false);
            }

            // Show the retry menu
            retryMenu.SetActive(true);
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

    public void RetryGame()
    {
        if (HorseGameManager.hr_IsQuestioning)
        {
            ShuffleQuestions(); // Shuffle questions at the start
            DisplayQuestion();
            questionText.gameObject.SetActive(true);
            foreach (Button btn in answerButtons)
            {
                btn.gameObject.SetActive(true);
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ExitRetry()
    {
        retryMenu.SetActive(false); // Hide the menu, let them retry the same question
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }


    //---------------------------------------------------------------------------------------//
    //Horse Game Functions

    void BackToHRMenu()
    {
        HorseGameManager.hr_InEndGameMenu = true;
        HorseGameManager.hr_IsQuestioning = false;
    }

}