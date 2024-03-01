using System;
using System.Collections.Generic;
using System.Runtime;

[Serializable]
public struct SkillGroup
{
    public Skill programming;
    public Skill design;
    public Skill graphic_art;
    public Skill production;
    public Skill sound_and_music;
    public Skill leadership;
    public Skill foreign_lang;

    public SkillGroup(int p, int d, int ga, int pr, int s, int ld, int fl)
    {
        programming = new Skill("Programming", p, SkillType.Programming);
        design = new Skill("Design", d, SkillType.Design);
        graphic_art = new Skill("Aesthetics & Art", ga, SkillType.Graphics);
        production = new Skill("Production", pr, SkillType.Production);
        sound_and_music = new Skill("Sound & Music", s, SkillType.Sound);
        leadership = new Skill("Leadership", ld, SkillType.Leadership);
        foreign_lang = new Skill("Foreign Lang", fl, SkillType.ForeignLang);
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
        returnable.foreign_lang = a.foreign_lang + b.foreign_lang;
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
            leadership,
            foreign_lang,
        };
        return listOfSkills;
    }

    public Skill GetSkill(SkillType type)
    {
        switch (type)
        {
            case SkillType.Design:
                return design;
            case SkillType.Graphics:
                return graphic_art;
            case SkillType.Sound:
                return sound_and_music;
            case SkillType.Production:
                return production;
            case SkillType.Leadership:
                return leadership;
            case SkillType.ForeignLang:
                return foreign_lang;
            default:
                return programming;
        }
    }

}
