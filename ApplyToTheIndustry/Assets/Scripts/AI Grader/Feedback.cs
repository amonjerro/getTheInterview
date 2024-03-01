using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Feedback : MonoBehaviour
{
    public TextMeshProUGUI feedbackMessage;
    public TextMeshProUGUI companyName;
    public TextMeshProUGUI positionName;
    public TextMeshProUGUI buttonText;
    public Grader grader;
    private int messageIndex = 0;


    void OnEnable()
    {
        messageIndex = 0;
        companyName.text = "";
        positionName.text = "";
        if (grader.companiesAppliedCount == 0)
        {
            
            positionName.text = "You have not applied to any positions, you have no pending messages.";
            ServiceLocator.Instance.GetService<ResourceManager>().EndOfTheWeek();
            feedbackMessage.text = "You have been charged for your weekly costs of rent, food and utilities.";
            buttonText.text = "Move on to next week";
            messageIndex = 1;
        } 
        else if(grader.feedback.Count == 0)
        {
            positionName.text = "You have not received any responses from your applications.";
            ServiceLocator.Instance.GetService<ResourceManager>().EndOfTheWeek();
            feedbackMessage.text = "You have been charged for your weekly costs of rent, food and utilities.";
            buttonText.text = "Move on to next week";
            messageIndex = 2;
        }
        else
        {
            feedbackMessage.text = "You have pending responses from your applications";
            buttonText.text = "Next Message";
        }
        
    }

    public void NextMessage()
    {
        if (messageIndex > grader.feedback.Count) {
            grader.ResetFeedback();
            ServiceLocator.Instance.GetService<UIGeneralManager>().MoveToMainScreen();
        } else if (messageIndex == grader.feedback.Count)
        {
            ServiceLocator.Instance.GetService<ResourceManager>().EndOfTheWeek();
            companyName.text = "";
            positionName.text = "";
            feedbackMessage.text = "You have been charged for your weekly costs of rent, food and utilities.";
            buttonText.text = "Move on to next week";
        } else 
        {
            feedbackMessage.text = grader.feedback[messageIndex];
            companyName.text = grader.companiesAppliedTo[messageIndex];
            positionName.text = grader.positionsAppliedTo[messageIndex];
        }
        messageIndex++;
    }

}

