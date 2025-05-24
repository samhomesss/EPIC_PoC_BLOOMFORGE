using System;
using System.Collections.Generic;
using UnityEngine;
using static Define;
/// <summary>
/// 모든 오브젝트 , 몬스터 , 히어로 , 날라다니는 투사체 등 
/// 모든 오브젝트 들이 상속 받을 부모 클래스 
/// </summary>
public class BaseObject : InitBase
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
    public EObjectType ObjectType { get; protected set; } = EObjectType.None;
    public CircleCollider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public float ColliderRadius { get { return Collider?.radius ?? 0.0f; } }
    public Vector3 CenterPosition { get { return transform.position + Vector3.up * ColliderRadius; } }

    bool _lookLeft = true;

    // 온라인 멀티 게임을 염두해 두고 만든 변수 
    public bool LookLeft 
    {
        get
        { 
            return _lookLeft;
        }
        set 
        { 
            _lookLeft = value; 
            Flip(!value); 
        }
    }
    #region Bind & Get
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] name = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[name.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < name.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, name[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, name[i], true);
            if (objects[i] == null)
                Debug.Log($"Failed to bind({name[i]})");
        }
    }

    protected void BindObjects(Type type) { Bind<GameObject>(type); }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;

        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    //Todo : 게임 오브젝트를 다른 곳으로 가져오기 위해서 Public으로 변경 하지만 protected로 해놓은거를 public 으로 바꿔야 할지가 고민 
    public GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    #endregion
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Collider = gameObject.GetOrAddComponent<CircleCollider2D>();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        return true;
    }

    public void TranslateEx(Vector3 dir)
    {
        transform.Translate(dir);

        if (dir.x < 0)
            LookLeft = true;
        else if (dir.x > 0)
            LookLeft = false;
    }

    /// <summary>
    /// 상속받는 자식에서 해당 함수를 구현 하는 거 
    /// </summary>
    protected virtual void UpdateAnimation() { }
    public void PlayAnimation(string AniName)
    {
        if (Animator == null)
            return;
        Debug.Log(AniName + "실행");
        Animator.Play(AniName);
    }

    public void Flip(bool flag)
    {
        if (Animator == null)
            return;

        Vector3 localScale = Animator.gameObject.transform.localScale;
        localScale.x = flag ? 1f : -1f;
        Animator.gameObject.transform.localScale = localScale;
    }
}
