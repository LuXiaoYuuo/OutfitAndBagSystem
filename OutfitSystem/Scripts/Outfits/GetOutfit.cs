using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOutfit : MonoBehaviour
{
    /// <summary>
    /// ��ȡ�µ�װ��ʵ��
    /// </summary>
    /// <param name="outfitInfo">װ��ԭʼ����</param>
    /// <returns>�µ�װ��ʵ��</returns>
    public OutfitInstance GetNewOutfitInstance(OutfitInfo outfitInfo)
    {
        OutfitInstance outfitInstance = new OutfitInstance();
        outfitInstance.outfitInfo = outfitInfo;
        outfitInstance.instanceGuid = System.Guid.NewGuid().ToString();
        outfitInstance.isEnable = false;
        return outfitInstance;
    }
    /// <summary>
    /// ֱ�ӻ�ȡȫ��װ�������뱳��
    /// </summary>
    public void GetAllOutfitInstance()
    {
        foreach(var x in OutfitManager.instance.weaponOutfitSO.outfitInfos)
        {
            BagManager.instance.AddProp(GetNewOutfitInstance(x));
        }
        foreach (var x in OutfitManager.instance.headOutfitSO.outfitInfos)
        {
            BagManager.instance.AddProp(GetNewOutfitInstance(x));
        }
        foreach (var x in OutfitManager.instance.armorOutfitSO.outfitInfos)
        {
            BagManager.instance.AddProp(GetNewOutfitInstance(x));
        }
    }
}
