using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PaintLibraryPanel : BasePanel
{
    private static string name = "PaintLibraryPanel";
    private static string path = "Panel/PaintLibraryPanel";
    public static readonly UIType uIType = new UIType(path, name);
    private Button ReturnBtn;
    private Button CloseBtn;

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
        ReturnBtn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "ReturnBtn");
        ReturnBtn.onClick.AddListener(OnReturnBtnClick);
        CloseBtn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "CloseBtn");
        CloseBtn.onClick.AddListener(OnCloseBtnClick);
    }

    private void OnCloseBtnClick()
    {
#if UNITY_EDITOR
        Debug.Log("¹Ø±Õ");
#endif
        Application.Quit();
    }

    private void OnReturnBtnClick()
    {
        GameRoot.GetInstance().UIManager_Root.PopAll();
        StartPanel startPanel = new StartPanel();
        GameRoot.GetInstance().UIManager_Root.Push(startPanel);
    }
}
