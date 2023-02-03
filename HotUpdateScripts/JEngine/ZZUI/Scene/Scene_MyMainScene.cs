using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using JEngine.ZZUi;
using JEngine.Core;

public class Scene_MyMainScene : ZZUISceneBase
{
    //----------成员组件 | 变量-----------
    UIEventListener Btn_点击按钮1;
    //----------↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑-----------

    //初始化配置
    public override void IniDeploy()
    {
        maskIsOn = false; //遮罩是否打开
        maskColor = new Color(0,0,0,0); //遮罩颜色 RGBA : 0~1
        base.IniDeploy();
    }
	
    //初始化参数
    public override void Iniparameter(){
        if(objs.Length>0){
            
        }
    }

    //组件赋值
    public override void SetModles()
    {
        Btn_点击按钮1 = skinTr.SearchGetJB<UIEventListener>("Btn_点击按钮1");

    }

    //添加事件
    public override void OnAddEvent()
    {
        
    }

    //update
    public override void OnUpdateUI()
    {
        base.OnUpdateUI();
        
    }

    //点击事件装载
    public override void OnClicks(Transform btnClick)
    {
        switch (btnClick.name)
        {
            case "Btn_点击按钮1":
                Debug.Log(Btn_点击按钮1);
            break;
            case "Btn_进入另一个Scene":
                ZZSceneManager.Instance.ChooseScene(ZZSceneName.Scene_Ttest);
            break;
        }
    }

    //Scene被切换后
    public override void OnChangeScene()
    {
        base.OnChangeScene();

    }

    //Scene完成显示后
    public override void OnShowed()
    {
        base.OnShowed();

    }
}
