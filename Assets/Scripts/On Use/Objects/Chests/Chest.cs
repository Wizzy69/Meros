using UnityEngine;

public class Chest : MonoBehaviour
{
	private BoxCollider2D boxCollider;
	public  float         maxMoney;

	public  float minMoney;
	private float money;
	private bool  opened;

	private void Start()
	{
		money       = Random.Range(minMoney, maxMoney);
		boxCollider = GetComponent<BoxCollider2D>();
	}


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (opened) return;
		if (collision.collider.name == "Player")
		{
			SystemVariables.playerData.money += money;
			opened                           =  false;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		boxCollider  = GetComponent<BoxCollider2D>();
		Gizmos.DrawWireCube(transform.position, new Vector3(boxCollider.size.x, boxCollider.size.y, 0f));
	}
}