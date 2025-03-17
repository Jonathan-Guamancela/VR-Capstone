using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int questionsAnswered = 0;
    public GameObject finalDoor; // Assign the locked door object

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void QuestionAnswered()
    {
        questionsAnswered++;
        if (questionsAnswered >= 4)
        {
            UnlockFinalDoor();
        }
    }

    void UnlockFinalDoor()
    {
        Debug.Log("Final Door Unlocked!");
        finalDoor.GetComponent<DoorController>().UnlockDoor();
    }
}

