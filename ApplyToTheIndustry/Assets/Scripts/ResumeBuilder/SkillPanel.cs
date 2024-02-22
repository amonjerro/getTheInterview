using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* CLASS: SkillPanel
 * Used for managing pool of options for Resume Builder
 */
public class SkillPanel : MonoBehaviour
{
    // Public fields
    public SkillContainer test1;
    public SkillContainer test2;

    /// <summary>
    /// Called upon activation of UI panel
    /// </summary>
    void OnEnable()
    {
        // Update each container's text
        test1.UpdateUIText();
        test2.UpdateUIText();
    }
}
