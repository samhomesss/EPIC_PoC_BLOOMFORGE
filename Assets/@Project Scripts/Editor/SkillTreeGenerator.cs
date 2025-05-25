#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class SkillTreeGenerator
{
    [MenuItem("Tools/Generate Random Tree with 30 Nodes")]
    public static void GenerateTree()
    {
        string folderPath = "Assets/SkillTreeGenerated";
        if (!AssetDatabase.IsValidFolder(folderPath))
            AssetDatabase.CreateFolder("Assets", "SkillTreeGenerated");

        // 1. 트리 생성
        SkillTreeSO skillTree = ScriptableObject.CreateInstance<SkillTreeSO>();
        skillTree.allNodes = new List<SkillNodeSO>();

        // 2. 노드 30개 생성
        List<SkillNodeSO> nodeList = new();
        for (int i = 0; i < 30; i++)
        {
            SkillNodeSO node = ScriptableObject.CreateInstance<SkillNodeSO>();
            node.name = $"Node_{i}";
            node.skillName = node.name;
            node.unlocked = false;
            node.children = new List<SkillNodeSO>();
            node.parents = new List<SkillNodeSO>();
            node.isTrunk = false;

            nodeList.Add(node);
            AssetDatabase.CreateAsset(node, $"{folderPath}/Node_{i}.asset");
            skillTree.allNodes.Add(node);
        }

        // 3. 루트 설정
        SkillNodeSO root = nodeList[0];
        skillTree.rootNode = root;

        // 4. 무작위 가지 연결 (최대 자식 3개씩)
        System.Random rand = new System.Random();
        for (int i = 0; i < nodeList.Count; i++)
        {
            SkillNodeSO parent = nodeList[i];

            int childCount = rand.Next(1, 4); // 1~3
            for (int j = 0; j < childCount; j++)
            {
                List<SkillNodeSO> available = nodeList.FindAll(n =>
                    n != parent &&
                    n.parents.Count == 0 &&
                    n != root &&
                    !parent.children.Contains(n)
                );

                if (available.Count == 0) break;

                SkillNodeSO child = available[rand.Next(available.Count)];
                parent.children.Add(child);
                child.parents.Add(parent);
            }
        }

        // 5. 저장
        AssetDatabase.CreateAsset(skillTree, $"{folderPath}/BigSkillTree.asset");
        AssetDatabase.SaveAssets();

        Debug.Log("🌳 30개의 노드가 연결된 스킬 트리가 생성되었습니다!");
    }
}
#endif
