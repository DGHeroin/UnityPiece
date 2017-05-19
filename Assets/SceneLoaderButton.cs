using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneLoaderButton : MonoBehaviour {
    public Button buttonLoadScene;
    public string scene_name;

	// Use this for initialization
	void Start () {
        if (buttonLoadScene == null) {
            buttonLoadScene = gameObject.GetComponent<Button> ();
        }

        if (buttonLoadScene == null) {
            Logger.Log ("加载场景按键没有绑定");
            return;
        }

        buttonLoadScene.onClick.AddListener (()=>{
            ProcLoadScene();
        });
	}
	
    void ProcLoadScene() {
        string s = scene_name;
        if (String.IsNullOrEmpty(s)) {
            Logger.Log ("场景名称未指定");
            return;
        }

        Logger.Log ("加载场景: " + s);

        SceneManager.LoadScene (s);
    }
}
