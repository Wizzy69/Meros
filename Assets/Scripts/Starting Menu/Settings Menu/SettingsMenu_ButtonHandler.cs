using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu_ButtonHandler : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Camera mainCamera;

    public GameObject videoPanel;
    public GameObject audioPanel;

    public AudioPanelScripts audioPanelScripts;
    public VideoPanelScripts videopanelScripts;

    public void Back()
    {
        audioPanelScripts.SaveAudioSettings();
        videopanelScripts.SaveSettings();

        mainMenuCanvas.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void BackGame(Camera current)
    {
        mainCamera.gameObject.SetActive(true);
        current.gameObject.SetActive(false);
        audioPanelScripts.SaveAudioSettings();
        videopanelScripts.SaveSettings();
        mainMenuCanvas.gameObject.SetActive(true);
    }

    public void VideoButtonClick()
    {
        videoPanel.SetActive(true);
        audioPanel.SetActive(false);
    }

    public void AudioButtonClick()
    {
        videoPanel.SetActive(false);
        audioPanel.SetActive(true);
    }
}
