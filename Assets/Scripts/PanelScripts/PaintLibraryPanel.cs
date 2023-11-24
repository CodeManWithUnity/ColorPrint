using F3Device.Common;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PaintLibraryPanel : BasePanel
{
    private static string name = "PaintLibraryPanel";
    private static string path = "Panel/PaintLibraryPanel";
    public static readonly UIType uIType = new UIType(path, name);
    //关闭按钮
    private Button CloseBtn;
    //PaintLibrary关闭按钮
    private Button PaintClose;
    //SystemColorBtn
    private Button SystemColorBtn;
    //Panel_ColorPicker
    private Transform Panel_ColorPicker;
    //颜色1
    private Image Color1BtnImage;
    //颜色1添加按钮
    private Button AddColor1Btn;
    //颜色2
    private Image Color2BtnImage;
    //颜色2添加按钮
    private Button AddColor2Btn;
    //颜色混合按钮
    private Button BlendColorBtn;
    //颜色混合图片
    private Image BlendColor;
    //ColorContent
    private Transform ColorContent;
    //颜色编辑界面
    private Transform PaintDesc;
    //添加混合颜色
    private Button AddBlendColorBtn;
    //删除颜色库的按钮
    private Button ItemDeleteBtn;

    //
    private Transform ColorEdit;

    private Dictionary<int, ColorData> m_ColorDataDic = new Dictionary<int, ColorData>();
    //PaintLibrary
    private Transform PaintLibrary;



    public PaintLibraryPanel() : base(uIType)
    {

    }
    public override void OnDestory()
    {
        base.OnDestory();
    }
    public override void OnDisable()
    {
        Debug.Log("PaintLibraryPanel is back");
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
        PaintClose = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "PaintClose");
        PaintClose.onClick.AddListener(OnPaintCloseBtnClick);
        PaintLibrary = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Transform>(ActiveObj, "PaintLibrary");
        Panel_ColorPicker = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Transform>(ActiveObj, "Panel_ColorPicker");
        Color1BtnImage = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Image>(ActiveObj, "Color1Btn");
        AddColor1Btn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "AddColor1Btn");
        AddColor1Btn.onClick.AddListener(OnAddColor1BtnClick);
        ColorContent = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Transform>(ActiveObj, "ColorContent");
        Color2BtnImage = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Image>(ActiveObj, "Color2Btn");
        AddColor2Btn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "AddColor2Btn");
        AddColor2Btn.onClick.AddListener(OnAddColor2BtnClick);
        PaintDesc = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Transform>(ActiveObj, "PaintDesc");
        BlendColorBtn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "BlendColorBtn");
        BlendColorBtn.onClick.AddListener(OnBlendColorBtnClick);
        BlendColor = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Image>(ActiveObj, "BlendColor");
        SystemColorBtn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "SystemColorBtn");
        SystemColorBtn.onClick.AddListener(OnSystemColorBtnClick);
        AddBlendColorBtn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "AddBlendColorBtn");
        AddBlendColorBtn.onClick.AddListener(OnAddBlendColorBtnClick);
        ColorEdit = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Transform>(ActiveObj, "ColorEdit");
        ColorEdit.gameObject.SetActive(false);

        PaintLibrary.gameObject.SetActive(false);
        Panel_ColorPicker.gameObject.SetActive(false);
        PaintDesc.gameObject.SetActive(false);
    }

    private void OnAddBlendColorBtnClick()
    {
        Color _color = BlendColor.color;
        string RGB = "#" + ColorUtility.ToHtmlStringRGB(_color);
        AddColorToManger(RGB);
    }

    private void OnAddColor2BtnClick()
    {
        Color _color = Color2BtnImage.color;
        string RGB = "#" + ColorUtility.ToHtmlStringRGB(_color);
        Debug.LogError($"当前的颜色是：{RGB}");
        AddColorToManger(RGB);
    }

    private void OnAddColor1BtnClick()
    {
        Color _color = Color1BtnImage.color;
        string RGB = "#" + ColorUtility.ToHtmlStringRGB(_color);
        AddColorToManger(RGB);
    }
    private void OnBlendColorBtnClick()
    {
        Color color1 = Color1BtnImage.color;
        Color color2 = Color2BtnImage.color;
        Color blendColor = ColorBlend(color1, color2);
        BlendColor.color = blendColor;
    }

    //颜色混合
    private Color ColorBlend(Color color1, Color color2)
    {
        float r = 0;
        float g = 0;
        float b = 0;

        r = color1.r + color2.r;
        g = color1.g + color2.g;
        b = color1.b + color2.b;
        //混合后颜色
        Color AddBlendedColor = new Color(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b), 1);
        return AddBlendedColor;
    }

    //添加颜色到DataManger
    private void AddColorToManger(string rgb)
    {
        string name = "新建颜色";
        string ChengFen = "无";
        string TeXing = "无";
        string JiaGe = "无";
        string BeiZhu = "无";
        ColorData colorData = new ColorData(name, rgb, ChengFen, TeXing, JiaGe, BeiZhu);
        GameRoot.GetInstance().DataManger_Root.AddColorData(colorData);
    }



    private void OnSystemColorBtnClick()
    {
        if (!PaintLibrary.gameObject.activeSelf)
        {
            PaintLibrary.gameObject.SetActive(true);
            RefreshPaintLibrary();
        }
        else
        {
            PaintLibrary.gameObject.SetActive(false);
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
            child.gameObject.name = "item" + (i + 1).ToString();
            if (i < m_ColorDataDic.Count)
            {
                child.gameObject.SetActive(true);
                ColorData colorData = m_ColorDataDic.ElementAt(i).Value;
                int id = m_ColorDataDic.ElementAt(i).Key;
                SetItem(id, child, colorData);
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
        Button ItemDeleteBtn = transform.GetChild(3).GetComponent<Button>();
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
            if (PaintDesc.gameObject.activeSelf)
            {
                PaintDesc.gameObject.SetActive(false);
            }
            else
            {
                PaintDesc.gameObject.SetActive(true);
                SetDescValue(id, colorData);
            }
        });
        ItemDeleteBtn.onClick.AddListener(() =>
        {
            DeleteItemData(id);
        });
    }
    private void DeleteItemData(int id) 
    {
        GameRoot.GetInstance().DataManger_Root.RemoveColorData(id);
        RefreshPaintLibrary();
    }

    private void  SetDescValue(int id, ColorData colorData) 
    {
        //设置属性详情
        InputField NameInput = PaintDesc.GetChild(0).GetChild(0).GetComponent<InputField>();
        InputField RGBInput = PaintDesc.GetChild(1).GetChild(0).GetComponent<InputField>();
        InputField ChengFenInput = PaintDesc.GetChild(2).GetChild(0).GetComponent<InputField>();
        InputField TeXingInput = PaintDesc.GetChild(3).GetChild(0).GetComponent<InputField>();
        InputField JiaGeInput = PaintDesc.GetChild(4).GetChild(0).GetComponent<InputField>();
        InputField BeiZhuInput = PaintDesc.GetChild(5).GetChild(0).GetComponent<InputField>();
        Button SaveBtn = PaintDesc.GetChild(6).GetComponent<Button>();
        NameInput.text = String.IsNullOrEmpty(NameInput.text) ? colorData.Name : NameInput.text;
        RGBInput.text = String.IsNullOrEmpty(RGBInput.text) ? colorData.RGB : RGBInput.text;
        ChengFenInput.text = String.IsNullOrEmpty(ChengFenInput.text) ? colorData.ChengFen : ChengFenInput.text;
        TeXingInput.text = String.IsNullOrEmpty(TeXingInput.text) ? colorData.TeXing : TeXingInput.text;
        JiaGeInput.text = String.IsNullOrEmpty(JiaGeInput.text) ? colorData.JiaGe : JiaGeInput.text;
        BeiZhuInput.text = String.IsNullOrEmpty(BeiZhuInput.text) ? colorData.BeiZhu : BeiZhuInput.text;
        SaveBtn.onClick.RemoveListener(() => { });
        SaveBtn.onClick.AddListener(() =>
        {
            string name = NameInput.text;
            string rgb = RGBInput.text;
            string chengfen = ChengFenInput.text;
            string texing = TeXingInput.text;
            string jiage = JiaGeInput.text;
            string beizhu = BeiZhuInput.text;
            ColorData data = new ColorData(name,rgb,chengfen,texing,jiage,beizhu);
            GameRoot.GetInstance().DataManger_Root.EditColorData(id, data);
            PaintDesc.gameObject.SetActive(false);
            RefreshPaintLibrary();
        });
    }
    private void OnPaintCloseBtnClick()
    {
        if (PaintLibrary != null) 
        {
            PaintLibrary.gameObject.SetActive(false);
        }
    }

    private void OnCloseBtnClick()
    {
#if UNITY_EDITOR
        Debug.Log("关闭");
#endif
        Application.Quit();
    }
}
