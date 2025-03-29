using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该脚本可作为玩家装备效果 BUFF效果等额外增益的总控脚本
/// </summary>
public class PlayerInfoManager : MonoBehaviour
{
    public static PlayerInfoManager instance;

    public InfoView infoView;//玩家信息视图 

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
    #region 供外部调用修改玩家数据的函数 可重载添加与移除函数供多种不同参数影响调用
    public void PlayerInfoAdd(OutfitInstance outfitInfo)//此处为参数为装备 重载参数可给其他类型调用
    {
        for (var i = 0; i < outfitInfo.outfitInfo.gainTypes.Count; i++)
        {
            AddInfo(outfitInfo.outfitInfo.gainTypes[i], outfitInfo.outfitInfo.gainValue[i]);
        }
        foreach(var x in outfitInfo.outfitInfo.entryNamePool)
        {
            EntryManager.instance.AddEntry(x);
            Debug.Log("添加到了词条!" + x);
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
    /// 增加玩家状态
    /// </summary>
    /// <param name="gainType">目标类型</param>
    /// <param name="value">数值</param>
    public void AddInfo(GainType gainType, float value)
    {
        switch (gainType)
        {
            case GainType.血量:
                {
                    playerInfo.healthP += value;
                    break;
                }
            case GainType.护甲:
                {
                    playerInfo.defenseP += value;
                    break;
                }
            case GainType.力量:
                {
                    playerInfo.damageP += value;
                    break;
                }
        }
    }
    /// <summary>
    /// 移除玩家状态
    /// </summary>
    /// <param name="gainType">目标类型</param>
    /// <param name="value">数值</param>
    public void RemoveInfo(GainType gainType, float value)
    {
        switch (gainType)
        {
            case GainType.血量:
                {
                    playerInfo.healthP -= value;
                    break;
                }
            case GainType.护甲:
                {
                    playerInfo.defenseP -= value;
                    break;
                }
            case GainType.力量:
                {
                    playerInfo.damageP -= value;
                    break;
                }
        }
    }

}
