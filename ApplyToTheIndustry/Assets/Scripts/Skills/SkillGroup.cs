using System;
using System.Collections.Generic;

[Serializable]
public struct SkillGroup
{
    public Skill programming;
    public Skill design;
    public Skill graphic_art;
    public Skill production;
    public Skill sound_and_music;
    public Skill leadership;

    public SkillGroup(float p, float d, float ga, float pr, float s, float ld)
    {
        programming = new Skill("Programming", p, SkillType.Programming);
        design = new Skill("Design", d, SkillType.Design);
        graphic_art = new Skill("Aesthetics & Art", ga, SkillType.Graphics);
        production = new Skill("Production", pr, SkillType.Production);
        sound_and_music = new Skill("Sound & Music", s, SkillType.Sound);
        leadership = new Skill("Leadership", ld, SkillType.Leadership);
    }

    public static SkillGroup operator +(SkillGroup a, SkillGroup b)
    {
        SkillGroup returnable = new SkillGroup();
        returnable.programming = a.programming + b.programming;
        returnable.graphic_art = a.graphic_art + b.graphic_art;
        returnable.design = a.design + b.design;
        returnable.sound_and_music = a.sound_and_music + b.sound_and_music;
        returnable.production = a.production + b.production;
        returnable.leadership = a.leadership + b.leadership;
        return returnable;
    }

    public List<Skill> ListSkills()
    {
        List<Skill> listOfSkills = new List<Skill>
        {
            programming,
            design,
            graphic_art,
            sound_and_music,
            production,
            leadership
        };
        return listOfSkills;
    }

}
