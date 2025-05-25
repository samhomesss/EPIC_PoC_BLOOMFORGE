using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SkillTree", menuName = "SkillTree/Tree")]
public class SkillTreeSO : ScriptableObject
{
    public SkillNodeSO rootNode;
    public List<SkillNodeSO> allNodes = new();
}
