using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JEngine.Core;
using System;
using DG.Tweening;
using System.Reflection;
namespace JEngine.ZZUi
{
    public class ZZPanelManager : MonoSingleton<ZZPanelManager>
    {

        //Panel字典
        private Dictionary<ZZPanelName, Transform> panelWareHouse = new Dictionary<ZZPanelName, Transform>();

        //创建一个panel
        public void CreatePanel(ZZPanelName panelName, Action<string> action = null, params object[] objs)
        {
            //判重
            Transform choosePanel = null;
            panelWareHouse.TryGetValue(panelName, out choosePanel);
            if (choosePanel) { Debug.Log("已存在该panel 请不要反复生成"); return; }

            Transform zPanel = ZZCanvasManager.Instance.ZPanel;

            string pName = panelName.ToString();

            new JPrefab("Assets/HotUpdateResources/Prefab/UIMainPrefabs/MainCanvas/Panel_Base.prefab",(result, panelBase) => {
                //实例化panelBase
                Transform panelB = panelBase.Instantiate(zPanel, pName).transform;
                new JPrefab("Assets/HotUpdateResources/Prefab/UIMainPrefabs/Panels/" + pName + ".prefab",(result , panelGo) => {
                    //实例化该panel
                    Transform panel = panelGo.Instantiate(panelB, pName).transform;
                    panelWareHouse.Add(panelName, panelB);
                    //添加脚本
                    var panelScript = Type.GetType(pName);
                    ZZUIPanelBase zusb = panel.gameObject.CreateJBehaviour(panelScript,false) as ZZUIPanelBase;
                    //绑定事件
                    BindingEvent(panel);
                    //脚本赋值
                    zusb.objs = objs;
                    zusb.ac = action;
                    zusb.Activate();
                });
            });
        }
        public void CreatePanel(ZZPanelName panelName, params object[] objs)
        {
            CreatePanel(panelName, null, objs);
        }

        //关闭某个panel
        public void ClosePanel(ZZPanelName panelName)
        {
            Transform choosePanel = null;
            panelWareHouse.TryGetValue(panelName, out choosePanel);
            if (choosePanel != null)
            {
                choosePanel.SearchGetJB<ZZUIPanelBase>(panelName.ToString()).OnClose();
            }
            else
                Debug.Log("没有找到这个panel被打开");
        }

        public void BindingEvent(Transform parent)
        {
            Transform[] children = parent.GetComponentsInChildren<Transform>();
            foreach (var item in children)
            {
                if (item.name.StartsWith("Btn_"))
                {
                    UIEventListener ul = item.gameObject.CreateJBehaviour<UIEventListener>();
                    ul.OnClick += () => { parent.GetComponent<ZZUIPanelBase>().OnClicks(item); };
                    //通过预添加脚本来加载更多的效果
                    UIAniUtil uau = item.GetComponent<UIAniUtil>();
                    if (uau)
                    {
                        ul.uiAniType = uau.IANITYPE;
                        ul.enterReplaceImg = uau.replaceImg;
                        ul.textColor = uau.textColor;
                        ul.imageColor = uau.imageColor;
                    }
                }
            }
        }

        //移除一个panel
        public void removePanelDicOne(string panelName)
        {
            foreach (var item in panelWareHouse)
            {
                if (panelName.Equals(item.Key.ToString()))
                {
                    panelWareHouse.Remove(item.Key);
                    return;
                }
            }
            Debug.LogError("panel字典中未被移除?! 请检查panel字典的问题!!!");
        }

        //显示Panel
        public void ShowPanel(ZZPanelName panelName)
        {
            Transform choosePanel = null;
            panelWareHouse.TryGetValue(panelName, out choosePanel);
            if (choosePanel != null)
            {
                choosePanel.gameObject.SetActive(true);
                choosePanel.SearchGetJB<ZZUIPanelBase>(panelName.ToString()).OnShowUI();
            }
            else
                Debug.Log("没有找到这个panel被打开");
        }
        //隐藏当前Panel
        public void HidePanel(ZZPanelName panelName)
        {
            Transform choosePanel = null;
            panelWareHouse.TryGetValue(panelName, out choosePanel);
            if (choosePanel != null)
            {
                choosePanel.gameObject.SetActive(false);
                choosePanel.SearchGetJB<ZZUIPanelBase>(panelName.ToString()).OnHideUI();
            }
            else
                Debug.Log("没有找到这个panel被打开");
        }
    }
}

