using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitButton : MonoBehaviour
{
    // Public fields
    public Resume resumeRef;

    /// <summary>
    /// Submits application to grader and resets
    /// resume on builder
    /// </summary>
    public void OnClick()
    {
        // Submit this application to the grader

        // Reset resume
        resumeRef.ResetResume();
    }
}
