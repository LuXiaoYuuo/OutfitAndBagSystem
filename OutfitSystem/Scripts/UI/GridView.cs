using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// ����������ͼ
/// </summary>
public class GridView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("��������")]
    public OutfitInstance outfitInfo;
    public int index;//��gridViews���б�������
    public string propGuid;//��ǰ�����ΨһGUID

    [Header("���")]
    public GameObject enableMask;
    public Image icon;
    public Image quality;

    [Header("״̬")]
    public bool isDrag;//�Ƿ���ק
    
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
    /// ˢ��UI ����ø��Ӳ�����������ˢ��ΪĬ����ʽ ������ˢ��ΪĿ�������ʽ
    /// </summary>
    public void Refresh()
    {
        if (outfitInfo == null)
            UiReset();
        else
            UiRefresh(outfitInfo);
    }
    /// <summary>
    /// ��������ʵ���Ƿ�����
    /// </summary>
    /// <param name="isEnable">�Ƿ�����</param>
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
