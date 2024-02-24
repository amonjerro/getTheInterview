using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* CLASS: Company
 * Scriptable Object used for storing
 * all information on any company
 */
[CreateAssetMenu(menuName = "ScriptableObjects/Company")]
public class Company : ScriptableObject
{
    // Data that the player sees
    public string compName;
    public Sprite logo;
}
