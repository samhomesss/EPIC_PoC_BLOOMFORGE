using UnityEngine;

public class TreeObject : BaseObject
{
    Hero _hero; // 플레이어 위치 받아 오기 위함 

    const float DISTANCE = 2f;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _hero = FindAnyObjectByType<Hero>();

        return true;
    }

    private void Update()
    {
        float dist = (_hero.transform.position - transform.position).magnitude;
        
        if(dist <= DISTANCE)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("현재 상호작용 중입니다.");
                // TODO :  씨앗을 성장 시키기 위한 UI 존재 
            }
        }

    }
}
