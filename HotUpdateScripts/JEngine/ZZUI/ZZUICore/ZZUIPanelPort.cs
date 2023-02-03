using UnityEngine;
using System;
using UnityEngine.UI;
using JEngine.Core;

namespace JEngine.ZZUi { 

    public abstract class ZZUIPanelPort : JBehaviour
    {
        //当前Panel的主Transform
        public Transform skinTr;


        //---------Panel配置----------
        public bool maskIsOn = true;  //全屏遮罩是否打开
        public Color maskColor = Color.black; //遮罩颜色
        public bool clickClose = false; //点击其他地方关闭当前panel
        //---------Panel配置----------


        //初始化传递参数
        public object[] objs = new object[]{};
        //UI回调
        public Action<string> ac = null;
        //mainMask
        public Image mainMask = null;

        //初始化
        public abstract void Ini();

        //传递初始化参数
        public abstract void Iniparameter();

        //初始化配置
        public abstract void IniDeploy();

        //设置组件
        public abstract void SetModles();

        //update
        public abstract void OnUpdateUI();

        //隐藏Panel
        public abstract void OnHideUI();

        //显示Panel
        public abstract void OnShowUI();
    
        //干掉Panel
        public abstract void OnClose();

        //添加事件
        public abstract void OnAddEvent();

        //完全展示后
        public abstract void OnShowed();

        //点击事件装载
        public abstract void OnClicks(Transform btnClick);
    }
}