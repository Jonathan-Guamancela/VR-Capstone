using UnityEngine;
using TMPro;  // Import TextMeshPro namespace
using System.Collections; // Required for IEnumerator




public class Collectible : MonoBehaviour
{
    public AudioClip collectSound; // Sound to play when collected
    public GameObject collectEffect; // Effect to instantiate when collected
    public TextMeshProUGUI collectibleText; // Reference to UI text

    private void Start()
    {
        if (collectibleText != null)
        {
            collectibleText.color = new Color(collectibleText.color.r, collectibleText.color.g, collectibleText.color.b, 0); // Start fully transparent
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if it's the player
        {
            Collect();
        }
    }

    private void Collect()
    {
        // Play collection sound if one is assigned
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        // Instantiate the collection effect at the collectible's position
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }

        // Show text on the screen
        if (collectibleText != null)
        {
            StartCoroutine(ShowText());
        }

        // Destroy the collectible after it's collected
        Destroy(gameObject);
    }

    private IEnumerator ShowText()
    {
        collectibleText.text = "Collected!";

        // Fade in
        float duration = 0.5f;
        float elapsed = 0f;
        Color originalColor = collectibleText.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            collectibleText.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0, 1, elapsed / duration));
            yield return null;
        }

        yield return new WaitForSeconds(1.5f); // Keep text visible for a while

        // Fade out
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            collectibleText.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, elapsed / duration));
            yield return null;
        }
    }
}
