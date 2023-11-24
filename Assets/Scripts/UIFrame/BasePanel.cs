using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePanel
{
    //用于作为每个Panel的父类为其提供virtual方法和ActiveObj

    public UIType uiType;
    //此Panel在场景里对应的具体的一个物体
    public GameObject ActiveObj;

    public BasePanel(UIType uitype)
    {
        uiType = uitype;
    }
    public virtual void OnStart()
    {
        Debug.Log($"{uiType.Name}开始使用！");
        UIMethods.GetInstance().GetorAddComponent<CanvasGroup>(ActiveObj).interactable  = true;
    }
    public virtual void OnEnable()
    {
        UIMethods.GetInstance().GetorAddComponent<CanvasGroup>(ActiveObj).interactable = true;
    }
    public virtual void OnDisable()
    {
        UIMethods.GetInstance().GetorAddComponent<CanvasGroup>(ActiveObj).interactable = false;
    }
    public virtual void OnDestory()
    {
        UIMethods.GetInstance().GetorAddComponent<CanvasGroup>(ActiveObj).interactable = false;
    }
    public virtual void ToggleChange<T>(Button LastBtn, Button NowBtn, T NowPanel) where T : BasePanel
    {

        if (LastBtn == NowBtn)
        {
            Debug.Log("更改 ");
            return;
        }
        NowBtn.transform.GetChild(0).GetComponent<Text>().color = Color.yellow;
        if (LastBtn != null && GameRoot.GetInstance().UIManager_Root.stack_ui.Peek().uiType.Name != "StartPanel")
        {
            LastBtn.transform.GetChild(0).GetComponent<Text>().color = Color.white;
            Debug.Log("执行了");
            GameRoot.GetInstance().UIManager_Root.PopSingle();
        }

        GameRoot.GetInstance().UIManager_Root.Push(NowPanel);
    }
}