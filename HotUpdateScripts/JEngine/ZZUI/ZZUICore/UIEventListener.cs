using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using JEngine.Core;
namespace JEngine.ZZUi
{


public enum UIANITYPE { 
    NONE,

    只有按压,

    超级按压,

    放上快速放大与归原,

    放上快速放大1点5倍,

    果冻放大与归原,

    放上加描边,

    放上_加描边和按压,

    放上之后向上跳,

    放上之后向上果冻跳,

    点击快速抖动_适合选择错误,


}

public static class UIStaticUtil
{
    /// <summary>
    /// 设置UI动画类型
    /// </summary>
    public static UIEventListener SetAni(this UIEventListener target, UIANITYPE uiAniType)
    {
        target.uiAniType = uiAniType;
        return target;
    }
}

public class UIEventListener : JBehaviour, 
    IPointerClickHandler, 
    IPointerEnterHandler,
    IPointerExitHandler ,
    IPointerDownHandler ,
    IPointerUpHandler
{
    public UIANITYPE uiAniType = UIANITYPE.只有按压;

    /// <summary>
    /// 选中需要替换的图片
    /// </summary>
    public Sprite enterReplaceImg = null;

    /// <summary>
    /// 点击中希望被改变颜色的子Text
    /// </summary>
    public Color textColor = Color.white;
    /// <summary>
    /// 点击中希望被改变颜色的子Image
    /// </summary>
    public Color imageColor = Color.white;

    private Image selfImg;

    /// <summary>
    /// 原图片
    /// </summary>
    private Sprite exitReplaceImg;

    /// <summary>
    /// 原C_text们的颜色 & 组件
    /// </summary>
    public List<Color> exitTextColors = new List<Color>();
    private List<Text> C_Texts = new List<Text>();
    /// <summary>
    /// 原C_image们的颜色
    /// </summary>
    public List<Color> exitImageColors = new List<Color>();
    private List<Image> C_Images = new List<Image>();

    private RectTransform rt;

    private Vector3 oldPo;

    private bool isMouseDown = false;

    public delegate void UIEventProxy();//一个委托对应多个事件
    public event  UIEventProxy OnClick;
    public event UIEventProxy OnMouseEnter;
    public event UIEventProxy OnMouseExit;
    public event UIEventProxy OnMouseDown;
    public event UIEventProxy OnMouseUp;
    public event UIEventProxy OnDragging;

        

    public override void Init()
    {
        rt = gameObject.GetComponent<RectTransform>();

        oldPo = rt.localPosition;

        selfImg = gameObject.GetComponent<Image>();

        exitReplaceImg = selfImg.sprite;

        C_Texts = rt.GetComponentsInPrefixChildren<Text>("C_");
        C_Images = rt.GetComponentsInPrefixChildren<Image>("C_");

        foreach (var item in C_Texts)
            exitTextColors.Add(item.color);

        foreach (var item in C_Images)
            exitImageColors.Add(item.color);
    }

    public override void Loop()
    {
        if (isMouseDown)
            OnDragging?.Invoke();
    }

    public void OnPointerClick(PointerEventData data)
    {
        switch (uiAniType)
        {
            case UIANITYPE.点击快速抖动_适合选择错误:
                rt.DOShakePosition(0.5f, 8, 15, 50, true);
                break;
            default:
                break;
        }

        OnClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        Sequence sq = DOTween.Sequence();

        switch (uiAniType)
        {
            case UIANITYPE.放上快速放大与归原:
                rt.DOScale(Vector3.one * 1.2f, 0.1f);
                break;
            case UIANITYPE.放上快速放大1点5倍:
                rt.DOScale(Vector3.one * 1.5f, 0.1f);
                break;
            case UIANITYPE.果冻放大与归原:

                sq.Append(rt.DOScale(Vector3.one * 1.5f, 0.1f));

                sq.Append(rt.DOScale(Vector3.one * 1.2f, 0.1f));

                sq.Append(rt.DOScale(Vector3.one * 1.3f, 0.1f));

                sq.Append(rt.DOScale(Vector3.one * 1.1f, 0.1f));

                sq.Append(rt.DOScale(Vector3.one * 1.15f, 0.1f));

                sq.Append(rt.DOScale(Vector3.one * 1.2f, 0.1f));

                sq.Append(rt.DOScale(Vector3.one, 0.1f));

                sq.OnComplete(()=>{rt.localScale = Vector3.one;});

                break;
            case UIANITYPE.放上_加描边和按压:
            case UIANITYPE.放上加描边:
                Outline ol = rt.GetOrAddComponent<Outline>();
                ol.effectDistance = Vector2.zero;
                DOTween.To(() => ol.effectDistance, p => ol.effectDistance = p, new Vector2(2,-2), 0.2f);
                ol.DOFade(0.8f,0.2f);
                break;
            case UIANITYPE.放上之后向上跳:
                rt.DOLocalMoveY(oldPo.y + 25,0.4f);
                break;
            case UIANITYPE.放上之后向上果冻跳:

                sq.Append(rt.DOLocalMoveY(oldPo.y + 35, 0.05f));

                sq.Append(rt.DOLocalMoveY(oldPo.y + 15, 0.07f));

                sq.Append(rt.DOLocalMoveY(oldPo.y + 30, 0.07f));

                sq.Append(rt.DOLocalMoveY(oldPo.y + 20, 0.07f));

                sq.Append(rt.DOLocalMoveY(oldPo.y + 25, 0.07f));

                break;
            default:
                break;
        }

        OnMouseEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData data)
    {
        switch (uiAniType)
        {
            case UIANITYPE.放上快速放大与归原:
                rt.DOScale(Vector3.one, 0.05f);
                break;
            case UIANITYPE.放上快速放大1点5倍:
                rt.DOScale(Vector3.one, 0.1f);
                break;
            case UIANITYPE.果冻放大与归原:
                rt.DOScale(Vector3.one, 0.05f);
                break;
            case UIANITYPE.放上_加描边和按压:
            case UIANITYPE.放上加描边:
                Outline ol = rt.GetOrAddComponent<Outline>();
                ol.DOFade(0f, 0.2f);
                DOTween.To(() => ol.effectDistance, p => ol.effectDistance = p, Vector2.zero, 0.2f).OnComplete(()=> {
                    Object.Destroy(ol);
                });
                break;
            case UIANITYPE.放上之后向上果冻跳:
            case UIANITYPE.放上之后向上跳:
                rt.DOLocalMoveY(oldPo.y, 0.4f);
                break;
            default:
                break;
        }
        OnMouseExit?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (uiAniType)
        {
            case UIANITYPE.放上_加描边和按压:
            case UIANITYPE.只有按压:
                rt.DOScale(Vector3.one * 0.95f, 0.05f);
                break;
            case UIANITYPE.超级按压:
                rt.DOScale(Vector3.one * 0.8f, 0.05f);
                break;
            default:
                break;
        }

        //按下换图
        if (enterReplaceImg!=null)
            selfImg.sprite = enterReplaceImg;

        //按下换颜色
        for (int i = 0; i < C_Texts.Count; i++)
        {
            C_Texts[i].DOColor(textColor,0.25f);
        }
        for (int i = 0; i < C_Images.Count; i++)
        {
            C_Images[i].DOColor(imageColor, 0.25f);
        }

        //按下中判定
        isMouseDown = true;

        OnMouseDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Sequence sq = DOTween.Sequence();

        switch (uiAniType)
        {
            case UIANITYPE.放上_加描边和按压:
            case UIANITYPE.只有按压:
                rt.DOScale(Vector3.one, 0.05f);
                break;
            case UIANITYPE.超级按压:
                sq.Append(rt.DOScale(Vector3.one * 1.15f, 0.1f));

                sq.Append(rt.DOScale(Vector3.one * 0.88f, 0.07f));

                sq.Append(rt.DOScale(Vector3.one * 1.08f, 0.05f));

                sq.Append(rt.DOScale(Vector3.one * 0.95f, 0.07f));

                sq.Append(rt.DOScale(Vector3.one, 0.05f));
                break;
            default:
                break;
        }

        //松开改回原图
        selfImg.sprite = exitReplaceImg;

        //松开换回原颜色
        for (int i = 0; i < C_Texts.Count; i++)
        {
            C_Texts[i].DOColor(exitTextColors[i], 0.25f);
        }
        for (int i = 0; i < C_Images.Count; i++)
        {
            C_Images[i].DOColor(exitImageColors[i], 0.25f);
        }

        //按起来判定
        isMouseDown = false;

        OnMouseUp?.Invoke();
    }
}
}