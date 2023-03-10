using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BM
{
    /// <summary>
    /// 用于配置单个Bundle包的构建信息
    /// </summary>
    public class AssetsLoadSetting : AssetsSetting
    {
        [Header("AssetBundle的后缀")]
        [Tooltip("AssetBundle资源的的后缀名(如'bundle')")] public string BundleVariant;
        
        [Header("是否启用Hash名")]
        [Tooltip("是否使用Hash名替换Bundle名称")] public bool NameByHash;

        [Header("构建选项")]
        public BuildAssetBundleOptions BuildAssetBundleOptions = BuildAssetBundleOptions.UncompressedAssetBundle;
        
        [Header("是否加密资源")]
        [Tooltip("加密启用后会多一步异或操作")] public bool EncryptAssets;
        
        [Header("加密密钥")]
        [Tooltip("进行异或操作的密钥")] public string SecretKey;
        
        [Header("资源路径")]
        [Tooltip("需要打包的资源所在的路径(不需要包含依赖, 只包括需要主动加载的资源)")]
        public List<string> AssetPath = new List<string>();
        
        [Header("一组资源路径")]
        [Tooltip("资源颗粒控制")]
        public List<string> AssetGroupPaths = new List<string>();
        
        [Header("场景资源")]
        [Tooltip("需要通过Bundle加载的场景的路径")]
        public List<string> ScenePath = new List<string>();

        [Header("黑名单文件")]
        [Header("该列表内的同名文件不会被打入AB")]
        public List<string> BlacklistFile = new List<string>()
        {
            ".DS_Store",
            "LightingData.asset"
        };

        [Header("黑名单后缀")] [Header("该列表内的同后缀文件不会被打入AB")]
        public List<string> BlacklistExtension = new List<string>()
        {
            ".dll",
            ".cs",
            ".meta",
            ".js",
            ".boo"
        };
    }
}


