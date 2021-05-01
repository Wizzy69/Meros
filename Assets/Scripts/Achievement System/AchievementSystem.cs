using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class AchievementSystem : MonoBehaviour
{
    /// <summary>
    ///     Time (s) to display the achievement banner
    /// </summary>
    public float showTime;

    public Image AchievementImage;
    //public Text achievemnetNameTextBox;

    private Animator a;

    private void Start() {
        a = AchievementImage.GetComponent<Animator>();
    }

    public virtual void Trigger(string achievementName, Sprite achievementSprite) {
        if (!SystemVariables.playerData.achievements.Contains(achievementName))
        {
            SystemVariables.playerData.achievements.Add(achievementName);
            //achievemnetNameTextBox.text = achievementName;
            StartCoroutine(ShowAchievement(achievementSprite));
        }
    }

    private IEnumerator ShowAchievement(Sprite s) {
        //AchievementImage.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();

        if (s != null)
            AchievementImage.sprite = s;

        a.SetTrigger("Show");
        yield return new WaitForSeconds(showTime);

        a.SetTrigger("Hide");
        // yield return achievemnetNameTextBox.text = null;
    }
}
