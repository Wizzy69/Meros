using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
	#region Achievements

	public List<string> achievements;

	#endregion

	#region Bosses

	public bool boss01_killed;

	#endregion


	#region Functions

	public bool HasErrorOnLoad()
	{
		if (achievements == null || oneTimeDialogues == null)
			return true;
		return false;
	}

	#endregion

	#region Player Data Variables

	public bool chest1Opened;
	public bool chest2Opened;
	public bool chest3Opened;
	public int  damagePotions;


	public bool firstStory;
	public int  healingPotions;

	public float HP;

	public bool isBoss;

	public float money;

	public Dictionary<Dialogue, bool> oneTimeDialogues;
	public int                        speedPotions;
	public float                      X;
	public float                      Y;
	public float                      Z;

	#endregion
}