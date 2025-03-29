using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OutfitSO",menuName = "SoAssets/OutfitSO")]
public class OutfitSO : ScriptableObject
{
    public List<OutfitInfo> outfitInfos = new List<OutfitInfo>();
}
