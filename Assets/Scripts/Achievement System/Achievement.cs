using UnityEngine;

public class Achievement : AchievementSystem
{
	public string AchievementName;

	public Sprite AchievementImageSprite;

	public AchievementType achievementType;

	public void TriggerAchievement()
	{
		Trigger(AchievementName, AchievementImageSprite);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.name == "Player" && achievementType == AchievementType.CollideWithObject)
			TriggerAchievement();
	}
}
