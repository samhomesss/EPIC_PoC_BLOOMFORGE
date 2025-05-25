using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour
{
    [Header("Tree Data")]
    public SkillTreeSO skillTree;
    public RectTransform treeRoot;
    public GameObject skillNodePrefab;
    public TreeManager treeManager;

    [Header("Spacing")]
    public float spacingX = 120f;
    public float spacingY = 200f;

    private Dictionary<SkillNodeSO, Vector2Int> positions = new();
    private HashSet<Vector2Int> occupied = new();

    private void Start()
    {
        if (skillTree == null || skillTree.rootNode == null)
        {
            Debug.LogError("🌲 SkillTreeUI: skillTree or rootNode is null");
            return;
        }

        positions.Clear();
        occupied.Clear();

        AssignPositions(skillTree.rootNode, 0, 0);

        int minX = int.MaxValue;
        foreach (var pos in positions.Values)
            if (pos.x < minX) minX = pos.x;

        foreach (var kvp in positions)
        {
            var node = kvp.Key;
            var gridPos = kvp.Value;

            GameObject go = Instantiate(skillNodePrefab, treeRoot);
            RectTransform rt = go.GetComponent<RectTransform>();

            float anchoredX = (gridPos.x - minX) * spacingX;
            float anchoredY = -gridPos.y * spacingY;
            rt.anchoredPosition = new Vector2(anchoredX, anchoredY);

            go.GetComponent<SkillNodeUI>().Setup(node, treeManager);
        }
    }

    private void AssignPositions(SkillNodeSO node, int depth, int x)
    {
        if (positions.ContainsKey(node)) return;

        Vector2Int pos = new(x, depth);
        while (occupied.Contains(pos))
        {
            pos.y--;
        }

        positions[node] = pos;
        occupied.Add(pos);

        if (node.children == null || node.children.Count == 0) return;

        int childCount = node.children.Count;
        int mid = childCount / 2;

        for (int i = 0; i < childCount; i++)
        {
            int offset = i - mid;
            if (childCount % 2 == 0 && i >= mid) offset++;

            int childX = node.isTrunk && node.children[i].isTrunk ? x : x + offset;
            AssignPositions(node.children[i], pos.y - 1, childX);
        }
    }

    public void UpdateVisualStates()
    {
        SkillNodeUI[] nodes = treeRoot.GetComponentsInChildren<SkillNodeUI>();
        foreach (var ui in nodes)
        {
            ui.Setup(ui.skillNode, treeManager);
        }
    }
}
