using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Course")]
public class CourseObject : ScriptableObject
{
	public CourseData data;
    public string courseName;
    [TextArea]
    public string description;

    private void Reset()
    {
        data.reward.programming.name = "Programming";
        data.reward.programming.skillType = SkillType.Programming;
        data.reward.design.name = "Design";
        data.reward.design.skillType = SkillType.Design;
        data.reward.sound_and_music.name = "Sound and Music";
        data.reward.sound_and_music.skillType = SkillType.Sound;
        data.reward.graphic_art.name = "Graphics and Art";
        data.reward.graphic_art.skillType = SkillType.Graphics;
        data.reward.production.name = "Production";
        data.reward.production.skillType = SkillType.Production;
        data.reward.leadership.name = "Leadership";
        data.reward.leadership.skillType = SkillType.Leadership;
    }
}

[CreateAssetMenu(menuName = "ScriptableObject/CourseGrouping")]
public class CourseGroupingByDay : ScriptableObject
{
    public CourseObject[] courses;
}