using UnityEngine.UIElements.Experimental;

public static class UtilityFunctions
{
    public static int ZeroOrMore(int value)
    {
        if ( value  < 0)
        {
            return 0;
        }
        return value;
    }
}