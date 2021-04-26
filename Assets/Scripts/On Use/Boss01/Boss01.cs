using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss01 : MonoBehaviour
{
    #region statics

    public static float bossMaxHP;

    #endregion

    public float attackRate;
    private Rigidbody2D boss;

    public float bossHP;
    public Slider bossHP_Slider;
    private bool canMove;
    public float collisionDamage;
    public bool enraged;

    public GameObject EnterTeleport;
    public GameObject EnterTeleport_Killed;
    public GameObject flameBall;
    public float movementSpeed;
    public ParticleSystem particleSystem;
    private bool phaseThree;
    public Transform player;
    public float projectileRange;
    private SpriteRenderer renderer;
    public float time_damageOverTime;

    private DialogueTrigger trigger;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "Player" && canMove) player.GetComponent<PlayerMovement>().hpBar.value -= collisionDamage;
    }

    private void Start() {
        canMove = true;
        SpawnFireBall();
        renderer = transform.GetComponent<SpriteRenderer>();
        boss = GetComponent<Rigidbody2D>();
        bossHP_Slider.maxValue = bossHP;
        bossHP_Slider.value = bossHP;

        bossMaxHP = bossHP;

        particleSystem.gameObject.SetActive(false);
        trigger = GetComponent<DialogueTrigger>();
    }

    public void TakeDamage(float damage) {
        if (bossHP - damage <= 0f)
        {
            bossHP = 0f;
            bossHP_Slider.value = 0f;
            canMove = false;

            EnterTeleport.SetActive(false);
            SystemVariables.playerData.boss01_killed = true;

            GetComponent<Achievement>().TriggerAchievement();

            StartCoroutine(startParticles());
        }
        else
        {
            bossHP_Slider.value -= damage;
            bossHP -= damage;
            if (!enraged && bossHP < bossHP_Slider.maxValue / 2f)
            {
                GetComponent<Animator>().SetBool("Enraged", true);
                attackRate -= 1f;
                enraged = true;

                StartCoroutine(phaseTwo());
            }

            if (bossHP < bossHP_Slider.maxValue / 20f && !phaseThree)
            {
                phaseThree = true;
                projectileRange += .25f;
                attackRate -= .25f;
            }
        }
    }

    private IEnumerator phaseTwo() {
        while (true)
        {
            SpawnFB();
            yield return new WaitForSeconds(attackRate);
        }
    }

    private IEnumerator startParticles() {
        trigger.TriggerDialogue();
        particleSystem.gameObject.SetActive(true);
        yield return new WaitForSeconds(GetComponent<Achievement>().showTime * 2);
        particleSystem.gameObject.SetActive(false);

        GetComponent<Teleport>().TeleportPlayer(player);

        SystemVariables.playerData.isBoss = false;
        EnterTeleport_Killed.SetActive(true);

        Destroy(gameObject);
    }

    private void SpawnFireBall() {
        InvokeRepeating("SpawnFB", 0f, attackRate);
    }

    private void SpawnFB() {
        if (!canMove) return;
        GameObject fb = Instantiate(flameBall, new Vector2(transform.position.x, transform.position.y),
            Quaternion.Euler(0f, 0f, -90f));
        Destroy(fb, projectileRange);
    }

    private void Update() {
        if (!canMove) return;
        if (player.position.x - transform.position.x < 0)
            renderer.flipX = true;
        if (player.position.x - transform.position.x > 0)
            renderer.flipX = false;
    }

    private void FixedUpdate() {
        if (!canMove) return;
        Vector2 newPosition =
            Vector2.MoveTowards(transform.position, player.position, movementSpeed * Time.fixedDeltaTime);
        boss.MovePosition(newPosition);
    }
}
