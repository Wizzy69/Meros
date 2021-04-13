using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoPanelScripts : MonoBehaviour
{
	public Dropdown graphics;
	public Dropdown resolution;
	public Dropdown screenSize;

	private void Start()
	{
		UpdateResolutionsArray();
		UpdateQualityArray();
		UpdateScreenSizeArray();
	}

	private void UpdateResolutionsArray()
	{
		resolution.ClearOptions();
		var resolutions = new List<string>();

		foreach (Resolution resolution in Screen.resolutions)
			resolutions.Add(resolution.width + " x " + resolution.height + " ( " + resolution.refreshRate + " Hz)");

		resolution.AddOptions(resolutions);

		resolution.value = SystemVariables.videoSettings.resolutionIndex;
		//get the current index of the current resolution
		//resolutions.FindIndex(s => s == (Screen.currentResolution.width + " x " + Screen.currentResolution.height + " ( " + Screen.currentResolution.refreshRate + " Hz)"));
		resolution.RefreshShownValue();
	}

	private void UpdateQualityArray()
	{
		graphics.ClearOptions();
		graphics.AddOptions(new List<string>{"Very Low", "Low", "Medium", "High", "Very High", "Ultra"});

		graphics.value = SystemVariables.videoSettings.qualityIndex;
		graphics.RefreshShownValue();
	}

	private void UpdateScreenSizeArray()
	{
		screenSize.ClearOptions();
		screenSize.AddOptions(new List<string>{"Borderless", "Full Screen", "Maximized", "Windowed"});

		screenSize.value = SystemVariables.videoSettings.screenSizeIndex;
		screenSize.RefreshShownValue();
	}

	public void SaveSettings()
	{
		int resolution = this.resolution.value;
		int graphics   = this.graphics.value;
		int screenSize = this.screenSize.value;

		if (resolution == 0 && graphics == 0 && screenSize == 0)
			return;

		var mode = (FullScreenMode) screenSize;

		QualitySettings.SetQualityLevel(graphics);

		Screen.SetResolution(Screen.resolutions[resolution].width, Screen.resolutions[resolution].height, mode);

		SystemVariables.videoSettings.qualityIndex    = graphics;
		SystemVariables.videoSettings.resolutionIndex = resolution;
		SystemVariables.videoSettings.screenSizeIndex = screenSize;
		SaveDataScript.SaveVideoSettings();
	}
}
