using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoView : MonoBehaviour
{
    [Header("对象")]
    public TextMeshProUGUI infoText;

    public void OnEnable()
    {
        //Refresh();
    }
    /// <summary>
    /// 刷新属性UI
    /// </summary>
    public void Refresh()
    {
        infoText.text = "血量：" + PlayerInfoManager.instance.playerInfo.healthP.ToString() + "\n" +
            "力量：" + PlayerInfoManager.instance.playerInfo.damageP.ToString() + "\n" +
            "护甲：" + PlayerInfoManager.instance.playerInfo.defenseP.ToString() + "\n" +
            "词条：";
        foreach(var entry in EntryManager.instance.entries)
        {
            infoText.text += entry.name;
        }
    }
}
