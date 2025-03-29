using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : MonoBehaviour
{

    public static BagManager instance;

    [Header("��¼")]
    public GridView selectedInfo;//��קʱѡ�е�����
    public GridView currentEnterInfo;//��굱ǰ���������
    [Header("����")]
    public GameObject gridPrefab;//����Ԥ����
    public GameObject outfitInfoObj;//װ����Ϣ
    public Transform bagViewContainer;//��������
    public OutfitInfoView outfitInfoView;//װ����Ϣ��ͼ
    public Image selectedImage;//��קʱ��������ͼƬ
    [Header("Info")]
    //�������������е���
    public List<OutfitInstance> bagContainer = new List<OutfitInstance>();
    //�������߶�Ӧ�ĸ�������
    public Dictionary<int,OutfitInstance> outfitIndex = new Dictionary<int,OutfitInstance>();
    //�����������и�����ͼ
    public List<GridView> gridViews = new List<GridView>();

    [Header("����")]
    public int gridNumber;//��������
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void Start()
    {
        for(int i = 0;i<gridNumber;i++)//��ʼ������
        {
            GameObject obj = GameObject.Instantiate(gridPrefab, bagViewContainer);
            GridView m_gridView = obj.GetComponent<GridView>();
            m_gridView.index = i;
            outfitIndex.Add(i,null);
            gridViews.Add(m_gridView);
        }
    }
    /// <summary>
    /// �򱳰�����ӵ���
    /// </summary>
    /// <param name="outfitInstance">����ʵ��</param>
    public void AddProp(OutfitInstance outfitInstance)
    {
        bagContainer.Add(outfitInstance);
        for(int i =0;i<gridNumber; i++)
        {
            if(outfitIndex[i]==null)
            {
                outfitIndex[i] = outfitInstance;
                gridViews[i].UiRefresh(outfitInstance);
                return;
            }
        }
    }
    /// <summary>
    /// �ӱ�����ɾ������
    /// </summary>
    /// <param name="outfitInstance">����ʵ��</param>
    public void RemoveProp(OutfitInstance outfitInstance)
    {
        bagContainer.Remove(outfitInstance);
        foreach(var x in outfitIndex)
        {
            if(x.Value==outfitInstance)
            {
                outfitIndex[x.Key] = null;
                gridViews[x.Key].UiReset();
                return;
            }
        }
    }
    /// <summary>
    /// ����ָ�����ӵ���
    /// </summary>
    /// <param name="outfitInstance">�µĵ���</param>
    /// <param name="index">�ڸ����б������</param>
    public void RefreshProp(OutfitInstance outfitInstance,int index)
    {
        gridViews[index].UiRefresh(outfitInstance);
        outfitIndex[index] = outfitInstance;
    }
    /// <summary>
    /// ����ָ�����ߵĸ������� ��û�ҵ��򷵻�-1
    /// </summary>
    /// <param name="outfitInstance"></param>
    /// <returns></returns>
    public int GetOutfitIndex(OutfitInstance outfitInstance)
    {
        foreach (var x in outfitIndex)
        {
            if (x.Value == null)
                continue;
            if (x.Value.instanceGuid == outfitInstance.instanceGuid)
            {
                return x.Key;
            }
        }
        return -1;
    }
    /// <summary>
    /// ������ǰ��ѡ�ж������ݲ�ˢ��UI
    /// </summary>
    public void CheckExchange()
    {
        if (selectedInfo == currentEnterInfo)
            return;
        OutfitInstance one = selectedInfo.outfitInfo;
        OutfitInstance two = currentEnterInfo.outfitInfo;

        RefreshProp(two, selectedInfo.index);
        RefreshProp(one, currentEnterInfo.index);
    }
    /// <summary>
    /// ���װ���Ƿ���Դ�����������
    /// </summary>
    public void CheckWear()
    {
        if (!OutfitManager.instance.checkMouseEnter[selectedInfo.outfitInfo.outfitInfo.outfitType])
            return;//װ�����Ͳ�һ��
        if (OutfitManager.instance.playerOutfit[selectedInfo.outfitInfo.outfitInfo.outfitType] == selectedInfo.outfitInfo)
            return;//װ���Ѿ���װ��
        OutfitManager.instance.OnOutfitWear(selectedInfo.outfitInfo);
    }
    /// <summary>
    /// ����ק����ʱִ��
    /// </summary>
    public void OnViewDragEnd()
    {
        if(currentEnterInfo!=null)
        CheckExchange();
        else if(OutfitManager.instance.checkMouseEnter[selectedInfo.outfitInfo.outfitInfo.outfitType])
        CheckWear();
    }
    public void OnInfoViewEnter(OutfitInstance outfitInstance)
    {
        outfitInfoView.Refresh(outfitInstance);
        outfitInfoObj.SetActive(true);
    }
    public void OnInfoViewExit()
    {
        outfitInfoObj.SetActive(false);
    }
}
