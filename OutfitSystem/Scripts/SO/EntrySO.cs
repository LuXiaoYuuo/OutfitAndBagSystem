using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntrySO",menuName = "SoAssets/EntrySO")]
public class EntrySO : ScriptableObject
{
    public List<Entry> entryPool = new List<Entry>();
}
