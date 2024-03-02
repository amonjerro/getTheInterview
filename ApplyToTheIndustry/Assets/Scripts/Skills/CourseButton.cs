using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CourseButton : MonoBehaviour
{
    // Public fields
    public CourseObject data;
    public TextMeshProUGUI courseName;
    private CourseDetailUI ui;

    public void SetCourseDetailUI(CourseDetailUI target)
    {
        ui = target;
    }

    public void UpdateUI()
    {
        courseName.text = data.courseName;
    }

    /// <summary>
    /// Event call for setting job manager's current job posting based
    /// on posting attached to this button's container
    /// </summary>
    public void OnClick()
    {
        ui.currentCourse = data;
        ui.UpdateUI();
    }
}
