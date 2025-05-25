// SkillNodeUI.cs
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillNodeUI : MonoBehaviour
{
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

        float alpha = node.unlocked ? 1f : 0.3f;
        if (iconImage != null)
        {
            var c = iconImage.color;
            c.a = alpha;
            iconImage.color = c;
        }
        if (nameText != null)
        {
            var c = nameText.color;
            c.a = alpha;
            nameText.color = c;
        }

        unlockButton.interactable = node.unlocked;
        unlockButton.onClick.RemoveAllListeners();
        unlockButton.onClick.AddListener(() =>
        {
            treeManager.WaterTree();
        });
    }
}
