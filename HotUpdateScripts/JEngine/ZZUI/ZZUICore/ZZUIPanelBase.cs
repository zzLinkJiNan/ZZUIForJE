using JEngine.Core;
using UnityEngine;
using UnityEngine.UI;

namespace JEngine.ZZUi {
    public class ZZUIPanelBase : ZZUIPanelPort
    {
        public override void Init() { Ini(); }
        public override void Loop() { OnUpdateUI(); }

        //Panel初始化流程
        public override void Ini()
        {
            IniDeploy();
            Iniparameter();
            SetModles();
            OnAddEvent();
            OnShowed();
        }

        //初始化panel配置
        public override void IniDeploy()
        {
            skinTr = gameObject.transform;
            mainMask = gameObject.transform.parent.Find("mainMask").GetComponent<Image>();
            mainMask.color = maskColor;
            mainMask.enabled = maskIsOn;
            if (clickClose)
            {
                UIEventListener uel = mainMask.gameObject.CreateJBehaviour<UIEventListener>();
                uel.OnClick += () => { OnClose(); };
                uel.uiAniType = UIANITYPE.NONE;
            }
        }

        public override void Iniparameter()
        {

        }

        //panelmanager中管理
        public override void OnHideUI()
        {

        }

        //panelmanager中管理
        public override void OnShowUI()
        {

        }

        public override void OnUpdateUI()
        {

        }

        public override void SetModles()
        {

        }

        public override void OnAddEvent()
        {

        }

        public override void OnShowed()
        {

        }

        public override void OnClose()
        {
            Object.Destroy(gameObject.transform.parent.gameObject);
            ZZPanelManager.Instance.removePanelDicOne(gameObject.name);
        }

        public override void OnClicks(Transform btnClick)
        {
            switch (btnClick.name)
            {
                case "":

                    break;
            }
        }


    }
}


