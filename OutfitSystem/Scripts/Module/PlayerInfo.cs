using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    [Header("����")]
    public float healthP;//Ѫ��
    public float damageP;//����
    public float defenseP;//����

     public PlayerInfo()
     {
        healthP = 100;
        damageP = 10;
        defenseP = 5;
     }
    /// <summary>
    /// �������״̬
    /// </summary>
    /// <param name="gainType">Ŀ������</param>
    /// <param name="value">��ֵ</param>
    public void AddInfo(GainType gainType,float value)
    {
        switch(gainType)
        {
            case GainType.Ѫ��:
                {
                    healthP += value;
                    break;
                }
            case GainType.����:
                {
                    defenseP += value;
                    break;
                }
            case GainType.����:
                {
                    damageP += value;
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
                    healthP -= value;
                    break;
                }
            case GainType.����:
                {
                    defenseP -= value;
                    break;
                }
            case GainType.����:
                {
                    damageP -= value;
                    break;
                }
        }
    }
}
