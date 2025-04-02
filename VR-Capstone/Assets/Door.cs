using UnityEngine;
using UnityEngine.UI;

public class DoorQuiz : MonoBehaviour
{
    public GameObject questionPanel; // Assign a UI panel with a Text and Buttons
    public Text questionText;
    public Button[] answerButtons; // Assign multiple buttons for choices
    public string correctAnswer = "Unity"; // Change this to your desired answer

    private void Start()
    {
        questionPanel.SetActive(false);
        foreach (Button btn in answerButtons)
        {
            btn.onClick.AddListener(() => CheckAnswer(btn.GetComponentInChildren<Text>().text));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questionPanel.SetActive(true);
            questionText.text = "What engine is used to create this game?"; // Change the question here
            SetupAnswers();
        }
    }

    private void SetupAnswers()
    {
        string[] choices = { "Unreal", "Unity", "Godot", "CryEngine" };
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = choices[i];
        }
    }

    private void CheckAnswer(string selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            Destroy(gameObject); // Remove the door
            questionPanel.SetActive(false);
        }
        else
        {
            questionPanel.SetActive(false); // Hide panel on incorrect answer
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questionPanel.SetActive(false);
        }
    }
}
