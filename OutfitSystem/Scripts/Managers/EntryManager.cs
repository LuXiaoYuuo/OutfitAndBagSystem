using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 词条库 所有词条方法均会写在这里
/// </summary>
public class EntryManager : MonoBehaviour
{
    public static EntryManager instance;

    public EntrySO entrySO;

    public List<Entry> entries = new List<Entry>();//玩家装备词条列表
    public Dictionary<string, Coroutine> entryCoroutine = new Dictionary<string, Coroutine>();//词条协程
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void Start()
    {
        EntryInit();
    }
    #region 词条增删
    public void AddEntry(Entry entry)
    {
        entries.Add(entry);
        entry.onEventAdd.Invoke();
    }
    public void AddEntry(string entryName)
    {
        Entry m_Entry = GetEntry(entryName);
        entries.Add(m_Entry);
        m_Entry.onEventAdd.Invoke();
    }
    public void RemoveEntry(Entry entry)
    {
        entries.Remove(entry);
        entry.onEventRemove.Invoke();
    }
    public void RemoveEntry(string entryName)
    {
        Entry m_Entry = GetEntry(entryName);
        entries.Remove(m_Entry);
        m_Entry.onEventRemove.Invoke();
    }
    #endregion
    /// <summary>
    /// 初始化词条库 将词条库回调函数与对应函数绑定
    /// </summary>
    public void EntryInit()
    {
        var _method = this.GetType().GetMethods();
        foreach (var method in _method)
        {
            var attr = method.GetCustomAttributes(typeof(EntryAttribute), false);
            if (attr == null||attr.Length==0)//为空则没有被该特性标记
                continue;
            EntryAttribute entryAttr = (EntryAttribute)attr[0];//不为空则唯一那个元素即为目标特性
            Debug.Log("找到了特性函数！" + entryAttr.Name + entryAttr.EntryState + attr.Length);
            UnityAction entryAction = () =>
            {
                method.Invoke(this, null);//无名方法 定义委托 封装该方法
            };
            if(entryAttr.EntryState== EntryState.Add)
            {
                GetEntry(entryAttr.Name).onEventAdd.AddListener(entryAction);//MethodInfo无法直接监听
            }else
            {
                GetEntry(entryAttr.Name).onEventRemove.AddListener(entryAction);//MethodInfo无法直接监听
            }
        }
    }
    /// <summary>
    /// 获取指定词条
    /// </summary>
    /// <param name="name">词条名字</param>
    /// <returns>词条 找不到对应词条则返回null</returns>
    public Entry GetEntry(string name)
    {
        foreach(var _entry in entrySO.entryPool)
        {
            if (_entry.name == name)
                return _entry;
        }
        return null;
    }
    #region 词条方法
    [Entry("凶镬",EntryState.Add)]
    public void FierceWokAdd()
    {
        Debug.Log("添加了词条效果，凶镬！");
    }
    [Entry("凶镬",EntryState.Remove)]
    public void FierceWokRemove()
    {
        Debug.Log("移除了词条效果，凶镬！");
    }
    [Entry("治愈",EntryState.Add)]
    public void CureAdd()
    {
        Debug.Log("添加治愈词条，准备进入协程");
        Coroutine coroutine = StartCoroutine(EntryCoroutine.Cure(0.5f * PlayerInfoManager.instance.playerInfo.healthP, 1f));
        entryCoroutine.Add("治愈", coroutine);
    }
    [Entry("治愈", EntryState.Remove)]
    public void CureRemove()
    {
        Debug.Log("移除治愈词条，准备离开协程");
        StopCoroutine(entryCoroutine["治愈"]);
        entryCoroutine.Remove("治愈");
    }
    #endregion
}
/// <summary>
/// 所有持续性词条方法会调用协程实现
/// </summary>
public static class EntryCoroutine
{
    public static IEnumerator Cure(float value,float waitTime)
    {
        float time = 0;
        while(true)
        {
            if(time<=waitTime)
            {
                time += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }else
            {
                time = 0;
                Debug.Log("治愈玩家" + value + "点血量");
            }
        }
    }
}
/// <summary>
/// 词条
/// </summary>
[System.Serializable]
public class Entry
{
    public string name;
    public string discription;
    public UnityEvent onEventAdd;
    public UnityEvent onEventRemove;
}
/// <summary>
/// 词条执行方法均会打上这个标签 传入的字符串为词条名
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class EntryAttribute : Attribute
{
    private string name;
    private EntryState entryState;
    public string Name
    {
        get
        {
            return name;
        }
    }
    public EntryState EntryState
    {
        get
        {
            return entryState;
        }
    }
    public EntryAttribute(string _name,EntryState _entryState)
    {
        name = _name;
        entryState = _entryState;
    }
}
/// <summary>
/// 词条方法状态 表明该词条回调函数是装备时触发还是卸下时触发
/// </summary>
public enum EntryState
{
    Add,
    Remove,
}
