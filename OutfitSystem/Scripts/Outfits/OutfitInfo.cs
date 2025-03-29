using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// װ�����ͻ���
/// </summary>
public class OutfitInfo : PropBase
{
    public OutfitType outfitType;
    public List<string> entryNamePool = new List<string>();//�������ֳ� ����������ô���
    public List<Entry> entries = new List<Entry>();//������ ���д������������
    /// <summary>
    /// �˴���ʾ��װ������Ч�����Ӧ��ֵ ͬ���� �ֵ������� ����Unity�ļ�������ڲ�֧���ֵ�����ʾ
    /// </summary>
    public List<GainType> gainTypes = new List<GainType>();
    public List<float> gainValue = new List<float>();

}
