using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JEngine.Core;
using System;
using DG.Tweening;
using System.Reflection;
namespace JEngine.ZZUi
{
    public class ZZSceneManager : MonoSingletonDontDes<ZZSceneManager>
    {

        //当前SceneTr
        public Transform currentScene;

        //Scene栈
        private List<ZZSceneName> sceneWareHouse = new List<ZZSceneName>();

        //选择SceneUI
        public void ChooseScene(ZZSceneName sceneName, Action<string> action = null, params object[] objs)
        {
            //选择一个 scene 要干掉之前的 scene 加载新的 scene
            if (currentScene == null && sceneWareHouse.Count == 0)
                createScene(sceneName, action, objs);
            else if (currentScene != null && sceneWareHouse.Count > 0)
            {
                Destroy(currentScene.gameObject);
                createScene(sceneName, action, objs);
            }
        }
        public void ChooseScene(ZZSceneName sceneName, params object[] objs)
        {
            ChooseScene(sceneName, null, objs);
        }

        //切回到上个SceneUI
        public void cutUpScene(Action<string> action = null, params object[] objs)
        {
            if (sceneWareHouse.Count > 1)
            {
                //scene1 scene2 
                ChooseScene(sceneWareHouse[sceneWareHouse.Count - 2], action, objs);
                sceneWareHouse.RemoveRange(sceneWareHouse.Count - 2, 2);
            }
            else
                Debug.Log("不足两个SceneUI");
        }

        //创建一个scene
        private void createScene(ZZSceneName sceneName, Action<string> action = null, params object[] objs)
        {

            Transform zScene = ZZCanvasManager.Instance.ZScene;

            string sName = sceneName.ToString();

            new JPrefab("Assets/HotUpdateResources/Prefab/UIMainPrefabs/MainCanvas/Scene_Base.prefab", (result, sceneBase) => {
                //实例化sceneBase
                Transform sceneB = sceneBase.Instantiate(zScene, sName).transform;
                new JPrefab("Assets/HotUpdateResources/Prefab/UIMainPrefabs/Scenes/" + sName + ".prefab", (result, sceneGo) => {
                    //实例化该scene
                    Transform scene = sceneGo.Instantiate(sceneB, sName).transform;
                    sceneWareHouse.Add(sceneName);
                    currentScene = sceneB;
                    //反射拿到脚本类型
                    var sceneType = Type.GetType(sName);
                    //取到UI基类
                    ZZUISceneBase sceneScr = scene.gameObject.CreateJBehaviour(sceneType,false) as ZZUISceneBase;
                    //绑定事件
                    //BindingEvent(scene);
                    //脚本赋值
                    sceneScr.objs = objs;
                    sceneScr.ac = action;
                    sceneScr.Activate();
                });
            });
        }

        public void BindingEvent(Transform parent)
        {
            Transform[] children = parent.GetComponentsInChildren<Transform>();
            foreach (var item in children)
            {
                if (item.name.StartsWith("Btn_"))
                {
                    UIEventListener ul = item.gameObject.CreateJBehaviour<UIEventListener>();
                    ul.OnClick += () => { parent.GetComponent<ZZUISceneBase>().OnClicks(item); };
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

        //显示当前Scene
        public void ShowScene()
        {
            if (currentScene != null)
            {
                currentScene.gameObject.SetActive(true);
                currentScene.SearchGet<Transform>(sceneWareHouse[sceneWareHouse.Count - 1].ToString()).GetComponent<ZZUISceneBase>().OnShowUI();
            }
            else
                Debug.Log("未找到当前Scene 无法显示");
        }
        //隐藏当前Scene
        public void HideScene()
        {
            if (currentScene != null)
            {
                currentScene.gameObject.SetActive(false);
                currentScene.SearchGet<Transform>(sceneWareHouse[sceneWareHouse.Count - 1].ToString()).GetComponent<ZZUISceneBase>().OnHideUI();
            }
            else
                Debug.Log("未找到当前Scene 无法隐藏");
        }
    }
}

