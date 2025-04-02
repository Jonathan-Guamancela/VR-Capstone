using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Exit()
    {
        Debug.Log("Exit button clicked!");

        // This will quit the application when running a build.
        Application.Quit();

        // If running in the Unity Editor, stop play mode.
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
