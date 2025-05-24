using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillTree/SkillNode")]
public class SkillNodeSO : ScriptableObject
{
    public string skillName;
    public string description;
    public Sprite icon;
    public bool unlocked;
    public int requiredGrowthPoint;

    public List<SkillNodeSO> nextSkills;
}
