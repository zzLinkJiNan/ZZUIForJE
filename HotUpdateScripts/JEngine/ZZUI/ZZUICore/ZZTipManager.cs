using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;
using JEngine.Core;
namespace JEngine.ZZUi
{
    //Tip确认位置
    public enum TipShowLocation
    {
        居中,
        在上方往下叠加,
        在上方,
    }
    //Tip确认具体效果
    public enum TipShowType
    {
        渐出渐隐,
        变大变小
    }

    //每个Tip的生命周期在 Manager 中管理 ↓
    public class ZZTipManager : MonoSingletonDontDes<ZZTipManager>
    {

        Transform zTip = null;

        //创建一个Tip 
        public void CreateTip(ZZTipName tipName, string tipEx,
        TipShowLocation showLocation = TipShowLocation.在上方往下叠加,
        TipShowType showType = TipShowType.变大变小
        , Action<bool> isClose = null)
        {

            zTip = ZZCanvasManager.Instance.ZTip;

            string tName = tipName.ToString();

            Transform tipParent = null;

            //抉择位置
            switch (showLocation)
            {
                case TipShowLocation.在上方:
                    tipParent = zTip.SearchGet<Transform>("Top_Tip");
                    break;
                case TipShowLocation.在上方往下叠加:
                    tipParent = zTip.SearchGet<Transform>("Top_TipOverlay");
                    break;
                case TipShowLocation.居中:
                    tipParent = zTip.SearchGet<Transform>("Center_Tip");
                    break;
            }

            //创建tip
            GameObject tip = createTipInTo(tName, tipParent);

            //文本输入
            tip.transform.SearchGet<Text>("txt").text = tipEx;

            //tip效果
            ShowAniAndClose(tip, showType, isClose);
        }

        private GameObject createTipInTo(string tipName, Transform parent)
        {
            var prefab = new JPrefab("Assets/HotUpdateResources/Prefab/UIMainPrefabs/Tips/" + tipName + ".prefab", true);
            GameObject go = prefab.Instantiate(tipName);
            go.GetComponent<RectTransform>().localPosition = Vector3.zero;
            return go;
        }

        private void ShowAniAndClose(GameObject tip, TipShowType showType, Action<bool> isClose = null)
        {
            RectTransform rt = tip.GetComponent<RectTransform>();
            Sequence sq = DOTween.Sequence();
            switch (showType)
            {
                case TipShowType.变大变小:
                    Vector2 originalSize = rt.sizeDelta;
                    rt.sizeDelta = Vector2.zero;
                    sq.Append(rt.DOSizeDelta(originalSize, 0.5f));
                    sq.AppendInterval(3);
                    sq.Append(rt.DOSizeDelta(Vector2.zero, 0.5f));
                    sq.OnComplete(() => { isClose?.Invoke(true); Destroy(rt.gameObject); });
                    break;
                case TipShowType.渐出渐隐:
                    CanvasGroup CG = rt.GetOrAddComponent<CanvasGroup>();
                    CG.alpha = 0;
                    sq.Append(DOTween.To(() => CG.alpha, p => CG.alpha = p, 1, 0.5f));
                    sq.AppendInterval(3);
                    sq.Append(DOTween.To(() => CG.alpha, p => CG.alpha = p, 0, 0.5f));
                    sq.OnComplete(() => { isClose?.Invoke(true); Destroy(rt.gameObject); });
                    break;
            }
        }

    }
}


