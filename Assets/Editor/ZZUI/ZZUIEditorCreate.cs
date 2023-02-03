using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ZZUIEditorCreate
{

    public static string SceneClassStr =
@"using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using TEngine.Runtime;
using DG.Tweening;
using UnityEngine.UI;

public class #类名# : ZZUISceneBase
{
    //----------成员组件 | 变量-----------
    #成员变量定义#
    //----------↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑-----------

    //初始化配置
    public override void IniDeploy()
    {
        maskIsOn = false; //遮罩是否打开
        maskColor = new Color(0,0,0,0); //遮罩颜色 RGBA : 0~1
        base.IniDeploy();
    }
	
    //初始化参数
    public override void Iniparameter(){
        if(objs.Length>0){
            
        }
    }

    //组件赋值
    public override void SetModles()
    {
#组件赋值#
    }

    //添加事件
    public override void OnAddEvent()
    {
        
    }

    //update
    public override void OnUpdateUI()
    {
        base.OnUpdateUI();
        
    }

    //点击事件装载
    public override void OnClicks(Transform btnClick)
    {
        switch (btnClick.name)
        {
#点击事件装载#
        }
    }

    //Scene被切换后
    public override void OnChangeScene()
    {
        base.OnChangeScene();

    }

    //Scene完成显示后
    public override void OnShowed()
    {
        base.OnShowed();

    }
}
";
    
    public static string PanelClassStr =
@"using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using TEngine.Runtime;
using DG.Tweening;
using UnityEngine.UI;

public class #类名# : ZZUIPanelBase
{
    //----------成员组件 | 变量-----------
    #成员变量定义#
    //----------↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑-----------

    //初始化配置
    public override void IniDeploy()
    {
        maskIsOn = false; //遮罩是否打开
        maskColor = new Color(0,0,0,0); //遮罩颜色 RGBA : 0~1
        clickClose = false; //点击其他地方关闭当前panel
        base.IniDeploy();
    }
	
    //初始化参数
    public override void Iniparameter(){
        if(objs.Length>0){
            
        }
    }

    //组件赋值
    public override void SetModles()
    {
#组件赋值#
    }

    //添加事件
    public override void OnAddEvent()
    {
        
    }

    //update
    public override void OnUpdateUI()
    {
        base.OnUpdateUI();
        
    }

    //点击事件装载
    public override void OnClicks(Transform btnClick)
    {
        switch (btnClick.name)
        {
#点击事件装载#
        }
    }

    //Panel完成显示后
    public override void OnShowed()
    {
        base.OnShowed();

    }
}
";

public static string MaskClassStr =
@"using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using TEngine.Runtime;
using DG.Tweening;
using UnityEngine.UI;

public class #类名# : ZZUIMaskBase
{
    //----------成员组件 | 变量-----------
    #成员变量定义#
    //----------↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑-----------

    //初始化配置
    public override void IniDeploy()
    {
        CGAlpha = 0; //初始mask透明度
        base.IniDeploy();
    }
	
    //初始化参数
    public override void Iniparameter(){
        if(objs.Length>0){
            
        }
    }

    //组件赋值
    public override void SetModles()
    {
#组件赋值#
    }

    //添加事件
    public override void OnAddEvent()
    {
        
    }

    //update
    public override void OnUpdateUI()
    {
        base.OnUpdateUI();
        
    }

    //点击事件装载
    public override void OnClicks(Transform btnClick)
    {
        switch (btnClick.name)
        {
#点击事件装载#
        }
    }

    //Mask完成显示后
    public override void OnShowed()
    {
        base.OnShowed();

    }
}
";
    
    //返回组件前缀
    public static string prefixReturnFullName(string cName){
        switch (cName)
        {
            case "Btn":
                return "UIEventListener";
            case "Tex":
                return "Text";
            case "Tran":
                return "Transform";
            case "Img":
                return "Image";
            default:
                return "";
        }      
    }

    [MenuItem("ZZUI/创建Scene|Panel|Mask|Tip %F9")]
    public static void BuildUIScript()
    {
        GameObject selectobj = Selection.gameObjects[0]; 

        //所有的子 一会儿做代码数据绑定
        Transform [] childrens = selectobj.GetComponentsInChildren<Transform>();

        if(selectobj.name.StartsWith("Scene_")){
            string sceneScriptStr = SceneClassStr;

            string scriptPath = Application.dataPath + "/TResources/Scripts/UIScripts/Scene/" + selectobj.name + ".cs";

            string enumNameScriptPath = Application.dataPath + "/TResources/Scripts/UIScripts/ZZUICore/ZZUIName.cs";

            string 组件赋值 = "";

            string 成员变量定义 = "";

            string 点击事件装载 = "";

            for (int i = 0; i < childrens.Length; i++)
            {
                string cName = prefixReturnFullName(childrens[i].name.Split('_')[0]);
                if(cName!="")
                {
                    组件赋值 += "\t\t"+childrens[i].name 
                    + " = skinTr.SearchGet<"+ cName 
                    +">(\""+childrens[i].name+"\");" + "\n";
                    成员变量定义 += cName + " " + childrens[i].name + ";";
                    if(cName == "UIEventListener"){
                        点击事件装载 += "\t\t\tcase \"" + childrens[i].name + "\":\n\t\t\t\t\n\t\t\tbreak;\n";
                    }
                }
            }

            sceneScriptStr = sceneScriptStr.Replace("#组件赋值#",组件赋值);
            sceneScriptStr = sceneScriptStr.Replace("#成员变量定义#",成员变量定义);
            sceneScriptStr = sceneScriptStr.Replace("#点击事件装载#",点击事件装载);
            sceneScriptStr = sceneScriptStr.Replace("#类名#",selectobj.name);
            
            FileStream scriptFile = new FileStream(scriptPath, FileMode.CreateNew);
            StreamWriter fileWrite = new StreamWriter(scriptFile, System.Text.Encoding.UTF8);

            //读取并写入Enum
            string enumScriptStr = File.ReadAllText(enumNameScriptPath,System.Text.Encoding.UTF8);
            enumScriptStr = enumScriptStr.Replace("SLastFind",selectobj.name+",\nSLastFind");
            
            FileStream enumNameScriptFile = new FileStream(enumNameScriptPath,FileMode.Create);
            StreamWriter enumScriptWrite = new StreamWriter(enumNameScriptFile, System.Text.Encoding.UTF8);


            fileWrite.Write(sceneScriptStr);
            enumScriptWrite.Write(enumScriptStr);


            fileWrite.Flush();
            enumScriptWrite.Flush();
            fileWrite.Close();
            scriptFile.Close();
            enumScriptWrite.Close();
            enumNameScriptFile.Close();

            //创建Prefab
            string prefabPath = "Assets/TResources/Prefabs/UIMainPrefabs/Scenes/"+selectobj.name + ".prefab";

            bool createPrefabSucceed = false;  
            PrefabUtility.SaveAsPrefabAsset(selectobj,prefabPath,out createPrefabSucceed);

            if(createPrefabSucceed) 
                Debug.Log("创建scene预制体成功!"+prefabPath);

            Debug.Log("创建脚本 " + Application.dataPath + "/Scripts/" + selectobj.name + ".cs 成功!");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            
        }
        else if(selectobj.name.StartsWith("Panel_")){
            string panelScriptStr = PanelClassStr;

            string scriptPath = Application.dataPath + "/TResources/Scripts/UIScripts/Panel/" + selectobj.name + ".cs";

            string enumNameScriptPath = Application.dataPath + "/TResources/Scripts/UIScripts/ZZUICore/ZZUIName.cs";

            string 组件赋值 = "";

            string 成员变量定义 = "";

            string 点击事件装载 = "";

            for (int i = 0; i < childrens.Length; i++)
            {
                string cName = prefixReturnFullName(childrens[i].name.Split('_')[0]);
                if(cName!="")
                {
                    组件赋值 += "\t\t"+ childrens[i].name 
                    + " = skinTr.SearchGet<"+ cName 
                    +">(\""+childrens[i].name+"\");" + "\n";
                    成员变量定义 += cName + " " + childrens[i].name + ";";
                    if(cName == "UIEventListener"){
                        if(childrens[i].name.Equals("Btn_Close"))
                        {
                            点击事件装载 += "\t\t\tcase \"" + childrens[i].name + "\":\n\t\t\t\tOnClose();\n\t\t\tbreak;\n\t";
                            continue;
                        }
                        点击事件装载 += "\t\tcase \"" + childrens[i].name + "\":\n\t\t\t\t\n\t\t\tbreak;\n\t";
                    }
                }
            }

            panelScriptStr = panelScriptStr.Replace("#组件赋值#",组件赋值);
            panelScriptStr = panelScriptStr.Replace("#成员变量定义#",成员变量定义);
            panelScriptStr = panelScriptStr.Replace("#点击事件装载#",点击事件装载);
            panelScriptStr = panelScriptStr.Replace("#类名#",selectobj.name);
            
            FileStream scriptFile = new FileStream(scriptPath, FileMode.CreateNew);
            StreamWriter fileWrite = new StreamWriter(scriptFile, System.Text.Encoding.UTF8);

            //读取并写入Enum
            string enumScriptStr = File.ReadAllText(enumNameScriptPath,System.Text.Encoding.UTF8);
            enumScriptStr = enumScriptStr.Replace("PLastFind",selectobj.name+",\nPLastFind");
            
            FileStream enumNameScriptFile = new FileStream(enumNameScriptPath,FileMode.Create);
            StreamWriter enumScriptWrite = new StreamWriter(enumNameScriptFile, System.Text.Encoding.UTF8);


            fileWrite.Write(panelScriptStr);
            enumScriptWrite.Write(enumScriptStr);


            fileWrite.Flush();
            enumScriptWrite.Flush();
            fileWrite.Close();
            scriptFile.Close();
            enumScriptWrite.Close();
            enumNameScriptFile.Close();

            //创建Prefab
            string prefabPath = "Assets/TResources/Prefabs/UIMainPrefabs/Panels/"+selectobj.name + ".prefab";

            bool createPrefabSucceed = false;  
            PrefabUtility.SaveAsPrefabAsset(selectobj,prefabPath,out createPrefabSucceed);

            if(createPrefabSucceed) 
                Debug.Log("创建panel预制体成功!"+prefabPath);

            Debug.Log("创建脚本 " + scriptPath + ".cs 成功!");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();                
        }
        else if(selectobj.name.StartsWith("Mask_")){
            string maskScriptStr = MaskClassStr;

            string scriptPath = Application.dataPath + "/TResources/Scripts/UIScripts/Mask/" + selectobj.name + ".cs";

            string enumNameScriptPath = Application.dataPath + "/TResources/Scripts/UIScripts/ZZUICore/ZZUIName.cs";

            string 组件赋值 = "";

            string 成员变量定义 = "";

            string 点击事件装载 = "";

            for (int i = 0; i < childrens.Length; i++)
            {
                string cName = prefixReturnFullName(childrens[i].name.Split('_')[0]);
                if(cName!="")
                {
                    组件赋值 += "\t\t"+ childrens[i].name 
                    + " = skinTr.SearchGet<"+ cName 
                    +">(\""+childrens[i].name+"\");" + "\n";
                    成员变量定义 += cName + " " + childrens[i].name + ";";
                    if(cName == "UIEventListener"){
                        点击事件装载 += "\t\tcase \"" + childrens[i].name + "\":\n\t\t\t\t\n\t\t\tbreak;\n\t";
                    }
                }
            }

            maskScriptStr = maskScriptStr.Replace("#组件赋值#",组件赋值);
            maskScriptStr = maskScriptStr.Replace("#成员变量定义#",成员变量定义);
            maskScriptStr = maskScriptStr.Replace("#点击事件装载#",点击事件装载);
            maskScriptStr = maskScriptStr.Replace("#类名#",selectobj.name);
            
            FileStream scriptFile = new FileStream(scriptPath, FileMode.CreateNew);
            StreamWriter fileWrite = new StreamWriter(scriptFile, System.Text.Encoding.UTF8);

            //读取并写入Enum
            string enumScriptStr = File.ReadAllText(enumNameScriptPath,System.Text.Encoding.UTF8);
            enumScriptStr = enumScriptStr.Replace("MLastFind",selectobj.name+",\nMLastFind");
            
            FileStream enumNameScriptFile = new FileStream(enumNameScriptPath,FileMode.Create);
            StreamWriter enumScriptWrite = new StreamWriter(enumNameScriptFile, System.Text.Encoding.UTF8);


            fileWrite.Write(maskScriptStr);
            enumScriptWrite.Write(enumScriptStr);


            fileWrite.Flush();
            enumScriptWrite.Flush();
            fileWrite.Close();
            scriptFile.Close();
            enumScriptWrite.Close();
            enumNameScriptFile.Close();

            //创建Prefab
            string prefabPath = "Assets/TResources/Prefabs/UIMainPrefabs/Masks/"+selectobj.name + ".prefab";

            bool createPrefabSucceed = false;  
            PrefabUtility.SaveAsPrefabAsset(selectobj,prefabPath,out createPrefabSucceed);

            if(createPrefabSucceed) 
                Debug.Log("创建Mask预制体成功!"+prefabPath);

            Debug.Log("创建脚本 " + scriptPath + ".cs 成功!");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();    
        }
        else if(selectobj.name.StartsWith("Tip_")){
            //创建tip enum
            string enumNameScriptPath = Application.dataPath + "/TResources/Scripts/UIScripts/ZZUICore/ZZUIName.cs";

            string enumScriptStr = File.ReadAllText(enumNameScriptPath,System.Text.Encoding.UTF8);
            enumScriptStr = enumScriptStr.Replace("TLastFind",selectobj.name+",\nTLastFind");

            FileStream enumNameScriptFile = new FileStream(enumNameScriptPath,FileMode.Create);
            StreamWriter enumScriptWrite = new StreamWriter(enumNameScriptFile, System.Text.Encoding.UTF8);

            enumScriptWrite.Write(enumScriptStr);

            enumScriptWrite.Flush();
            enumScriptWrite.Close();
            enumNameScriptFile.Close();

            //创建Prefab
            string prefabPath = "Assets/TResources/Prefabs/UIMainPrefabs/Tips/"+selectobj.name + ".prefab";

            bool createPrefabSucceed = false;  
            PrefabUtility.SaveAsPrefabAsset(selectobj,prefabPath,out createPrefabSucceed);

            if(createPrefabSucceed) 
                Debug.Log("创建Mask预制体成功!"+prefabPath);
        }
        else
            Debug.Log("请选择Panel_ | Scene_ | Mask_ | Tip_ 做前缀来创建UI套装");
    }       
}
