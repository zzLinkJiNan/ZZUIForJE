using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using JEngine.ZZUi;
using JEngine.Core;

public class Panel_lalala : ZZUIPanelBase
{
    //----------成员组件 | 变量-----------
    UIEventListener Btn_Close;UIEventListener Btn_lala;
    //----------↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑-----------

    //初始化配置
    public override void IniDeploy()
    {
        maskIsOn = false; //遮罩是否打开
        maskColor = new Color(0f,0f,0f,0.2f); //遮罩颜色 RGBA : 0~1
        clickClose = true; //点击其他地方关闭当前panel
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
		Btn_Close = skinTr.SearchGetJB<UIEventListener>("Btn_Close");
		Btn_lala = skinTr.SearchGetJB<UIEventListener>("Btn_lala");

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
			case "Btn_Close":
				OnClose();
			break;
			case "Btn_lala":
				Debug.Log("你好");
			break;
	
        }
    }

    //Panel完成显示后
    public override void OnShowed()
    {
        base.OnShowed();

    }
}
