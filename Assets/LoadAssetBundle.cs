using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class LoadAssetBundle : MonoBehaviour {

    public Button buttonLoadAssetBundle;
    public InputField inputFieldAssetBundle;
    public Dropdown dropdownScenes;
    public SceneLoaderButton loader;

    public static AssetBundle ab;

    List<string> scene_list = new List<string>();

    public LoadAssetBundle() {
       
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad (this.gameObject);
        buttonLoadAssetBundle.onClick.AddListener (()=>{
            ProcLoadAssetBundle();
        });

        dropdownScenes.onValueChanged.AddListener ((int n)=>{
            loader.scene_name = scene_list[n];
        });

	}

    void ProcLoadAssetBundle() {
        string file_path = inputFieldAssetBundle.text;
        if (!File.Exists (file_path)) {
            Logger.Log ("文件不存在");
            return;
        }

        if (ab != null) {
            ab.Unload (true);
        } 

        ab = AssetBundle.LoadFromFile (file_path);
        if (ab == null) {
            Logger.Log ("加载文件失败");
            return;
        }

        string[] scene_paths = ab.GetAllScenePaths ();
        if (scene_paths.Length == 0) {
            Logger.Log ("AssetBundle没有找到场景信息");
            return;
        }
        dropdownScenes.ClearOptions ();
        scene_list = new List<string>();

        foreach (string s in scene_paths) {
            string tmp = s;
            if (s.Contains ("/")) {
                tmp = s.Substring (s.LastIndexOf('/') + 1);
                tmp = tmp.Replace (".unity", "");
            }
            scene_list.Add(tmp);
        }
        if (scene_list.Count > 0) {
            loader.scene_name = scene_list [0];
        }

        dropdownScenes.AddOptions (scene_list);
        Logger.Log ("从AssetBundle加载场景成功");
    }

    void OnApplicationQuit() {
        Logger.Log ("OnApplicationQuit Unload AssetBundle");
        if (ab != null) {
            ab.Unload (true);
        }
        DestroyImmediate (this.gameObject);
    }
}
