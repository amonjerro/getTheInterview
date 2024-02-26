using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Feedback : MonoBehaviour
{
    public TextMeshProUGUI TextOutput;
    Grader feedback;

    // Start is called before the first frame update
    void Start()
    {
        feedback = ServiceLocator.Instance.GetService<Grader>();
        TextOutput.text = feedback.GetFeedback();
    }

}

