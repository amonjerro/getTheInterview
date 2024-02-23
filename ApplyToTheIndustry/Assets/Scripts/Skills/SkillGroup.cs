using System.Collections.Generic;
using UnityEngine;

public class SkillGroup: MonoBehaviour
{

    public Skill programming;
    public Skill design;
    public Skill graphic_art;
    public Skill production;
    public Skill sound_and_music;
    public Skill leadership;
    public static SkillGroup Instance;


private void Awake()
    {
        // Ensure there is only one instance of JobContainer
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Another instance of SkillGroup already exists. Destroying this one.");
            Destroy(gameObject);
        }
    }

    public SkillGroup()
    {
        programming = new Skill("Programming", 0);
        design = new Skill("Design", 0);
        graphic_art = new Skill("Aesthetics & Art", 0);
        production = new Skill("Production", 0);
        sound_and_music = new Skill("Sound & Music", 0);
        leadership = new Skill("Leadership", 0);
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
