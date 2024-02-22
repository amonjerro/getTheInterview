public enum ResourceTypes
{
    Time,
    Money
}

public abstract class Resource
{
    public int Value {  get; set; }

    public abstract bool IsThereEnough(int cost);

    public abstract Resource Add(Resource otherResource);
    public abstract Resource Subtract(Resource otherResource);

    public static Resource operator +(Resource a, Resource b)
    {
        return a.Add(b);
    }

    public static Resource operator -(Resource a, Resource b)
    {
        return a.Subtract(b);
    }

}

public class Time : Resource
{
    public override bool IsThereEnough(int cost)
    {
        if (cost == 0) return true;

        if (cost > Value) return false;

        return true;
    }

    public override Resource Add(Resource otherResource)
    {
        Time other = otherResource as Time;
        Time output = new Time();
        output.Value = Value + other.Value;
        return output;
    }

    public override Resource Subtract(Resource otherResource)
    {
        Time other = otherResource as Time;
        Time output = new Time();
        output.Value = Value - other.Value;
        return output;
    }
}

public class Money : Resource
{
    public override bool IsThereEnough(int cost)
    {
        if (cost == 0) return true;

        if (cost > Value) return false;

        return true;
    }

    public override Resource Add(Resource otherResource)
    {
        Money other = otherResource as Money;
        Money output = new Money();
        output.Value = Value + other.Value;
        return output;
    }

    public override Resource Subtract(Resource otherResource)
    {
        Money other = otherResource as Money;
        Money output = new Money();
        output.Value = Value - other.Value;
        return output;
    }


}