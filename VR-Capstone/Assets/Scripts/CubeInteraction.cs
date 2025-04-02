using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class CubeInteraction : MonoBehaviour
{
    public Transform playerHand;
    public float moveSpeed = 5f;
    public float returnSpeed = 2f;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isPickedUp = false;
    private static GameObject firstPickedCube = null;
    private static GameObject secondPickedCube = null;
    private bool isReturning = false;

    private XRGrabInteractable grabInteractable;

    void Start()
    {
        // Store the initial position and rotation at the start
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Set up the XR Grab Interactable
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void Update()
    {
        // Move the cube to the player's hand if picked up
        if (isPickedUp)
        {
            MoveTo(playerHand.position);
        }
        // Move the cube back to its original position and rotation if returning
        else if (isReturning)
        {
            MoveTo(initialPosition);
            RotateTo(initialRotation);

            // Stop returning when position and rotation are close enough
            if (Vector3.Distance(transform.position, initialPosition) < 0.001f &&
                Quaternion.Angle(transform.rotation, initialRotation) < 0.1f)
            {
                transform.position = initialPosition;
                transform.rotation = initialRotation;
                isReturning = false;
            }
        }
    }

    // Move the cube towards the target position
    private void MoveTo(Vector3 target)
    {
        float speed = isReturning ? returnSpeed : moveSpeed;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    // Rotate the cube towards the target rotation
    private void RotateTo(Quaternion targetRotation)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, returnSpeed * 100 * Time.deltaTime);
    }

    // Called when the cube is grabbed
    private void OnGrab(SelectEnterEventArgs args)
    {
        if (firstPickedCube == null)
        {
            firstPickedCube = gameObject;
            isPickedUp = true;
        }
        else if (secondPickedCube == null && gameObject != firstPickedCube)
        {
            secondPickedCube = gameObject;
            isPickedUp = true;
            StartCoroutine(CheckMatch());
        }
    }

    // Called when the cube is released
    private void OnRelease(SelectExitEventArgs args)
    {
        isPickedUp = false;
        isReturning = true;

        if (firstPickedCube == gameObject)
        {
            firstPickedCube = null;
        }
        else if (secondPickedCube == gameObject)
        {
            secondPickedCube = null;
        }
    }

    // Coroutine to check if the two selected cubes match
    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1f);

        if (firstPickedCube != null && secondPickedCube != null)
        {
            if (firstPickedCube.CompareTag(secondPickedCube.tag))
            {
                Debug.Log("Matched: " + firstPickedCube.tag);
                Destroy(firstPickedCube);
                Destroy(secondPickedCube);
            }
            else
            {
                Debug.Log("No match: " + firstPickedCube.tag + " and " + secondPickedCube.tag);
                firstPickedCube.GetComponent<CubeInteraction>().ReturnToPosition();
                secondPickedCube.GetComponent<CubeInteraction>().ReturnToPosition();
            }

            firstPickedCube = null;
            secondPickedCube = null;
        }
    }

    // Method to initiate the return to the initial position and rotation
    public void ReturnToPosition()
    {
        isPickedUp = false;
        isReturning = true;
    }
}
