using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ShowColorPanel : BasePanel
{
    private static string name = "ShowColorPanel";
    private static string path = "Panel/ShowColorPanel";
    public static readonly UIType uIType = new UIType(path, name);
    private Button CloseBtn;
    //系统颜色库按钮
    private Button SystemColorBtn;
    //PaintLibrary
    private Transform PaintLibrary;
    //系统库关闭按钮
    private Button PaintClose;
    //ColorContent
    private Transform ColorContent;
    private Dictionary<int, ColorData> m_ColorDataDic = new Dictionary<int, ColorData>();

    public ShowColorPanel() : base(uIType)
    {

    }
    public override void OnDestory()
    {
        base.OnDestory();
    }
    public override void OnDisable()
    {
        Debug.Log("ShowColorPanel is back");
        base.OnDisable();
    }
    public override void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnStart()
    {
        base.OnStart();
        CloseBtn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "CloseBtn");
        CloseBtn.onClick.AddListener(OnCloseBtnClick);
        SystemColorBtn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "SystemColorBtn");
        SystemColorBtn.onClick.AddListener(OnSystemColorBtnClick);
        PaintLibrary = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Transform>(ActiveObj, "PaintLibrary");
        ColorContent = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Transform>(ActiveObj, "ColorContent");
        PaintClose = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "PaintClose");
        PaintClose.onClick.AddListener(OnPaintCloseBtnClick);
        PaintLibrary.gameObject.SetActive(false);
    }

    private void OnPaintCloseBtnClick() 
    {
        PaintLibrary.gameObject.SetActive(false);
    }

    private void OnSystemColorBtnClick()
    {
        if (PaintLibrary.gameObject.activeSelf) 
        {
            PaintLibrary.gameObject.SetActive(false);
        }
        else 
        {
            PaintLibrary.gameObject.SetActive(true);
            RefreshPaintLibrary();
        }
    }
    private void RefreshPaintLibrary()
    {
        //刷新颜色库

        var ChildCount = ColorContent.childCount;
        m_ColorDataDic = GameRoot.GetInstance().DataManger_Root.GetAllData();
        var Count = m_ColorDataDic.Count;
        for (int i = 0; i < ChildCount; i++)
        {
            var child = ColorContent.GetChild(i);
            if (i < m_ColorDataDic.Count)
            {
                child.gameObject.SetActive(true);
                SetItem(i + 1, child, m_ColorDataDic[i + 1]);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    private void SetItem(int id, Transform transform, ColorData colorData)
    {
        Transform ColorName = transform.GetChild(0);
        Transform ColorValue = transform.GetChild(1);
        Button AttrBtn = transform.GetChild(2).GetComponent<Button>();
        if (ColorName != null)
        {
            ColorName.GetComponent<Text>().text = colorData.Name;
        }
        if (ColorValue != null)
        {
            Color _newColor;
            ColorUtility.TryParseHtmlString(colorData.RGB, out _newColor);
            ColorValue.GetComponent<Image>().color = _newColor;
        }
        AttrBtn.onClick.AddListener(() =>
        {
            Color color;
            ColorUtility.TryParseHtmlString(colorData.RGB, out color);
            GameRoot.GetInstance().Condiment.GetComponent<MeshRenderer>().material.color = color;
            PaintLibrary.gameObject.SetActive(false);
        });
    }
    private void OnCloseBtnClick()
    {
#if UNITY_EDITOR
        Debug.Log("关闭");
#endif
        Application.Quit();
    }
}
