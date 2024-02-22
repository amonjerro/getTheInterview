using System;

public class Skill
{
    public string Name { get; protected set; }
    public float Value { get; set; }

    public Skill(string name, float value)
    {
        Name = name;
        Value = value;
    }

    public static Skill operator +(Skill a, Skill b)
    {
        if (a.Name != b.Name)
        {
            throw new ArgumentException("The two skills being added are not of the same type");
        }
        return new Skill(a.Name, a.Value + b.Value);
    }
}