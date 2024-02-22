public enum ResourceTypes
{
    Time,
    Money
}

public abstract class Resource
{
    public int Time {  get; set; }
    public int Money { get; set;}

    public abstract bool IsThereEnough();
    public abstract void Spend();
}