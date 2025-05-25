using UnityEngine;
using UnityEngine.UI;

public class SkillNodeButton : MonoBehaviour
{
    public SkillNodeSO node;
    public TreeManager treeManager;

    //private Button _button;

    //private void Awake()
    //{
    //    _button = GetComponent<Button>();
    //    _button.onClick.AddListener(OnClick);
    //}

    //private void OnClick()
    //{
    //    if (node.unlocked)
    //    {
    //        treeManager.Prune(node);
    //    }
    //    else if (node.isPruned)
    //    {
    //        treeManager.Regrow(node);
    //    }
    //    else
    //    {
    //        treeManager.TryUnlock(node);
    //    }
    //}
}
