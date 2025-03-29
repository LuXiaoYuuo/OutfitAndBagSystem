using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ������ ���д�����������д������
/// </summary>
public class EntryManager : MonoBehaviour
{
    public static EntryManager instance;

    public EntrySO entrySO;

    public List<Entry> entries = new List<Entry>();//���װ�������б�
    public Dictionary<string, Coroutine> entryCoroutine = new Dictionary<string, Coroutine>();//����Э��
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
    #region ������ɾ
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
    /// ��ʼ�������� ��������ص��������Ӧ������
    /// </summary>
    public void EntryInit()
    {
        var _method = this.GetType().GetMethods();
        foreach (var method in _method)
        {
            var attr = method.GetCustomAttributes(typeof(EntryAttribute), false);
            if (attr == null||attr.Length==0)//Ϊ����û�б������Ա��
                continue;
            EntryAttribute entryAttr = (EntryAttribute)attr[0];//��Ϊ����Ψһ�Ǹ�Ԫ�ؼ�ΪĿ������
            Debug.Log("�ҵ������Ժ�����" + entryAttr.Name + entryAttr.EntryState + attr.Length);
            UnityAction entryAction = () =>
            {
                method.Invoke(this, null);//�������� ����ί�� ��װ�÷���
            };
            if(entryAttr.EntryState== EntryState.Add)
            {
                GetEntry(entryAttr.Name).onEventAdd.AddListener(entryAction);//MethodInfo�޷�ֱ�Ӽ���
            }else
            {
                GetEntry(entryAttr.Name).onEventRemove.AddListener(entryAction);//MethodInfo�޷�ֱ�Ӽ���
            }
        }
    }
    /// <summary>
    /// ��ȡָ������
    /// </summary>
    /// <param name="name">��������</param>
    /// <returns>���� �Ҳ�����Ӧ�����򷵻�null</returns>
    public Entry GetEntry(string name)
    {
        foreach(var _entry in entrySO.entryPool)
        {
            if (_entry.name == name)
                return _entry;
        }
        return null;
    }
    #region ��������
    [Entry("����",EntryState.Add)]
    public void FierceWokAdd()
    {
        Debug.Log("����˴���Ч�������죡");
    }
    [Entry("����",EntryState.Remove)]
    public void FierceWokRemove()
    {
        Debug.Log("�Ƴ��˴���Ч�������죡");
    }
    [Entry("����",EntryState.Add)]
    public void CureAdd()
    {
        Debug.Log("�������������׼������Э��");
        Coroutine coroutine = StartCoroutine(EntryCoroutine.Cure(0.5f * PlayerInfoManager.instance.playerInfo.healthP, 1f));
        entryCoroutine.Add("����", coroutine);
    }
    [Entry("����", EntryState.Remove)]
    public void CureRemove()
    {
        Debug.Log("�Ƴ�����������׼���뿪Э��");
        StopCoroutine(entryCoroutine["����"]);
        entryCoroutine.Remove("����");
    }
    #endregion
}
/// <summary>
/// ���г����Դ������������Э��ʵ��
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
                Debug.Log("�������" + value + "��Ѫ��");
            }
        }
    }
}
/// <summary>
/// ����
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
/// ����ִ�з���������������ǩ ������ַ���Ϊ������
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
/// ��������״̬ �����ô����ص�������װ��ʱ��������ж��ʱ����
/// </summary>
public enum EntryState
{
    Add,
    Remove,
}
