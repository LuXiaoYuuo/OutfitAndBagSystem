using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OutfitGridView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    public OutfitType outfitGirdType;
    [Header("数据")]
    public OutfitInstance m_OutfitInstance;
    [Header("组件")]
    public Image icon;
    public Image quality;

    [Header("状态")]
    public bool isWearn;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OutfitManager.instance.checkMouseEnter[outfitGirdType] = true;
        Debug.Log("已设置鼠标进入！" + outfitGirdType.ToString() + OutfitManager.instance.checkMouseEnter[outfitGirdType].ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OutfitManager.instance.checkMouseEnter[outfitGirdType] = false;
        Debug.Log("已设置鼠标离开！" + outfitGirdType.ToString() + OutfitManager.instance.checkMouseEnter[outfitGirdType].ToString());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_OutfitInstance == null)
            return;
        OutfitManager.instance.OnOutfitRemove(m_OutfitInstance);
    }
    /// <summary>
    /// 刷新UI
    /// </summary>
    public void Refresh()
    {
        if(m_OutfitInstance==null)
        {
            icon.sprite = null;
            quality.color = new Color(1, 1, 1, 1);
            isWearn = false;
        }else
        {
            icon.sprite = m_OutfitInstance.outfitInfo.icon;
            quality.color = OutfitManager.instance.GetQualityColor(m_OutfitInstance.outfitInfo.quality);
            isWearn = true;
        }
    }
    /// <summary>
    /// 穿戴指定装备
    /// </summary>
    /// <param name="_outfitInstance">装备</param>
    public void Wear(OutfitInstance _outfitInstance)
    {
        m_OutfitInstance = _outfitInstance;
        BagManager.instance.gridViews[BagManager.instance.GetOutfitIndex(m_OutfitInstance)].SetOutfitEnable(true);
        BagManager.instance.gridViews[BagManager.instance.GetOutfitIndex(m_OutfitInstance)].Refresh();
        Refresh();
    }
    /// <summary>
    /// 卸下装备
    /// </summary>
    public void Unload()
    {
        BagManager.instance.gridViews[BagManager.instance.GetOutfitIndex(m_OutfitInstance)].SetOutfitEnable(false);
        BagManager.instance.gridViews[BagManager.instance.GetOutfitIndex(m_OutfitInstance)].Refresh();
        m_OutfitInstance = null;
        Refresh();
    }

}
