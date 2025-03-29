using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OutfitManager : MonoBehaviour
{
    public static OutfitManager instance;

    [Header("SoAssets")]
    public QualityColorSO qualityColorSO;//品质颜色
    public OutfitSO weaponOutfitSO;//武器库
    public OutfitSO headOutfitSO;//头盔库
    public OutfitSO armorOutfitSO;//盔甲库

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
        //初始化装备字典 保证所有枚举装备均可以被生成
        foreach(var outfitType in Enum.GetNames(typeof(OutfitType)))
        {
            object _outfitType;
            Enum.TryParse(typeof(OutfitType), outfitType, out _outfitType);
            playerOutfit.Add((OutfitType)_outfitType, null);//装备栏数据
            checkMouseEnter.Add((OutfitType)_outfitType, false);//鼠标检测
        }
    }
    /// <summary>
    /// 当穿装备时执行
    /// </summary>
    /// <param name="outfitInfo">目标装备数据</param>
    public void OnOutfitWear(OutfitInstance outfitInfo)
    {
        if (playerOutfit[outfitInfo.outfitInfo.outfitType] != null)//当该位置有装备时先卸下装备
            OnOutfitRemove(playerOutfit[outfitInfo.outfitInfo.outfitType]);
        playerOutfit[outfitInfo.outfitInfo.outfitType] = outfitInfo;
        //通知UI渲染装备穿戴
        GetOutfitGridView(outfitInfo.outfitInfo.outfitType).Wear(outfitInfo);
        //通知玩家属性更新
        PlayerInfoManager.instance.PlayerInfoAdd(outfitInfo);
    }
    /// <summary>
    /// 当卸下装备时
    /// </summary>
    /// <param name="outfitInfo">目标装备数据</param>
    public void OnOutfitRemove(OutfitInstance outfitInfo)
    {
        if (playerOutfit[outfitInfo.outfitInfo.outfitType] == null)//当该位置无装备/时无法卸下装备
        {
            return;
        }
        playerOutfit[outfitInfo.outfitInfo.outfitType] = null;
        //通知UI渲染装备卸下
        GetOutfitGridView(outfitInfo.outfitInfo.outfitType).Unload();
        //通知玩家属性更新
        PlayerInfoManager.instance.PlayerInfoRemove(outfitInfo);
    }
    /// <summary>
    /// 增加指定装备增益
    /// </summary>
    /// <param name="outfitInfo">装备数据</param>
    public void InfoAdd(OutfitInstance outfitInfo)
    {
        for(var i =0;i<outfitInfo.outfitInfo.gainTypes.Count;i++)
        {
            PlayerInfoManager.instance.AddInfo(outfitInfo.outfitInfo.gainTypes[i], outfitInfo.outfitInfo.gainValue[i]);
        }
    }
    /// <summary>
    /// 移除指定装备增益
    /// </summary>
    /// <param name="outfitInfo">装备增益</param>
    public void InfoRemove(OutfitInstance outfitInfo)
    {
        for (var i = 0; i < outfitInfo.outfitInfo.gainTypes.Count; i++)
        {
            PlayerInfoManager.instance.RemoveInfo(outfitInfo.outfitInfo.gainTypes[i], outfitInfo.outfitInfo.gainValue[i]);
        }
    }
    /// <summary>
    /// 获取指定品质对应颜色
    /// </summary>
    /// <param name="quality">品质</param>
    /// <returns>颜色 无指定颜色则反回白色</returns>
    public Color GetQualityColor(Quality quality)
    {
        foreach(var qua in qualityColorSO.qualityToColor)
        {
            if (quality == qua.quality)
                return qua.color;
        }
        return new Color(1,1,1,1);//无对应品质则反应为白色
    }
    /// <summary>
    /// 获取指定装备类型的装备格子视图
    /// </summary>
    /// <param name="_outfitType">装备类型</param>
    /// <returns>装备格子视图 若没找到指定则返回null</returns>
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
