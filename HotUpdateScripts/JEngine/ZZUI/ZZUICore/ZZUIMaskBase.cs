using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JEngine.ZZUi
{
    public class ZZUIMaskBase : ZZUIMaskPort
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

        public override void IniDeploy()
        {
            skinTr = gameObject.transform;
            CG = gameObject.AddComponent<CanvasGroup>();
            CG.alpha = CGAlpha;
        }

        public override void Iniparameter()
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
            Object.Destroy(skinTr.gameObject);
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


