using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour
{
    [Header("Skill Tree Data")]
    public SkillTreeSO treeData;
    public TreeManager treeManager;

    [Header("Prefabs & UI")]
    public GameObject skillUIPrefab;
    public GameObject lineImagePrefab;
    public Transform skillRootParent;

    [Header("Spacing")]
    public float xSpacing = 300f;
    public float ySpacing = 200f;

    private Dictionary<int, int> depthToIndex = new();
    private Dictionary<SkillNodeSO, RectTransform> nodeToUI = new();
    private Dictionary<SkillNodeSO, int> nodeDepthMap = new();

    private void Start()
    {
        treeManager.skillTreeUI = this;

        if (treeData == null || treeData.rootSkill == null)
        {
            Debug.LogError("SkillTreeSO 또는 rootSkill이 설정되지 않았습니다.");
            return;
        }

        foreach (var node in treeData.AllNodes())
            node.unlocked = false;

        treeData.rootSkill.unlocked = true;

        CreateSkillUI(treeData.rootSkill, 0, null);
    }

    public void CreateSkillUI(SkillNodeSO node, int depth, RectTransform parentUI)
    {
        if (!depthToIndex.ContainsKey(depth))
            depthToIndex[depth] = 0;

        int index = depthToIndex[depth];
        depthToIndex[depth]++;

        Vector3 position = new Vector3(index * xSpacing, -depth * ySpacing, 0);

        GameObject go = Instantiate(skillUIPrefab, skillRootParent);
        RectTransform currentUI = go.GetComponent<RectTransform>();
        currentUI.anchoredPosition = position;

        SkillNodeUI ui = go.GetComponent<SkillNodeUI>();
        ui.Setup(node, treeManager);

        nodeToUI[node] = currentUI;
        nodeDepthMap[node] = depth;

        if (parentUI != null)
            CreateLineBetween(parentUI, currentUI);

        foreach (SkillNodeSO child in node.nextSkills)
        {
            if (!child.unlocked) continue;
            CreateSkillUI(child, depth + 1, currentUI);
        }
    }

    public void CreateLineBetween(RectTransform from, RectTransform to)
    {
        GameObject lineGO = Instantiate(lineImagePrefab, skillRootParent);
        lineGO.transform.SetAsFirstSibling();
        UILineConnector connector = lineGO.AddComponent<UILineConnector>();
        connector.from = from;
        connector.to = to;
        connector.lineRect = lineGO.GetComponent<RectTransform>();
    }

    public RectTransform GetParentRect(SkillNodeSO node)
    {
        foreach (var kv in nodeToUI)
        {
            if (kv.Key.nextSkills.Contains(node))
                return kv.Value;
        }
        return null;
    }

    public int GetDepthOfParent(SkillNodeSO node)
    {
        foreach (var kv in nodeToUI)
        {
            if (kv.Key.nextSkills.Contains(node))
                return nodeDepthMap.ContainsKey(kv.Key) ? nodeDepthMap[kv.Key] + 1 : 1;
        }
        return 0;
    }
}