using System.Collections.Generic;
using UnityEngine;

public class UI_MySkillTree : UI_Scene
{
    public List<GameObject> treeObject;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Game.OnGiveWaterEvent += TreeGrowsUp;

        // 시작 시 모든 트리 초기화
        foreach (var item in treeObject)
        {
            var root = item.GetComponent<MySkillTreeRoot>();
            if (root != null)
            {
                item.SetActive(false);              // 처음엔 비활성화
                root.isSkillOpen = false;
            }
        }

        // 루트 노드 하나만 수동으로 켜기 (예: Root1)
        var first = treeObject.Find(obj => obj.name == "Root1");
        if (first != null)
        {
            first.SetActive(true);
            var root = first.GetComponent<MySkillTreeRoot>();
            if (root != null)
            {
                root.isCanOpen = true;
                root.isSkillOpen = true;
                root.RefreshStatus();
            }
        }

        return true;
    }

    void TreeGrowsUp(int giveWater)
    {
        // 상태 갱신 (단순히 isCanOpen만 재계산, 활성화는 여기서 안 함)
        foreach (var item in treeObject)
        {
            var root = item.GetComponent<MySkillTreeRoot>();
            root?.RefreshStatus();
        }

        // 해금 가능한 노드 리스트업
        List<GameObject> openableNodes = new List<GameObject>();
        foreach (var item in treeObject)
        {
            var root = item.GetComponent<MySkillTreeRoot>();
            if (root != null && root.isCanOpen && !root.isSkillOpen) // 🔒 이미 열린 건 제외
            {
                openableNodes.Add(item);
            }
        }

        // 하나만 무작위로 열기
        if (openableNodes.Count > 0)
        {
            int randIndex = Random.Range(0, openableNodes.Count);
            GameObject selectedNode = openableNodes[randIndex];
            var rootScript = selectedNode.GetComponent<MySkillTreeRoot>();

            // 부모 트리 켜기
            if (rootScript.treeRoot != null)
                rootScript.treeRoot.SetActive(true);

            // 현재 노드 열기
            selectedNode.SetActive(true);
            rootScript.isCanOpen = false;
            rootScript.isSkillOpen = true;

            // 자식 슬롯 열기
            foreach (var slot in rootScript.slotList)
                slot.SetActive(true);

            Debug.Log($"[🌱 하나만 성장] {selectedNode.name}");
        }
    }

}
