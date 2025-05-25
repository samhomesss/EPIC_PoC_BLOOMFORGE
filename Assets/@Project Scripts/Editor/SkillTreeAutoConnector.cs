using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SkillTreeAutoConnector : EditorWindow
{
    private string nodeFolderPath = "Assets/SkillTreeGenerated";
    private SkillTreeSO skillTreeAsset;

    [MenuItem("Tools/🌳 랜덤 스킬 트리 생성기")]
    public static void ShowWindow()
    {
        GetWindow<SkillTreeAutoConnector>("랜덤 SkillTree 생성기");
    }

    private void OnGUI()
    {
        GUILayout.Label("📂 SkillNodeSO 자동 연결", EditorStyles.boldLabel);
        nodeFolderPath = EditorGUILayout.TextField("노드 폴더 경로", nodeFolderPath);
        skillTreeAsset = (SkillTreeSO)EditorGUILayout.ObjectField("SkillTreeSO", skillTreeAsset, typeof(SkillTreeSO), false);

        if (GUILayout.Button("🔥 랜덤으로 노드 연결하기"))
        {
            ConnectNodes();
        }
    }

    private void ConnectNodes()
    {
        var guids = AssetDatabase.FindAssets("t:SkillNodeSO", new[] { nodeFolderPath });
        var nodes = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<SkillNodeSO>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(node => node != null)
            .ToList();

        if (nodes.Count == 0)
        {
            Debug.LogWarning("SkillNodeSO를 찾을 수 없습니다.");
            return;
        }

        // 모든 노드 초기화
        foreach (var node in nodes)
        {
            node.children = new List<SkillNodeSO>();
            node.unlocked = false;
            node.isTrunk = false;
            EditorUtility.SetDirty(node);
        }

        // Node_0을 root로 사용
        SkillNodeSO root = nodes.FirstOrDefault(n => n.skillName == "Node_0");
        if (root == null)
        {
            Debug.LogError("Node_0을 찾을 수 없습니다.");
            return;
        }

        List<SkillNodeSO> available = new List<SkillNodeSO>(nodes);
        available.Remove(root);

        Queue<SkillNodeSO> queue = new Queue<SkillNodeSO>();
        queue.Enqueue(root);

        int maxChildren = 2;

        while (queue.Count > 0 && available.Count > 0)
        {
            SkillNodeSO parent = queue.Dequeue();

            int childCount = Random.Range(1, Mathf.Min(maxChildren, available.Count) + 1);
            for (int i = 0; i < childCount; i++)
            {
                var child = available[0];
                available.RemoveAt(0);
                parent.children.Add(child);
                queue.Enqueue(child);
            }

            EditorUtility.SetDirty(parent);
        }

        if (skillTreeAsset != null)
        {
            skillTreeAsset.rootNode = root;
            EditorUtility.SetDirty(skillTreeAsset);
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"[✅ 연결 완료] 총 {nodes.Count}개 노드를 랜덤 연결했습니다.");
    }
}
