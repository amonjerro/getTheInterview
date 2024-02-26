using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GradingProfile")]
public class GradingProfile : ScriptableObject
{
    public SkillGroup skillGroup;
    public SkillType[] expectedSkillOrdering;

    // Reset will be called when the scriptable object is created. This essentially creates default values for
    // this type of scriptable object.
    private void Reset()
    {
        skillGroup.programming.name = "Programming";
        skillGroup.programming.skillType = SkillType.Programming;

        skillGroup.design.name = "Design";
        skillGroup.design.skillType = SkillType.Design;

        skillGroup.graphic_art.name = "Graphics and Art";
        skillGroup.graphic_art.skillType = SkillType.Graphics;

        skillGroup.sound_and_music.name = "Sound and Music";
        skillGroup.sound_and_music.skillType = SkillType.Sound;

        skillGroup.production.name = "Production";
        skillGroup.production.skillType = SkillType.Production;

        skillGroup.leadership.name = "Leadership";
        skillGroup.leadership.skillType = SkillType.Leadership;
    }
}