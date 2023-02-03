using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using JEngine.Core;
namespace JEngine.ZZUi
{
    public abstract class ZZUIMaskPort : JBehaviour
    {
        //当前Mask的主Transform
        public Transform skinTr;
        //当前Mask的透明度控制
        public CanvasGroup CG;

        //-----当前Mask初始化参数-----
        public float CGAlpha = 0;  //初始mask透明度
                                   //-----↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑-----

        //初始化传递参数
        public object[] objs = new object[] { };
        //UI回调
        public Action<string> ac = null;

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

        //添加事件
        public abstract void OnAddEvent();

        //完全展示后
        public abstract void OnShowed();

        //消失
        public abstract void OnClose();

        //点击事件装载
        public abstract void OnClicks(Transform btnClick);
    }
}

