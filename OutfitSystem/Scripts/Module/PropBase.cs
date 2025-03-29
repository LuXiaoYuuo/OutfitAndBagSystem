using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道具基类
/// </summary>
public class PropBase
{
    public string name;
    public Sprite icon;
    public string discription;
    public Quality quality;
}
public enum Quality
{
    无,
    破损,
    精良,
    附魔,
}
public enum OutfitType
{
    头盔,
    护甲,
    武器,
}
public enum GainType
{
    血量,
    护甲,
    力量,
}