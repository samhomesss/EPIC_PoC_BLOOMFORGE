using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SkillNode", menuName = "SkillTree/SkillNode")]
public class SkillNodeSO : ScriptableObject
{
    public string skillName;
    public bool unlocked;
    public bool isTrunk = false;
    public bool isPruned = false;

    public List<SkillNodeSO> children = new();
    public List<SkillNodeSO> parents = new();

    public List<SkillNodeSO> GetAllDescendants()
    {
        List<SkillNodeSO> result = new();
        void Traverse(SkillNodeSO current)
        {
            foreach (var child in current.children)
            {
                result.Add(child);
                Traverse(child);
            }
        }
        Traverse(this);
        return result;
    }
}
