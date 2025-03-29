using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : MonoBehaviour
{

    public static BagManager instance;

    [Header("记录")]
    public GridView selectedInfo;//推拽时选中的数据
    public GridView currentEnterInfo;//鼠标当前进入的数据
    [Header("对象")]
    public GameObject gridPrefab;//格子预制体
    public GameObject outfitInfoObj;//装备信息
    public Transform bagViewContainer;//格子容器
    public OutfitInfoView outfitInfoView;//装备信息视图
    public Image selectedImage;//推拽时跟随鼠标的图片
    [Header("Info")]
    //背包容器内所有道具
    public List<OutfitInstance> bagContainer = new List<OutfitInstance>();
    //背包道具对应的格子索引
    public Dictionary<int,OutfitInstance> outfitIndex = new Dictionary<int,OutfitInstance>();
    //背包容器所有格子视图
    public List<GridView> gridViews = new List<GridView>();

    [Header("变量")]
    public int gridNumber;//格子数量
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void Start()
    {
        for(int i = 0;i<gridNumber;i++)//初始化背包
        {
            GameObject obj = GameObject.Instantiate(gridPrefab, bagViewContainer);
            GridView m_gridView = obj.GetComponent<GridView>();
            m_gridView.index = i;
            outfitIndex.Add(i,null);
            gridViews.Add(m_gridView);
        }
    }
    /// <summary>
    /// 向背包内添加道具
    /// </summary>
    /// <param name="outfitInstance">道具实例</param>
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
    /// 从背包内删除道具
    /// </summary>
    /// <param name="outfitInstance">道具实例</param>
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
    /// 更改指定格子道具
    /// </summary>
    /// <param name="outfitInstance">新的道具</param>
    /// <param name="index">在格子列表的索引</param>
    public void RefreshProp(OutfitInstance outfitInstance,int index)
    {
        gridViews[index].UiRefresh(outfitInstance);
        outfitIndex[index] = outfitInstance;
    }
    /// <summary>
    /// 返回指定道具的格子索引 若没找到则返回-1
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
    /// 交换当前和选中二者数据并刷新UI
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
    /// 检测装备是否可以穿戴并穿戴上
    /// </summary>
    public void CheckWear()
    {
        if (!OutfitManager.instance.checkMouseEnter[selectedInfo.outfitInfo.outfitInfo.outfitType])
            return;//装备类型不一样
        if (OutfitManager.instance.playerOutfit[selectedInfo.outfitInfo.outfitInfo.outfitType] == selectedInfo.outfitInfo)
            return;//装备已经被装备
        OutfitManager.instance.OnOutfitWear(selectedInfo.outfitInfo);
    }
    /// <summary>
    /// 当拖拽结束时执行
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
