using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutfitInfoView : MonoBehaviour
{
    [Header("数据")]
    public OutfitInstance m_OutfitInstance;
    [Header("组件")]
    public Image icon;
    public Image quality;
    public TextMeshProUGUI outfitName;
    public TextMeshProUGUI discription;
    public TextMeshProUGUI entryList;

    public void Refresh(OutfitInstance outfitInstance)
    {
        m_OutfitInstance = outfitInstance;
        icon.sprite = outfitInstance.outfitInfo.icon;
        quality.color = OutfitManager.instance.GetQualityColor(m_OutfitInstance.outfitInfo.quality);
        outfitName.text = outfitInstance.outfitInfo.name;
        discription.text = outfitInstance.outfitInfo.discription;
        entryList.text = "";
        foreach(var entry in outfitInstance.outfitInfo.entryNamePool)
        {
            Entry _entry = EntryManager.instance.GetEntry(entry);
            entryList.text += "|" + _entry.name + "|" + _entry.discription+"\n";
        }
    }
}
