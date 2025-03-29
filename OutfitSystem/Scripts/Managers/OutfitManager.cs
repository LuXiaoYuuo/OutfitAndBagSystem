using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OutfitManager : MonoBehaviour
{
    public static OutfitManager instance;

    [Header("SoAssets")]
    public QualityColorSO qualityColorSO;//Ʒ����ɫ
    public OutfitSO weaponOutfitSO;//������
    public OutfitSO headOutfitSO;//ͷ����
    public OutfitSO armorOutfitSO;//���׿�

    [Header("Data")]
    public Dictionary<OutfitType, OutfitInstance> playerOutfit = new Dictionary<OutfitType, OutfitInstance>();
    public Dictionary<OutfitType, bool> checkMouseEnter = new Dictionary<OutfitType, bool>();
    public List<OutfitGridView> playerOutfitView = new List<OutfitGridView>();
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(this);
        }
    }
    public void Start()
    {
        //��ʼ��װ���ֵ� ��֤����ö��װ�������Ա�����
        foreach(var outfitType in Enum.GetNames(typeof(OutfitType)))
        {
            object _outfitType;
            Enum.TryParse(typeof(OutfitType), outfitType, out _outfitType);
            playerOutfit.Add((OutfitType)_outfitType, null);//װ��������
            checkMouseEnter.Add((OutfitType)_outfitType, false);//�����
        }
    }
    /// <summary>
    /// ����װ��ʱִ��
    /// </summary>
    /// <param name="outfitInfo">Ŀ��װ������</param>
    public void OnOutfitWear(OutfitInstance outfitInfo)
    {
        if (playerOutfit[outfitInfo.outfitInfo.outfitType] != null)//����λ����װ��ʱ��ж��װ��
            OnOutfitRemove(playerOutfit[outfitInfo.outfitInfo.outfitType]);
        playerOutfit[outfitInfo.outfitInfo.outfitType] = outfitInfo;
        //֪ͨUI��Ⱦװ������
        GetOutfitGridView(outfitInfo.outfitInfo.outfitType).Wear(outfitInfo);
        //֪ͨ������Ը���
        PlayerInfoManager.instance.PlayerInfoAdd(outfitInfo);
    }
    /// <summary>
    /// ��ж��װ��ʱ
    /// </summary>
    /// <param name="outfitInfo">Ŀ��װ������</param>
    public void OnOutfitRemove(OutfitInstance outfitInfo)
    {
        if (playerOutfit[outfitInfo.outfitInfo.outfitType] == null)//����λ����װ��/ʱ�޷�ж��װ��
        {
            return;
        }
        playerOutfit[outfitInfo.outfitInfo.outfitType] = null;
        //֪ͨUI��Ⱦװ��ж��
        GetOutfitGridView(outfitInfo.outfitInfo.outfitType).Unload();
        //֪ͨ������Ը���
        PlayerInfoManager.instance.PlayerInfoRemove(outfitInfo);
    }
    /// <summary>
    /// ����ָ��װ������
    /// </summary>
    /// <param name="outfitInfo">װ������</param>
    public void InfoAdd(OutfitInstance outfitInfo)
    {
        for(var i =0;i<outfitInfo.outfitInfo.gainTypes.Count;i++)
        {
            PlayerInfoManager.instance.AddInfo(outfitInfo.outfitInfo.gainTypes[i], outfitInfo.outfitInfo.gainValue[i]);
        }
    }
    /// <summary>
    /// �Ƴ�ָ��װ������
    /// </summary>
    /// <param name="outfitInfo">װ������</param>
    public void InfoRemove(OutfitInstance outfitInfo)
    {
        for (var i = 0; i < outfitInfo.outfitInfo.gainTypes.Count; i++)
        {
            PlayerInfoManager.instance.RemoveInfo(outfitInfo.outfitInfo.gainTypes[i], outfitInfo.outfitInfo.gainValue[i]);
        }
    }
    /// <summary>
    /// ��ȡָ��Ʒ�ʶ�Ӧ��ɫ
    /// </summary>
    /// <param name="quality">Ʒ��</param>
    /// <returns>��ɫ ��ָ����ɫ�򷴻ذ�ɫ</returns>
    public Color GetQualityColor(Quality quality)
    {
        foreach(var qua in qualityColorSO.qualityToColor)
        {
            if (quality == qua.quality)
                return qua.color;
        }
        return new Color(1,1,1,1);//�޶�ӦƷ����ӦΪ��ɫ
    }
    /// <summary>
    /// ��ȡָ��װ�����͵�װ��������ͼ
    /// </summary>
    /// <param name="_outfitType">װ������</param>
    /// <returns>װ��������ͼ ��û�ҵ�ָ���򷵻�null</returns>
    public OutfitGridView GetOutfitGridView(OutfitType _outfitType)
    {
        foreach(var x in playerOutfitView)
        {
            if (x.outfitGirdType == _outfitType)
                return x;
        }
        return null;
    }
}
