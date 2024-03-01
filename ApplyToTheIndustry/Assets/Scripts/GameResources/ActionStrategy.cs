public enum ActionType
{
    Nothing,
    Study
}

public static class ActionFactory
{
    public static IActionStrategy MakeAction(ActionType type)
    {
        switch (type)
        {
            case ActionType.Study:
                return new StudyAction();
            default:
                return new NullAction();
        }
    }
}

public interface IActionStrategy
{
    public void Perform();
}

public class NullAction : IActionStrategy
{
    public void Perform()
    {
        //lol. Lmao even.
        return;
    }
}

public class StudyAction : IActionStrategy
{
    public void Perform()
    {
        PlayerSkillsManager psm = ServiceLocator.Instance.GetService<PlayerSkillsManager>();
        psm.AdvanceCourse();
    }
}