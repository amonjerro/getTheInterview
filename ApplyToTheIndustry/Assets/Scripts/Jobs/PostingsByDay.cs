using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PostingByDay")]
public class PostingsByDay : ScriptableObject
{
    public JobPosting[] posts;
    public int day;
}
