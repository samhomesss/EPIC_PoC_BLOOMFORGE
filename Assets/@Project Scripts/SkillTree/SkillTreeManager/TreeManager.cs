using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public SkillTreeSO skillTree;
    public SkillTreeUI skillTreeUI;
    public int currentGrowthPoint;

    public void TryUnlock(SkillNodeSO node)
    {
        if (!node.unlocked && currentGrowthPoint >= node.requiredGrowthPoint)
        {
            node.unlocked = true;
            Debug.Log($"[Unlocked] {node.skillName}");

            RectTransform parent = skillTreeUI.GetParentRect(node);
            int depth = skillTreeUI.GetDepthOfParent(node);

            skillTreeUI.CreateSkillUI(node, depth, parent);
        }

        if (node.unlocked)
        {
            foreach (var child in node.nextSkills)
            {
                TryUnlock(child);
            }
        }
    }
}