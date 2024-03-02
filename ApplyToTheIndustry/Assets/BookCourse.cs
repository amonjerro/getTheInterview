using UnityEngine;

public class BookCourse : MonoBehaviour
{
    public void OnClick()
    {
        PlayerSkillsManager psm = ServiceLocator.Instance.GetService<PlayerSkillsManager>();
        CourseDetailUI cdui = GetComponentInParent<CourseDetailUI>();
        psm.SetBookedCourse(cdui.currentCourse.data);
    }
}
