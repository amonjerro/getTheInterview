public enum ActionType
{
    Nothing,
    Study,
    Connection
}

public static class ActionFactory
{
    public static IActionStrategy MakeAction(ActionType type)
    {
        switch (type)
        {
            case ActionType.Study:
                return new StudyAction();
            case ActionType.Connection:
                return new ConnectionAction();
            default:
                return new NullAction();
        }
    }
}

public interface IActionStrategy
{
    public void Perform(ActionCost cost);
}

public class NullAction : IActionStrategy
{
    public void Perform(ActionCost cost)
    {
        //lol. Lmao even.
        ResourceManager rm = ServiceLocator.Instance.GetService<ResourceManager>();
        rm.ManageCost(cost);
        return;
    }
}

public class StudyAction : IActionStrategy
{
    public void Perform(ActionCost cost)
    {
        ResourceManager rm = ServiceLocator.Instance.GetService<ResourceManager>();
        PlayerSkillsManager psm = ServiceLocator.Instance.GetService<PlayerSkillsManager>();
        if (psm.isCourseBooked())
        {
            rm.ManageCost(cost);
        }
        psm.AdvanceCourse();   
    }
}

public class ConnectionAction : IActionStrategy
{
    public void Perform(ActionCost cost)
    {
        ResourceManager rm = ServiceLocator.Instance.GetService<ResourceManager>();
        ServiceLocator.Instance.GetService<ConnectionMaker>().AddConnectionToPool();
        rm.ManageCost(cost);
    }
}