using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JEngine.ZZUi
{
    public class UIAniUtil : MonoBehaviour
    {
        [Header("选择动画效果")]
        public UIANITYPE IANITYPE = UIANITYPE.只有按压;

        [Header("选择点击中替换的图片")]
        public Sprite replaceImg;

        [Header("点击中希望被改变颜色的子Text (请使用C_起头)")]
        public Color textColor = Color.white;

        [Header("点击中希望被改变颜色的子Image (请使用C_起头)")]
        public Color imageColor = Color.white;

    }
}
