﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleControl : MonoBehaviour
{
    [SerializeField,Tooltip("遷移先のシーン")]
    private SceneControl.SceneName scene;
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private RawImage rawImage;
    [SerializeField]
    private List<TitleSubCamera> subCameras;
    private RenderTexture[] renderTextures;
    private bool cameraStartFlag = false;
    private int cameraNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        startButton.enabled = true;

        // ステージモデル用のカメラRenderTextureの作成
        renderTextures = new RenderTexture[subCameras.Count];
        for(int i = 0; i < renderTextures.Length; i++)
        {
            renderTextures[i] = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
            subCameras[i].CameraRender = renderTextures[i];
        }

        cameraStartFlag = true;
    }

    private void Update()
    {
        if (cameraStartFlag)
        {
            // カメラを回転させる
            cameraStartFlag = false;
            rawImage.texture = renderTextures[cameraNumber];
            subCameras[cameraNumber].CameraRotateStop = false;
        }

        if (subCameras[cameraNumber].CameraRotateStop)
        {
            // カメラが一回転したら次のカメラを回転させる
            cameraStartFlag = true;
            cameraNumber++;
            if(cameraNumber >= subCameras.Count)
            {
                cameraNumber = 0;
            }
        }
    }

    /// <summary>
    /// スタートボタンで呼び出される処理
    /// </summary>
    public void ButtonAction()
    {
        startButton.enabled = false;
        AudioManager.Instance.PlaySE(AudioManager.SeName.button);
        // シーン遷移
        SceneControl.Instance.LoadScene(scene, true);
    }
}
