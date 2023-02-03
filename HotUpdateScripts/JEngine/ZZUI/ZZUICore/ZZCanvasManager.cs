using UnityEngine;
using System;
using DG.Tweening;
using JEngine.Core;
namespace JEngine.ZZUi
{
    public class ZZCanvasManager : MonoSingletonDontDes<ZZCanvasManager>
    {
        public RectTransform MainCanvas;

        public Transform ZScene;
        public Transform ZPanel;
        public Transform ZMask;
        public Transform ZTip;

        //是否创建出全局唯一Canvas
        public bool isCreateCanvas = false;

        public override void Awake()
        {
            if (!isCreateCanvas)
            {
                createCanvas(go => {
                    MainCanvas = go.GetComponent<RectTransform>();
                    DontDestroyOnLoad(MainCanvas);
                    Debug.Log("找到前");
                    ZScene = MainCanvas.SearchGet<Transform>("ZScene");
                    ZPanel = MainCanvas.SearchGet<Transform>("ZPanel");
                    ZMask = MainCanvas.SearchGet<Transform>("ZMask");
                    ZTip = MainCanvas.SearchGet<Transform>("ZTip");
                    isCreateCanvas = true;
                });
            }
        }


        private void createCanvas(Action<GameObject> canvas)
        {
            new JPrefab("Assets/HotUpdateResources/Prefab/UIMainPrefabs/MainCanvas/MainCanvas.prefab", (result, canvasPrefab) => {
                if (!result)
                {
                    Debug.Log("MainCanvas.prefab 资源没出来");
                    return;
                }
                else
                    canvas?.Invoke(canvasPrefab.Instantiate("MainCanvas"));
            });
        }

    }
}

