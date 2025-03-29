using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    [Header("属性")]
    public float healthP;//血量
    public float damageP;//力量
    public float defenseP;//防御

     public PlayerInfo()
     {
        healthP = 100;
        damageP = 10;
        defenseP = 5;
     }
    /// <summary>
    /// 增加玩家状态
    /// </summary>
    /// <param name="gainType">目标类型</param>
    /// <param name="value">数值</param>
    public void AddInfo(GainType gainType,float value)
    {
        switch(gainType)
        {
            case GainType.血量:
                {
                    healthP += value;
                    break;
                }
            case GainType.护甲:
                {
                    defenseP += value;
                    break;
                }
            case GainType.力量:
                {
                    damageP += value;
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
                    healthP -= value;
                    break;
                }
            case GainType.护甲:
                {
                    defenseP -= value;
                    break;
                }
            case GainType.力量:
                {
                    damageP -= value;
                    break;
                }
        }
    }
}
