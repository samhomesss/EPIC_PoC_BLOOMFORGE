using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public SkillTreeSO skillTree;
    public int currentGrowthPoint;

    public void TryUnlock(SkillNodeSO node)
    {
        if (!node.unlocked && currentGrowthPoint >= node.requiredGrowthPoint)
        {
            node.unlocked = true;
            Debug.Log($"[Unlocked] {node.skillName}");
        }

        foreach (var child in node.nextSkills)
        {
            TryUnlock(child); // 다음 스킬도 재귀로 확인
        }
    }

    private void Start()
    {
        TryUnlock(skillTree.rootSkill);
    }
}
