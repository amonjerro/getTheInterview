
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ApplicationResponseTexts")]
public class ApplicationResponseTexts : ScriptableObject
{
    [TextArea]
    public string[] responseCandidates;
}