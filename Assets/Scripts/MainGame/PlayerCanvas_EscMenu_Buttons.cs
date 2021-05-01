using UnityEngine;

public class PlayerCanvas_EscMenu_Buttons : MonoBehaviour
{
	public GameObject ESCMenu;

	private void Start()
	{
		SystemVariables.videoSettings = SaveDataScript.LoadVideoSettings();
		SystemVariables.audioSettings = SaveDataScript.LoadAudioSettings();
	}


	public void HpBarValueChanged(float newValue)
	{
		SystemVariables.playerData.HP = newValue;
	}

	public void AchievementButton(Camera AchievementsCamera)
	{
		AchievementsCamera.gameObject.SetActive(true);
		Camera.main.gameObject.SetActive(false);
		gameObject.SetActive(false);
	}

	public void ResumeClick()
	{
		ESCMenu.SetActive(false);
	}

	public void SettingsClick(Camera Start)
	{
		Start.gameObject.SetActive(true);
		gameObject.SetActive(false);
		//Camera.main.gameObject.SetActive(false);
		
	}

	public void SaveExitClick(Transform playerPosition)
	{
		SaveDataScript.SaveAudioSettings();
		SaveDataScript.SaveVideoSettings();

		SystemVariables.playerData.X = playerPosition.position.x;
		SystemVariables.playerData.Y = playerPosition.position.y;
		SystemVariables.playerData.Z = playerPosition.position.z;


		SaveDataScript.SaveGame();

		Application.Quit();
	}
}