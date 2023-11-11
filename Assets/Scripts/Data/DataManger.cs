using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManger
{
    private Dictionary<int, ColorData> m_DicColorData = new Dictionary<int, ColorData>();

    public void Init()
    {
        m_DicColorData.Clear();
        ColorData colorData1 = new ColorData("白色", "#FFFFFF", "无","无", "无", "无");
        ColorData colorData2 = new ColorData("红色", "#FF0000", "无","无", "无", "无");
        ColorData colorData3 = new ColorData("黄色", "#FFFF00", "无","无", "无", "无");
        ColorData colorData4 = new ColorData("蓝色", "#0000FF", "无","无", "无", "无");
        m_DicColorData.Add(1, colorData1);
        m_DicColorData.Add(2, colorData2);
        m_DicColorData.Add(3, colorData3);
        m_DicColorData.Add(4, colorData4);
    }
    public ColorData GetColorData(int id) 
    {
        ColorData data;
        if (m_DicColorData.TryGetValue(id,out data)) 
        {
            return data;
        }
        Debug.Log($"当前id为{id}的颜色数据是空的");
        return data;
    }
    public Dictionary<int, ColorData> GetAllData() 
    {
        if (m_DicColorData != null) 
        {
            return m_DicColorData;
        }
        Dictionary<int,ColorData> m_dic = new Dictionary<int,ColorData>();
        return m_dic;
    }

    public void AddColorData(ColorData colorData) 
    {
        if (!m_DicColorData.ContainsValue(colorData))
        {
            int index = m_DicColorData.Keys.Count;
            m_DicColorData.Add(index + 1, colorData);
        }
        else 
        {
            Debug.LogError("添加颜色失败");
        }
    }
    public void RemoveColorData(int id) 
    {
        m_DicColorData.Remove(id);
    }

    public void EditColorData(int id,ColorData colorData) 
    {
        if (id < 0 || id > m_DicColorData.Count)
        {
            Debug.LogError("当前编辑数据出错了，请检查");
        }
        else
        {
            m_DicColorData[id] = colorData;
        }
    }
    #region 获取颜色数据
    public string GetColorName(int id) 
    {
        string name = "";
        if (m_DicColorData != null) 
        {
            name = m_DicColorData[id].Name;
        }
        return name;
    }
    public string GetColorRGB(int id)
    {
        string rgb = "";
        if (m_DicColorData != null)
        {
            rgb = m_DicColorData[id].RGB;
        }
        return rgb;
    }
    public string GetColorChengFen(int id)
    {
        string chengfen = "";
        if (m_DicColorData != null)
        {
            chengfen = m_DicColorData[id].ChengFen;
        }
        return chengfen;
    }
    public string GetColorTeXing(int id) 
    {
        string texing = "";
        if (m_DicColorData != null)
        {
            texing = m_DicColorData[id].TeXing;
        }
        return texing;
    }
    public string GetColorJiaGe(int id)
    {
        string jiage = "";
        if (m_DicColorData != null)
        {
            jiage = m_DicColorData[id].JiaGe;
        }
        return jiage;
    }
    public string GetColorBeiZhu(int id)
    {
        string beizhu = "";
        if (m_DicColorData != null)
        {
            beizhu = m_DicColorData[id].BeiZhu;
        }
        return beizhu;
    }


    #endregion
}
