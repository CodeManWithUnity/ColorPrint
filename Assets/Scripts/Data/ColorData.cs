using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ColorData
{
    //����
    private string name;
    //rgbֵ
    private string rgb;
    //�ɷ�
    private string chengFen;
    //����
    private string teXing;
    //�۸�
    private string jiaGe;
    //��ע
    private string beiZhu;
    public string Name 
    {
        get { return name; }
        set { name = value; }
    }
    public string RGB
    {
        get { return rgb; }
        set { rgb = value; }
    }
    public string ChengFen
    {
        get { return chengFen; }
        set { chengFen = value; }
    }
    public string TeXing
    {
        get { return teXing; }
        set { teXing = value; }
    }
    public string JiaGe
    {
        get { return jiaGe; }
        set { jiaGe = value; }
    }
    public string BeiZhu 
    {
        get { return beiZhu; }
        set { beiZhu = value; }
    }

    public ColorData(string Name,string Rgb,string ChengFen,string TeXing,string Jiage,string BeiZhu) 
    {
        name = Name;
        rgb = Rgb;
        chengFen = ChengFen;
        teXing = TeXing;
        jiaGe = Jiage;
        beiZhu = BeiZhu;
    }
}
