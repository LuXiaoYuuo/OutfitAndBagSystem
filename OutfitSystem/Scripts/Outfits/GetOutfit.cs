using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOutfit : MonoBehaviour
{
    /// <summary>
    /// 获取新的装备实例
    /// </summary>
    /// <param name="outfitInfo">装备原始数据</param>
    /// <returns>新得装备实例</returns>
    public OutfitInstance GetNewOutfitInstance(OutfitInfo outfitInfo)
    {
        OutfitInstance outfitInstance = new OutfitInstance();
        outfitInstance.outfitInfo = outfitInfo;
        outfitInstance.instanceGuid = System.Guid.NewGuid().ToString();
        outfitInstance.isEnable = false;
        return outfitInstance;
    }
    /// <summary>
    /// 直接获取全部装备并加入背包
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
