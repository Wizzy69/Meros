using UnityEngine;

public class Eye : Monster
{
	#region Class Properties

	[Header("Eye")] [Space(10f)]

	#endregion

	public bool LookToTarget;

	public override void TakeDamage(float Damage)
	{
		base.TakeDamage(Damage);
	}

	public void FixedUpdate()
	{
		DefaultMovementToPlayer(LookToTarget);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.name == "Player")
		{
			collision.GetComponent<PlayerMovement>().TakeDamage(attackDamage);

			Vector2 newPos = NewPosition(attackRange);
			transform.position = newPos;
		}
	}
}