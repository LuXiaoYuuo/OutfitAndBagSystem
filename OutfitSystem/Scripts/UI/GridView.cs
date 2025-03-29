using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 背包格子视图
/// </summary>
public class GridView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("道具数据")]
    public OutfitInstance outfitInfo;
    public int index;//在gridViews的列表索引号
    public string propGuid;//当前对象的唯一GUID

    [Header("组件")]
    public GameObject enableMask;
    public Image icon;
    public Image quality;

    [Header("状态")]
    public bool isDrag;//是否拖拽
    
    public void UiReset()
    {
        enableMask.SetActive(false);
        icon.sprite = null;
        quality.color = new Color(1,1,1,1);
        outfitInfo = null;
        propGuid = null;
    }
    public void UiRefresh(OutfitInstance _outfitInfo)
    {
        outfitInfo = _outfitInfo;
        if (_outfitInfo == null)
            UiReset();
        else
        {
            icon.sprite = _outfitInfo.outfitInfo.icon;
            quality.color = OutfitManager.instance.GetQualityColor(_outfitInfo.outfitInfo.quality);
            propGuid = _outfitInfo.instanceGuid;
            enableMask.SetActive(outfitInfo.isEnable);
        }
    }
    /// <summary>
    /// 刷新UI 如果该格子不存在数据则刷新为默认样式 存在则刷新为目标道具样式
    /// </summary>
    public void Refresh()
    {
        if (outfitInfo == null)
            UiReset();
        else
            UiRefresh(outfitInfo);
    }
    /// <summary>
    /// 设置武器实例是否被启用
    /// </summary>
    /// <param name="isEnable">是否被启用</param>
    public void SetOutfitEnable(bool isEnable)
    {
        outfitInfo.isEnable = isEnable;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (outfitInfo == null || outfitInfo.isEnable)
            return;
        isDrag = true;
        BagManager.instance.selectedInfo = this;
        BagManager.instance.selectedImage.transform.position = Input.mousePosition;
        BagManager.instance.selectedImage.color = new Color(1, 1, 1, 1);
        icon.color = new Color(1, 1, 1, 0);
        BagManager.instance.selectedImage.sprite = icon.sprite;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (outfitInfo == null||outfitInfo.isEnable)
            return;
        isDrag = false;
        BagManager.instance.OnViewDragEnd();
        icon.color = new Color(1, 1, 1, 1);
        BagManager.instance.selectedImage.color = new Color(1, 1, 1, 0);
        BagManager.instance.selectedInfo = null;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (outfitInfo == null || outfitInfo.isEnable)
            return;
        BagManager.instance.selectedImage.transform.position = Input.mousePosition;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        BagManager.instance.currentEnterInfo = this;
        if (outfitInfo != null)
            BagManager.instance.OnInfoViewEnter(outfitInfo);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        BagManager.instance.currentEnterInfo = null;
        BagManager.instance.OnInfoViewExit();
    }
}
