using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillNodeUI : MonoBehaviour
{
    [Header("UI Components")]
    public Image iconImage;
    public TMP_Text nameText;
    public Button unlockButton;

    [HideInInspector] public SkillNodeSO skillNode;
    private TreeManager treeManager;

    // SkillNodeUI.cs → Setup 함수
    public void Setup(SkillNodeSO node, TreeManager manager)
    {
        skillNode = node;
        treeManager = manager;

        nameText.text = node.skillName;
        iconImage.sprite = node.icon;

        // ✅ 테스트용으로 버튼 항상 클릭 가능하게
        unlockButton.interactable = true;

        unlockButton.onClick.RemoveAllListeners();
        unlockButton.onClick.AddListener(OnClick);
    }


    public void OnClick()
    {
        // 🎯 테스트용으로 GrowthPoint 증가
        treeManager.currentGrowthPoint += 1;
        Debug.Log($"GrowthPoint: {treeManager.currentGrowthPoint}");

        // 기존 TryUnlock 유지
        treeManager.TryUnlock(skillNode);

        // UI 갱신
        Setup(skillNode, treeManager);
    }
}
