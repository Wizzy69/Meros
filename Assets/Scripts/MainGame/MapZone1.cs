using UnityEngine;

public class MapZone1 : MonoBehaviour
{
	public GameObject TeleportPortal_BossAlive;

	public GameObject TeleportPortal_BossKilled;

	private void Start()
	{
		if (SystemVariables.playerData.boss01_killed)
		{
			TeleportPortal_BossAlive.SetActive(false);
			TeleportPortal_BossKilled.SetActive(true);
		}
		else
		{
			TeleportPortal_BossAlive.SetActive(true);
			TeleportPortal_BossKilled.SetActive(false);
		}
	}
}