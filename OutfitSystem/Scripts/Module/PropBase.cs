using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���߻���
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
    ��,
    ����,
    ����,
    ��ħ,
}
public enum OutfitType
{
    ͷ��,
    ����,
    ����,
}
public enum GainType
{
    Ѫ��,
    ����,
    ����,
}