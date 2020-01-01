using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using GlobalEnums;
using Modding;
using ModCommon;
using ModCommon.Util;
using UnityEngine;
using UnityEngine.UI;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

namespace MenuThemesInGame
{
    public class MenuThemesInGame : Mod
    {
        internal static MenuThemesInGame Instance;

        public override string GetVersion() => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public override void Initialize()
        {
            Log("Initializing");
            Instance = this;

            GameManager.instance.StartCoroutine(ColorCorrection());

            Log("Initialized");
        }

        private IEnumerator ColorCorrection()
        {
            yield return new WaitWhile(() => !GameObject.FindObjectOfType<MenuStyles>());
            MenuStyles ms = GameObject.FindObjectOfType<MenuStyles>();
            MenuStyles.MenuStyle.CameraCurves cc = ms.styles[ms.CurrentStyle].cameraColorCorrection;
            GameCameras gc = GameCameras.instance;
            while (true)
            {
                cc = ms.styles[ms.CurrentStyle].cameraColorCorrection;
                gc.colorCorrectionCurves.saturation = cc.saturation; // 0.0f - 5.0f
                gc.colorCorrectionCurves.redChannel = cc.redChannel; // Curve (0.0f, 0.0f) - (1.0f, 1.0f)
                gc.colorCorrectionCurves.greenChannel = cc.greenChannel; // Curve (0.0f, 0.0f) - (1.0f, 1.0f)
                gc.colorCorrectionCurves.blueChannel = cc.blueChannel; // Curve (0.0f, 0.0f) - (1.0f, 1.0f)
                yield return null;
            }
        }
    }
}
