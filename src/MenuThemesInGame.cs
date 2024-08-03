using System.Collections;
using System.Reflection;
using Modding;
using UnityEngine;

namespace MenuThemesInGame;

public class MenuThemesInGame : Mod
{
    internal static MenuThemesInGame Instance;

    public override string GetVersion()
    {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }

    public override void Initialize()
    {
        Log("Initializing");
        Instance = this;

        GameManager.instance.StartCoroutine(ColorCorrection());

        Log("Initialized");
    }

    private IEnumerator ColorCorrection()
    {
        yield return new WaitWhile(() => !Object.FindObjectOfType<MenuStyles>());
        var ms = Object.FindObjectOfType<MenuStyles>();
        var cc = ms.styles[ms.CurrentStyle].cameraColorCorrection;
        var gc = GameCameras.instance;
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