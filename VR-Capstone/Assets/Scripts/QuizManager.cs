using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using System;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;
    }

    public Question[] questions;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI[] answerTexts;
    public GameObject retryMenu;
    public TextMeshProUGUI scoreText;

    private Question currentQuestion;
    private int currentQuestionIndex = 0;
    private bool isAnsweringQuestion = true;

    private int score = 0;
    private int streak = 0;
    private int multiplier = 1;
    private int basePoints = 10;

    void Start()
    {
        ShuffleQuestions();
        DisplayQuestion();
        SetDefaultButtonStyles();
        retryMenu.SetActive(false);
    }

    void ShuffleQuestions()
    {
        System.Random rng = new System.Random();
        for (int i = questions.Length - 1; i > 0; i--)
        {
            int swapIndex = rng.Next(i + 1);
            var temp = questions[i];
            questions[i] = questions[swapIndex];
            questions[swapIndex] = temp;
        }
    }

public void DisplayQuestion()
{
    retryMenu.SetActive(false);

    questionText.gameObject.SetActive(true);
    foreach (Button btn in answerButtons)
    {
        btn.gameObject.SetActive(true);
    }

    if (questions.Length == 0)
    {
        Debug.LogError("No questions available.");
        return;
    }

    Question q = questions[currentQuestionIndex];
    questionText.text = q.questionText;

    ShuffleAnswers(q);

    for (int i = 0; i < answerTexts.Length; i++)
    {
        answerTexts[i].text = q.answers[i];
    }

    foreach (Button btn in answerButtons)
    {
        btn.GetComponent<Image>().color = Color.white;
        btn.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        btn.interactable = true;
    }

    isAnsweringQuestion = true;
}


    void ShuffleAnswers(Question question)
    {
        List<string> shuffledAnswers = question.answers.ToList();
        string correctAnswer = question.answers[question.correctAnswerIndex];
        shuffledAnswers = shuffledAnswers.OrderBy(a => UnityEngine.Random.value).ToList();
        question.answers = shuffledAnswers.ToArray();
        question.correctAnswerIndex = shuffledAnswers.IndexOf(correctAnswer);
    }

    void SetDefaultButtonStyles()
    {
        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white;
            btn.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
    }

    public void CheckAnswer(int index)
    {
        Debug.Log("Button clicked with index: " + index);
        if (!isAnsweringQuestion) return;

        Question q = questions[currentQuestionIndex];

        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white;
            btn.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }

        string selectedAnswer = answerButtons[index].GetComponentInChildren<TextMeshProUGUI>().text;
        string correctAnswer = q.answers[q.correctAnswerIndex];

        if (index == q.correctAnswerIndex)
        {
            Debug.Log("Correct Answer");
            answerButtons[index].GetComponent<Image>().color = Color.green;

            score++;
            UpdateScoreUI();
            streak++;
            if (streak >= 2) multiplier++;
            score += basePoints * multiplier;
            scoreText.text = "Score: " + score;

            if (HorseGameManager.hr_IsQuestioning)
            {
                BackToHRMenu();
                Invoke("BackToHRMenu", 1f);
            }
            else
            {
                Invoke("NextQuestion", 1f);
            }
        }
        else
        {
            Debug.Log("Wrong Answer");
            answerButtons[index].GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            score = 0;
            UpdateScoreUI();
            streak = 0;
            multiplier = 1;
            scoreText.text = "Score: " + score;

            questionText.gameObject.SetActive(false);
            foreach (Button btn in answerButtons)
            {
                btn.gameObject.SetActive(false);
            }

            retryMenu.SetActive(true);
        }
        MatchQuestionHandler.Instance.OnQuestionAnswered();
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
            Debug.Log("Quiz Complete!");
            ShuffleQuestions();
            currentQuestionIndex = 0;
            DisplayQuestion();
        }
    }

    public void RetryGame()
    {
        if (HorseGameManager.hr_IsQuestioning)
        {
            ShuffleQuestions();
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
        retryMenu.SetActive(false);
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void BackToHRMenu()
    {
        HorseGameManager.hr_InEndGameMenu = true;
        HorseGameManager.hr_IsQuestioning = false;
    }
}