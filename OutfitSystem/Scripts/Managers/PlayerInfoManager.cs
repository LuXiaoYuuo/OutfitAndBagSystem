using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ýű�����Ϊ���װ��Ч�� BUFFЧ���ȶ���������ܿؽű�
/// </summary>
public class PlayerInfoManager : MonoBehaviour
{
    public static PlayerInfoManager instance;

    public InfoView infoView;//�����Ϣ��ͼ 

    public PlayerInfo playerInfo = new PlayerInfo();

    public void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }else
        {
            Destroy(this);
        }
    }
    #region ���ⲿ�����޸�������ݵĺ��� ������������Ƴ����������ֲ�ͬ����Ӱ�����
    public void PlayerInfoAdd(OutfitInstance outfitInfo)//�˴�Ϊ����Ϊװ�� ���ز����ɸ��������͵���
    {
        for (var i = 0; i < outfitInfo.outfitInfo.gainTypes.Count; i++)
        {
            AddInfo(outfitInfo.outfitInfo.gainTypes[i], outfitInfo.outfitInfo.gainValue[i]);
        }
        foreach(var x in outfitInfo.outfitInfo.entryNamePool)
        {
            EntryManager.instance.AddEntry(x);
            Debug.Log("��ӵ��˴���!" + x);
        }
        infoView.Refresh();
    }

    public void PlayerInfoRemove(OutfitInstance outfitInfo)
    {
        for (var i = 0; i < outfitInfo.outfitInfo.gainTypes.Count; i++)
        {
            RemoveInfo(outfitInfo.outfitInfo.gainTypes[i], outfitInfo.outfitInfo.gainValue[i]);
        }
        foreach (var x in outfitInfo.outfitInfo.entryNamePool)
        {
            EntryManager.instance.RemoveEntry(x);
        }
        infoView.Refresh();
    }
    #endregion

    /// <summary>
    /// �������״̬
    /// </summary>
    /// <param name="gainType">Ŀ������</param>
    /// <param name="value">��ֵ</param>
    public void AddInfo(GainType gainType, float value)
    {
        switch (gainType)
        {
            case GainType.Ѫ��:
                {
                    playerInfo.healthP += value;
                    break;
                }
            case GainType.����:
                {
                    playerInfo.defenseP += value;
                    break;
                }
            case GainType.����:
                {
                    playerInfo.damageP += value;
                    break;
                }
        }
    }
    /// <summary>
    /// �Ƴ����״̬
    /// </summary>
    /// <param name="gainType">Ŀ������</param>
    /// <param name="value">��ֵ</param>
    public void RemoveInfo(GainType gainType, float value)
    {
        switch (gainType)
        {
            case GainType.Ѫ��:
                {
                    playerInfo.healthP -= value;
                    break;
                }
            case GainType.����:
                {
                    playerInfo.defenseP -= value;
                    break;
                }
            case GainType.����:
                {
                    playerInfo.damageP -= value;
                    break;
                }
        }
    }

}
