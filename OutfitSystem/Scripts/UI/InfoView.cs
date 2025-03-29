using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoView : MonoBehaviour
{
    [Header("����")]
    public TextMeshProUGUI infoText;

    public void OnEnable()
    {
        //Refresh();
    }
    /// <summary>
    /// ˢ������UI
    /// </summary>
    public void Refresh()
    {
        infoText.text = "Ѫ����" + PlayerInfoManager.instance.playerInfo.healthP.ToString() + "\n" +
            "������" + PlayerInfoManager.instance.playerInfo.damageP.ToString() + "\n" +
            "���ף�" + PlayerInfoManager.instance.playerInfo.defenseP.ToString() + "\n" +
            "������";
        foreach(var entry in EntryManager.instance.entries)
        {
            infoText.text += entry.name;
        }
    }
}
