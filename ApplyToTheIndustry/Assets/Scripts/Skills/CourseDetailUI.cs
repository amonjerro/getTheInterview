using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CourseDetailUI : MonoBehaviour
{
    public TextMeshProUGUI courseTitle;
    public TextMeshProUGUI courseDescription;
    public TextMeshProUGUI duration;
    public List<UIBar> skillBars;
    public List<SkillType> skillBarOrder;
    public float maximumSkillGain;
    public CourseObject currentCourse;
    private bool setupComplete = false;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateTextWithChild(TextMeshProUGUI target, string value)
    {
        TextMeshProUGUI child = target.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        target.text = value;
        child.text = value;
    }

    // Method for updating UI
    public void UpdateUI()
    {
        // Update UI text based on incoming course
        if (currentCourse != null)
        {
            int studyRate = ServiceLocator.Instance.GetService<PlayerSkillsManager>().GetStudyRate();
            UpdateTextWithChild(courseTitle, currentCourse.courseName);
            UpdateTextWithChild(courseDescription, currentCourse.description);
            float sessions = Mathf.Ceil(currentCourse.data.timeCost.value / (float) studyRate);
            string session_string = sessions == 1 ? " session" : " sessions";
            UpdateTextWithChild(duration, sessions.ToString() + session_string);
            
            // Update the skill bars
            for (int i = 0; i < skillBarOrder.Count; i++)
            {
                if (!setupComplete)
                {
                    skillBars[i].Setup();
                }
                float proportion = currentCourse.data.reward.GetSkill(skillBarOrder[i]).value / maximumSkillGain;
                skillBars[i].SetFullness(proportion);
            }
            setupComplete = true;
        }

        
    }
}