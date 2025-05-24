using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillTree/SkillTree")]
public class SkillTreeSO : ScriptableObject
{
    public SkillNodeSO rootSkill; // 시작 스킬
}
