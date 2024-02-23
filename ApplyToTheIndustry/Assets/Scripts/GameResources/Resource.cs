using UnityEngine;
using System;

public enum ResourceTypes
{
    Time,
    Money
}

[Serializable]
public class Resource
{
    [SerializeField]
    public int value;

    public Resource(int value)
    {
        this.value = value;
    }
    public bool IsThereEnough(int cost)
    {
        return cost <= value;
    }

    public Resource Add(Resource otherResource) {
        return new Resource(value + otherResource.value);
    }

    public Resource Subtract(Resource otherResource)
    {

        return new Resource(value);
    }

    public static Resource operator +(Resource a, Resource b)
    {
        return a.Add(b);
    }

    public static Resource operator -(Resource a, Resource b)
    {
        return a.Subtract(b);
    }

}
