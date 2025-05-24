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

    public void Setup(SkillNodeSO node, TreeManager manager)
    {
        skillNode = node;
        treeManager = manager;

        nameText.text = node.skillName;
        iconImage.sprite = node.icon;
        unlockButton.interactable = !node.unlocked;

        unlockButton.onClick.RemoveAllListeners();
        unlockButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        treeManager.TryUnlock(skillNode);
        Setup(skillNode, treeManager); // 다시 UI 반영
    }
}
