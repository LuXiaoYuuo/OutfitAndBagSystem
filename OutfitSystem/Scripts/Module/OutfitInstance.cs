using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 由于配置武器库的武器只是游戏会出现的模板 实际武器实例可能会有与模板的衍生关系 所以需要单独写实例代码
/// </summary>
public class OutfitInstance
{
    public OutfitInfo outfitInfo;//模板武器数据
    public string instanceGuid;//实例的唯一GUID
    public bool isEnable;//是否启用
}
