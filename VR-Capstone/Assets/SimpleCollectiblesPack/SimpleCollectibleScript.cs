using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import for TextMeshPro

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour
{
    public enum CollectibleTypes { NoType, Type1, Type2, Type3, Type4, Type5 };

    public CollectibleTypes CollectibleType;
    public bool rotate;
    public float rotationSpeed;
    public AudioClip collectSound;
    public GameObject collectEffect;

    public TextMeshProUGUI collectibleText; // Reference to UI Text
    public float textDisplayDuration = 2f; // How long the text stays visible

    void Update()
    {
        if (rotate)
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with collectible");
            Collect();
        }
    }


    public void Collect()
    {
        if (collectSound)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        if (collectEffect)
            Instantiate(collectEffect, transform.position, Quaternion.identity);

        ShowCollectibleText(); // Show text on screen

        Destroy(gameObject);
    }

    void ShowCollectibleText()
    {
        if (collectibleText)
        {
            collectibleText.text = "Collected: " + CollectibleType.ToString();
            collectibleText.gameObject.SetActive(true);
            StartCoroutine(HideTextAfterDelay());
        }
    }

    IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(textDisplayDuration);
        collectibleText.gameObject.SetActive(false);
    }
}


