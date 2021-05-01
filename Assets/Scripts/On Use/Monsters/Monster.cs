using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Monster : MonoBehaviour
{

    public float attackDamage;
    public float attackRange;
    public float currentHP;
    public float knockbackCoefficient;
    public float monsterHP;
    public float movementSpeedDefault;
    public float movementSpeedToPlayer;


    private Vector2 newDestination;


    [HideInInspector] public Transform player;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer renderer;
    [HideInInspector] public Spawner spawner;


    private void Start() {
        currentHP = monsterHP;

        renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>().transform;
        spawner = GetComponentInParent<Spawner>();

        getRandomPositionNearby();


    }

    public virtual void TakeDamage(float Damage) {
        currentHP -= Damage;

        if (currentHP <= 0f)
            Die();
    }

    private void Die() {
        Destroy(gameObject);

        spawner.monsterCounter -= 1f;
    }

    public Vector2 NewPosition(float r) {
        float X = Random.Range(transform.position.x - r, transform.position.x + r);
        float Y = Random.Range(transform.position.y - r, transform.position.y + r);
        return new Vector2(X, Y);
    }

    private void getRandomPositionNearby() {
        Vector2 newPos = NewPosition(attackRange);
        if (newPos.x < spawner.mapRight) newPos.x = spawner.mapRight;
        if (newPos.x > spawner.mapLeft) newPos.x = spawner.mapLeft;
        if (newPos.y > spawner.mapTop) newPos.y = spawner.mapTop;
        if (newPos.y < spawner.mapBottom) newPos.y = spawner.mapBottom;

        newDestination = newPos;
    }

    /// <summary>
    ///     Look at object (Z Rotation)
    /// </summary>
    /// <param name="t">The object to look at</param>
    public void LookToTransform(Transform t) {
        Vector3 v = t.position - transform.position;
        float a = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(a, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * 100f);
    }

    /// <summary>
    ///     Look to object (Z Rotation)
    /// </summary>
    /// <param name="t">The position of the object to look at</param>
    public void LookToTransform(Vector3 t) {
        Vector3 v = t - transform.position;
        float a = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(a, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * 100f);
    }


    /// <summary>
    ///     Fixed Update Movement
    /// </summary>
    public void DefaultMovementToPlayer(bool lookToTarget) {
        float d = Vector2.Distance(player.position, transform.position);

        if (d <= attackRange)
        {
            if (lookToTarget)
            {
                LookToTransform(player);
            }
            else
            {
                if (player.position.x - transform.position.x < 0)
                    renderer.flipX = false;
                if (player.position.x - transform.position.x > 0)
                    renderer.flipX = true;
            }

            Vector2 newPosition = Vector2.MoveTowards(transform.position, player.position,
                movementSpeedToPlayer * Time.fixedDeltaTime);

            rb.MovePosition(newPosition);
            newDestination = player.position;
        }
        else
        {
            float distance = Vector2.Distance(transform.position, newDestination);
            if (distance <= 0.2f) getRandomPositionNearby();

            Vector2 newPosition = Vector2.MoveTowards(transform.position, newDestination,
                movementSpeedDefault * Time.fixedDeltaTime);
            if (lookToTarget) LookToTransform(newDestination);

            rb.MovePosition(newPosition);

            //Debug.Log(gameObject.name + " " + newPosition.x + " " + newPosition.y);

        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
