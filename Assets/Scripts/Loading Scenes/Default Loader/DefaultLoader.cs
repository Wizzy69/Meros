using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefaultLoader : MonoBehaviour
{
    public Slider progressBar;

    public int LoadSceneIndex;

    private void Start() {
        StartCoroutine(loadSceneAsync());
    }

    private IEnumerator loadSceneAsync() {
        SystemVariables.playerData = SaveDataScript.LoadGame();
        if (SystemVariables.playerData == null)
        {
            SystemVariables.playerData = new PlayerData
            {
                HP = 100,
                X = -19f,
                Y = -27f,
                Z = 0f,
                isBoss = false,
                damagePotions = 2,
                healingPotions = 5,
                speedPotions = 1,
                money = 1000,
                achievements = new List<string>(),
                oneTimeDialogues = new Dictionary<Dialogue, bool>(),
                chest1Opened = false,
                chest2Opened = false,
                chest3Opened = false,
                boss01_killed = false,
                firstStory = false,
                ZoneName = "MapZone1",
                chestHiddenOpened = false
            };


            SaveDataScript.SaveGame();
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(LoadSceneIndex);
        while (asyncOperation.isDone == false)
        {
            progressBar.value = asyncOperation.progress * Time.fixedDeltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
