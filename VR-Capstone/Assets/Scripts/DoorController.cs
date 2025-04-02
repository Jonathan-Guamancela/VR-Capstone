using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool isUnlocked = false;

    public void UnlockDoor()
    {
        isUnlocked = true;
        Debug.Log("Door is now unlocked!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isUnlocked)
        {
            gameObject.SetActive(false); // Hide door to simulate opening
        }
    }
}

