using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QualityColorSO",menuName = "SoAssets/QualityColorSO")]
public class QualityColorSO : ScriptableObject
{
    public List<QualityColor> qualityToColor = new List<QualityColor>();
}
[System.Serializable]
public class QualityColor
{
    public Quality quality;
    public Color color;
}