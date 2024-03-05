using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;


public class Feedback : MonoBehaviour
{
    private struct FeedbackTexts
    {
        public string companyName;
        public string feedbackMessage;
        public string positionName;
        public string buttonText;
        public string connectionMessage;

        public FeedbackTexts(string cN, string pN, string fM, string cM, string bT)
        {
            companyName = cN;
            positionName = pN;
            feedbackMessage = fM;
            buttonText = bT;
            connectionMessage = cM;
        }
    }


    public TextMeshProUGUI feedbackMessage;
    public TextMeshProUGUI companyName;
    public TextMeshProUGUI positionName;
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI connectionFeedback;
    public Grader grader;
    private FeedbackData feedbackData;
    private List<FeedbackTexts> feedbackItems = new List<FeedbackTexts>();
    private int messageIndex;

    void OnEnable()
    {
        messageIndex = 0;
        feedbackItems.Clear();
        companyName.text = "";
        positionName.text = "";
        feedbackData = grader.GetFeedback();

        if (feedbackData.validApplications.Count == 0)
        {
            feedbackMessage.text = "You have been charged for your weekly costs of rent, food and utilities.";
            companyName.text = "";
            positionName.text = "You have not applied to any positions, you have no pending messages.";
            buttonText.text = "Move on to next week";
            connectionFeedback.text = "";
        } 
        else if(feedbackData.validApplications.Count >= 0 && !feedbackData.validApplications.ContainsValue(true))
        {
            feedbackMessage.text = "You have not received any responses from your applications.";
            companyName.text = "";
            positionName.text = "";
            buttonText.text = "Next Message";
            connectionFeedback.text = "";
            feedbackItems.Add(new FeedbackTexts(
            "", "",
            "You have been charged for your weekly costs of rent, food and utilities.",
            "", "Move on to next week"
            ));
        }
        else
        {
            feedbackMessage.text = "You have pending responses from your applications";
            companyName.text = "";
            positionName.text = "";
            buttonText.text = "Next Message";
            connectionFeedback.text = "";
            PreProcessFeedback();
        }
        
    }

    public void NextMessage()
    {
        if (messageIndex == feedbackItems.Count)
        {
            ServiceLocator.Instance.GetService<ResourceManager>().EndOfTheWeek();
            grader.ResetFeedback();
            ServiceLocator.Instance.GetService<UIGeneralManager>().MoveToMainScreen();
            return;
        }
        feedbackMessage.text = feedbackItems[messageIndex].feedbackMessage;
        companyName.text = feedbackItems[messageIndex].companyName;
        positionName.text = feedbackItems[messageIndex].positionName;
        buttonText.text = feedbackItems[messageIndex].buttonText;
        connectionFeedback.text = feedbackItems[messageIndex].connectionMessage;
        messageIndex++;
    }

    private void PreProcessFeedback()
    {
        foreach(KeyValuePair<string, bool> kvp in feedbackData.validApplications)
        {   
            if (!kvp.Value)
            {
                continue;
            }

            // Otherwise you were not ghosted by the company
            feedbackItems.Add(new FeedbackTexts(kvp.Key, feedbackData.positionByCompany[kvp.Key],
                feedbackData.companyResponses[kvp.Key], "", "Next Message"
                ));

            // Check for connection feedback
            if (feedbackData.connectionFeedback.ContainsKey(kvp.Key))
            {
                feedbackItems.Add(new FeedbackTexts("", "", "", feedbackData.connectionFeedback[kvp.Key], "Next Message"));
            }
        }

        feedbackItems.Add(new FeedbackTexts(
        "", "",
        "You have been charged for your weekly costs of rent, food and utilities.",
        "", "Move on to next week"
        ));
    }

}

