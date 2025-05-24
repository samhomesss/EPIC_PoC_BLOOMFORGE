using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillTree/SkillTree")]
public class SkillTreeSO : ScriptableObject
{
    public SkillNodeSO rootSkill; // 시작 스킬

    public List<SkillNodeSO> AllNodes()
    {
        HashSet<SkillNodeSO> visited = new();
        Queue<SkillNodeSO> queue = new();
        List<SkillNodeSO> all = new();

        queue.Enqueue(rootSkill);
        visited.Add(rootSkill);

        while (queue.Count > 0)
        {
            SkillNodeSO current = queue.Dequeue();
            all.Add(current);

            foreach (var next in current.nextSkills)
            {
                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    queue.Enqueue(next);
                }
            }
        }

        return all;
    }
}
