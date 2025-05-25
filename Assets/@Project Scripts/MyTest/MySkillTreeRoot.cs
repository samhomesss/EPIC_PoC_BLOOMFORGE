using UnityEngine;

public class MySkillTreeRoot : MonoBehaviour
{
    public GameObject treeRoot;
    public GameObject[] slotList;

    public bool isCanOpen = false;
    public bool isSkillOpen = false;

    // 트리 노드 상태 갱신 함수
    public void RefreshStatus()
    {
        isCanOpen = treeRoot != null && treeRoot.activeSelf;

        // 자신은 treeRoot가 켜졌을 때만 보이게 설정
        gameObject.SetActive(isCanOpen);

        if (!isCanOpen)
        {
            isSkillOpen = false;
            foreach (var slot in slotList)
                slot.SetActive(false);
        }
        else if (isSkillOpen)
        {
            foreach (var slot in slotList)
                slot.SetActive(true);
        }
    }
}
