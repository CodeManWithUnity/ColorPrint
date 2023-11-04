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
    private Button ReturnBtn;
    private Button CloseBtn;

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
        ReturnBtn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "ReturnBtn");
        ReturnBtn.onClick.AddListener(OnReturnBtnClick);
        CloseBtn = UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "CloseBtn");
        CloseBtn.onClick.AddListener(OnCloseBtnClick);
    }

    private void OnCloseBtnClick()
    {
#if UNITY_EDITOR
        Debug.Log("�ر�");
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