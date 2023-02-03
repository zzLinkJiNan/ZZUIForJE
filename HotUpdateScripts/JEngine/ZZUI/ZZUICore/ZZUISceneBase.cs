using UnityEngine;
using UnityEngine.UI;

namespace JEngine.ZZUi
{
    public class ZZUISceneBase : ZZUIScenePort
    {
        public override void Init(){Ini();}
        public override void Loop(){OnUpdateUI();}
        public override void End(){OnChangeScene();}

        //Scene初始化流程
        public override void Ini()
        {
            IniDeploy();
            Iniparameter();
            SetModles();
            OnAddEvent();
            OnShowed();
        }

        //初始化面板配置
        public override void IniDeploy()
        {
            skinTr = gameObject.transform;
            mainMask = gameObject.transform.parent.Find("mainMask").GetComponent<Image>();
            mainMask.color = maskColor;
            mainMask.enabled = maskIsOn;
        }

        //初始化面板参数
        public override void Iniparameter()
        {

        }

        //面板被销毁
        public override void OnChangeScene()
        {

        }

        //scenemanager中管理
        public override void OnHideUI()
        {

        }

        //scenemanager中管理
        public override void OnShowUI()
        {

        }

        public override void OnUpdateUI()
        {

        }

        //组件获取
        public override void SetModles()
        {

        }

        //添加事件
        public override void OnAddEvent()
        {

        }

        //UI显示完成
        public override void OnShowed()
        {

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


