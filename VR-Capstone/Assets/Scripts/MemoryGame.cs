using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MemoryGame : MonoBehaviour
{
    // Card properties
    public int cardId;
    public GameObject frontFace;
    public GameObject backFace;
    private bool isFlipped = false;
    private bool isMatched = false;

    // Game management properties
    private static MemoryGame firstCard = null;
    private static MemoryGame secondCard = null;
    private static bool canFlip = true;

    private void Start()
    {
        ShowBack();
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!isFlipped && canFlip && !isMatched)
        {
            FlipCard();
            CheckMatch(this);
        }
    }

    public void FlipCard()
    {
        isFlipped = !isFlipped;
        RotateCard();
    }

    private void RotateCard()
    {
        if (isFlipped)
        {
            frontFace.SetActive(true);
            backFace.SetActive(false);
            StartCoroutine(RotateSmoothly(180));
        }
        else
        {
            frontFace.SetActive(false);
            backFace.SetActive(true);
            StartCoroutine(RotateSmoothly(0));
        }
    }

    private IEnumerator RotateSmoothly(float targetAngle)
    {
        float duration = 0.5f;
        float startAngle = transform.rotation.eulerAngles.y;
        float elapsed = 0;

        while (elapsed < duration)
        {
            float angle = Mathf.Lerp(startAngle, targetAngle, elapsed / duration);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, targetAngle, 0);
    }

    private void ShowFront()
    {
        frontFace.SetActive(true);
        backFace.SetActive(false);
    }

    private void ShowBack()
    {
        frontFace.SetActive(false);
        backFace.SetActive(true);
    }

    private void CheckMatch(MemoryGame clickedCard)
    {
        if (firstCard == null)
        {
            firstCard = clickedCard;
        }
        else if (secondCard == null)
        {
            secondCard = clickedCard;
            StartCoroutine(CheckForMatch());
        }
    }

    private IEnumerator CheckForMatch()
    {
        canFlip = false;
        yield return new WaitForSeconds(1);

        if (firstCard != null && secondCard != null)
        {
            if (firstCard.cardId == secondCard.cardId)
            {
                Debug.Log("Match found!");
                firstCard.isMatched = true;
                secondCard.isMatched = true;
            }
            else
            {
                Debug.Log("No match, flipping back.");
                firstCard.FlipCard();
                secondCard.FlipCard();
            }

            // Reset the static references for the next pair
            firstCard = null;
            secondCard = null;
        }

        canFlip = true;
    }
}
