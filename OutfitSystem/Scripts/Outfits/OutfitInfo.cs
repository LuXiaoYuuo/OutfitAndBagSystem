using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// 装备类型基类
/// </summary>
public class OutfitInfo : PropBase
{
    public OutfitType outfitType;
    public List<string> entryNamePool = new List<string>();//词条名字池 用于面板配置词条
    public List<Entry> entries = new List<Entry>();//词条池 所有词条会存在这里
    /// <summary>
    /// 此处表示该装备增益效果与对应数值 同序数 字典会更合适 但是Unity的检查器窗口不支持字典类显示
    /// </summary>
    public List<GainType> gainTypes = new List<GainType>();
    public List<float> gainValue = new List<float>();

}
