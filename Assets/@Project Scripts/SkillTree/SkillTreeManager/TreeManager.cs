// SkillTreeManager.cs (통합 리팩토링)
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreeManager : MonoBehaviour
{
    [Header("References")]
    public SkillTreeSO skillTree;
    public RectTransform treeRoot;
    public GameObject skillNodePrefab;
    public ScrollRect scrollRect;
    public float spacingX = 140f;
    public float spacingY = 200f;
    public int waterCount = 1;

    private Dictionary<SkillNodeSO, Vector2Int> positions = new();
    private HashSet<Vector2Int> usedPositions = new();
    private int rootX;

    void Awake()
    {
        if (skillTree == null || skillTree.rootNode == null) return;

        LockAllNodes();
        skillTree.rootNode.unlocked = true;
        SetLongestTrunk(skillTree.rootNode);
    }

    void Start()
    {
        GenerateTree();
        StartCoroutine(ScrollToBottom());
    }

    void LockAllNodes()
    {
        foreach (var node in skillTree.allNodes)
        {
            node.unlocked = false;
        }
    }

    void SetLongestTrunk(SkillNodeSO node)
    {
        var trunk = GetLongestPath(node);
        foreach (var n in skillTree.allNodes) n.isTrunk = false;
        foreach (var n in trunk) n.isTrunk = true;
    }

    List<SkillNodeSO> GetLongestPath(SkillNodeSO node)
    {
        List<SkillNodeSO> longest = new();
        List<SkillNodeSO> current = new();

        void DFS(SkillNodeSO n)
        {
            current.Add(n);
            if (n.children.Count == 0)
            {
                if (current.Count > longest.Count)
                    longest = new List<SkillNodeSO>(current);
            }
            else
            {
                foreach (var child in n.children)
                    DFS(child);
            }
            current.RemoveAt(current.Count - 1);
        }

        DFS(node);
        return longest;
    }

    public void GenerateTree()
    {
        positions.Clear();
        usedPositions.Clear();

        AssignPositions(skillTree.rootNode, 0, 0);
        rootX = positions[skillTree.rootNode].x;

        foreach (var kvp in positions)
        {
            SkillNodeSO node = kvp.Key;
            Vector2Int pos = kvp.Value;

            GameObject go = Instantiate(skillNodePrefab, treeRoot);
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2((pos.x - rootX) * spacingX, -pos.y * spacingY);

            go.GetComponent<SkillNodeUI>().Setup(node, this);
        }
    }

    void AssignPositions(SkillNodeSO node, int depth, int x)
    {
        if (positions.ContainsKey(node)) return;

        int safeY = depth;
        while (usedPositions.Contains(new Vector2Int(x, safeY)))
            safeY++;

        positions[node] = new Vector2Int(x, safeY);
        usedPositions.Add(positions[node]);

        if (node.isTrunk && node.children.Count == 1 && node.children[0].isTrunk)
        {
            AssignPositions(node.children[0], safeY + 1, x);
            return;
        }

        int mid = node.children.Count / 2;
        for (int i = 0; i < node.children.Count; i++)
        {
            var child = node.children[i];
            int offset = i - mid;
            if (node.children.Count % 2 == 0 && i >= mid) offset++;
            int childX = x + offset;

            AssignPositions(child, safeY + 1, childX);
        }
    }

    public void WaterTree()
    {
        if (skillTree == null || skillTree.rootNode == null) return;
        int remaining = waterCount;

        foreach (var node in skillTree.allNodes)
        {
            if (!node.unlocked) continue;
            List<SkillNodeSO> candidates = node.children.FindAll(c =>
                !c.unlocked && c.parents.TrueForAll(p => p.unlocked));

            while (candidates.Count > 0 && remaining > 0)
            {
                SkillNodeSO pick = candidates[Random.Range(0, candidates.Count)];
                pick.unlocked = true;
                candidates.Remove(pick);
                remaining--;
            }
        }

        UpdateVisualStates();
    }

    public void UpdateVisualStates()
    {
        foreach (var ui in treeRoot.GetComponentsInChildren<SkillNodeUI>())
        {
            ui.Setup(ui.skillNode, this);
        }
    }

    System.Collections.IEnumerator ScrollToBottom()
    {
        yield return null;
        if (scrollRect != null)
            scrollRect.verticalNormalizedPosition = 0f;
    }
}