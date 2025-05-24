using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour
{
    [Header("Skill Tree Data")]
    public SkillTreeSO treeData;
    public TreeManager treeManager;

    [Header("Prefabs & UI")]
    public GameObject skillUIPrefab;      // 노드 UI 프리팹
    public GameObject lineImagePrefab;    // 선 UI 프리팹 (얇은 이미지)
    public Transform skillRootParent;     // 모든 노드/선을 담을 부모

    [Header("Spacing")]
    public float xSpacing = 300f; // 좌우 간격
    public float ySpacing = 200f; // 위아래 간격 (음수면 아래로 배치)

    private void Start()
    {
        if (treeData == null || treeData.rootSkill == null)
        {
            Debug.LogError("SkillTreeSO 또는 rootSkill이 설정되지 않았습니다.");
            return;
        }

        CreateSkillUI(treeData.rootSkill, 0, 0, null);
    }

    void CreateSkillUI(SkillNodeSO node, int depth, int index, RectTransform parentUI)
    {
        // 1. 위치 계산
        Vector3 position = new Vector3(index * xSpacing, -depth * ySpacing, 0);

        // 2. 스킬 노드 UI 생성
        GameObject go = Instantiate(skillUIPrefab, skillRootParent);
        RectTransform currentUI = go.GetComponent<RectTransform>();
        currentUI.anchoredPosition = position;

        // 3. 데이터 바인딩
        SkillNodeUI ui = go.GetComponent<SkillNodeUI>();
        ui.Setup(node, treeManager);

        // 4. 선 연결 (부모가 있다면)
        if (parentUI != null)
        {
            CreateLineBetween(parentUI, currentUI);
        }

        // 5. 자식 노드들 생성 (재귀)
        for (int i = 0; i < node.nextSkills.Count; i++)
        {
            CreateSkillUI(node.nextSkills[i], depth + 1, i, currentUI);
        }
    }

    public void CreateLineBetween(RectTransform from, RectTransform to)
    {
        GameObject lineGO = Instantiate(lineImagePrefab, skillRootParent);
        lineGO.transform.SetAsFirstSibling(); // 선을 가장 아래에 위치시킴

        UILineConnector connector = lineGO.AddComponent<UILineConnector>();
        connector.from = from;
        connector.to = to;
        connector.lineRect = lineGO.GetComponent<RectTransform>();
    }

}
