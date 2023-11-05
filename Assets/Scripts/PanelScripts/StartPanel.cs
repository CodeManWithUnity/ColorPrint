using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    private static string name = "StartPanel";
    private static string path = "Panel/StartPanel";
    public static readonly UIType uIType = new UIType(path, name);
    //看色与展色按钮
    private Button ColorShowBtn;
    //涂料库管理按钮
    private Button ColorFormulaBtn;
    //材料选择按钮
    private Button ColorMaterialSelBtn;
    //调色仿色训练
    private Button ColorCombinationBtn;
    //关闭按钮
    private Button CloseBtn;
    public StartPanel() : base(uIType)
    {

    }
    public override void OnDestory()
    {
        base.OnDestory();
    }
    public override void OnDisable()
    {
        Debug.Log("StartPanel is back");
        base.OnDisable();
    }
    public override void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnStart()
    {
        base.OnStart();
        ColorShowBtn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "ColorShowBtn");
        ColorShowBtn.onClick.AddListener(OnColorShowClick);
        ColorFormulaBtn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "ColorFormulaBtn");
        ColorFormulaBtn.onClick.AddListener(OnColorFormulaClick);
        ColorMaterialSelBtn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "ColorMaterialSelBtn");
        ColorMaterialSelBtn.onClick.AddListener(OnColorMaterialSelClick);
        ColorCombinationBtn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "ColorCombinationBtn");
        ColorCombinationBtn.onClick.AddListener(OnColorCombinationClick);
        CloseBtn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "CloseBtn");
        CloseBtn.onClick.AddListener(OnCloseClick);

    }       
    public void OnColorShowClick() 
    {
        Debug.Log("展色按钮点击");
        GameRoot.GetInstance().UIManager_Root.PopAll();
        ShowColorPanel showColorPanel = new ShowColorPanel();
        GameRoot.GetInstance().UIManager_Root.Push(showColorPanel);
    }
    public void OnColorFormulaClick() 
    {
        Debug.Log("涂料库管理按钮点击");
        GameRoot.GetInstance().UIManager_Root.PopAll();
        PaintLibraryPanel paintLibraryPanel = new PaintLibraryPanel();
        GameRoot.GetInstance().UIManager_Root.Push(paintLibraryPanel);
    }
    public void OnColorMaterialSelClick()
    {
        Debug.Log("材料选择按钮点击");
    }
    public void OnColorCombinationClick() 
    {
        Debug.Log("调色仿色训练按钮点击");
    }
    public void OnCloseClick() 
    {
#if UNITY_EDITOR
        Debug.Log("关闭");
#endif
        Application.Quit();
    }
}
