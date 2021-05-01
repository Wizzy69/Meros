using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    public Animator Animator;

    public Vector3 attackOffset;
    public float attackRange;

    public GameObject boss01;
    public Camera BossCamera;
    private bool bossContact;
    private bool canMove = true;


    private DamagePotion damagePotion;

    public DialogueManager DialogueManager;
    public LayerMask enemyLayerMask;
    private bool EnterBoss;

    private bool enterShop;
    private bool ExitShop;
    private HealPotion healPotion;
    public Slider hpBar;

    private bool inBoss01;

    private GameObject interraction;
    public Camera mainCamera;
    private Vector2 movement;
    public float movementSpeed;

    public Rigidbody2D playerBody;
    public float playerDamage;
    public Camera ShopCamera;

    public GameObject ShopMenu;

    public GameObject SmallMenu;
    private SpeedPotion speedPotion;
    private bool talkToShop;
    private bool useDamage;

    private bool useHeal;
    private bool useSpeed;

    public GameObject MapZone2;
    public GameObject MapZone1;

    private void Start() {
        damagePotion = GetComponent<DamagePotion>();
        healPotion = GetComponent<HealPotion>();
        speedPotion = GetComponent<SpeedPotion>();

        healPotion.healLabel.text = SystemVariables.playerData.healingPotions.ToString();
        damagePotion.damageLabel.text = SystemVariables.playerData.damagePotions.ToString();
        speedPotion.speedLabel.text = SystemVariables.playerData.speedPotions.ToString();

        speedPotion.speedSlider.gameObject.SetActive(false);
        damagePotion.damageSlider.gameObject.SetActive(false);
        healPotion.healSlider.gameObject.SetActive(false);

        //hpBar.value = SystemVariables.playerData.HP;
        hpBar.value = hpBar.maxValue;
        transform.position = new Vector3(SystemVariables.playerData.X, SystemVariables.playerData.Y,
            SystemVariables.playerData.Z);

        if (SystemVariables.playerData.isBoss)
        {
            mainCamera.gameObject.SetActive(false);
            BossCamera.gameObject.SetActive(true);
        }

        if (!SystemVariables.playerData.firstStory)
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();

            SystemVariables.playerData.firstStory = true;
            SaveDataScript.SaveGame();

            SystemVariables.playerData.ZoneName = "MapZone1";
        }

        if (SystemVariables.playerData.boss01_killed)
        {
            boss01.GetComponent<Boss01>().EnterTeleport_Killed.SetActive(true);
            boss01.GetComponent<Boss01>().EnterTeleport.SetActive(false);
        }

        if (SystemVariables.playerData.ZoneName == "MapZone1")
        {
            mainCamera.gameObject.SetActive(true);
            MapZone2.gameObject.SetActive(false);
            MapZone1.gameObject.SetActive(true);
        }

        if (SystemVariables.playerData.ZoneName == "MapZone2")
        {

            MapZone2.gameObject.SetActive(true);
            MapZone1.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(false);
        }

        Debug.LogError(SystemVariables.playerData.ZoneName);
    }

    private void UsePotion(PotionType potion) {
        switch (potion)
        {
            case PotionType.HEAL:
                if (useHeal || SystemVariables.playerData.healingPotions <= 0) return;
                useHeal = true;
                healPotion.healSlider.gameObject.SetActive(true);
                SystemVariables.playerData.healingPotions--;
                healPotion.healLabel.text = SystemVariables.playerData.healingPotions.ToString();
                StartCoroutine(HealPlayer());

                break;
            case PotionType.DAMAGE:
                if (useDamage || SystemVariables.playerData.damagePotions <= 0) return;
                useDamage = true;
                damagePotion.damageSlider.gameObject.SetActive(true);
                SystemVariables.playerData.damagePotions--;
                damagePotion.damageLabel.text = SystemVariables.playerData.damagePotions.ToString();
                StartCoroutine(IncreaseDamage());


                break;
            case PotionType.SPEED:
                if (useSpeed || SystemVariables.playerData.speedPotions <= 0) return;
                useSpeed = true;
                speedPotion.speedSlider.gameObject.SetActive(true);
                SystemVariables.playerData.speedPotions--;
                speedPotion.speedLabel.text = SystemVariables.playerData.speedPotions.ToString();
                StartCoroutine(IncreaseSpeed());


                break;
        }
    }

    private IEnumerator IncreaseSpeed() {
        speedPotion.speedSlider.value = 1f;
        movementSpeed *= speedPotion.speedFactor;
        for (float i = 1; i <= speedPotion.useTime; i += 1)
        {
            speedPotion.speedSlider.value -= 1f / speedPotion.useTime;
            yield return new WaitForSeconds(1);
        }

        useSpeed = false;
        movementSpeed /= speedPotion.speedFactor;
        speedPotion.speedSlider.gameObject.SetActive(false);
    }

    private IEnumerator IncreaseDamage() {
        damagePotion.damageSlider.value = 1f;
        playerDamage *= damagePotion.damageFactor;
        for (float i = 1; i <= damagePotion.useTime; i += 1)
        {
            damagePotion.damageSlider.value -= 1f / damagePotion.useTime;
            yield return new WaitForSeconds(1);
        }

        playerDamage /= damagePotion.damageFactor;
        damagePotion.damageSlider.gameObject.SetActive(false);
        useDamage = false;
    }

    private IEnumerator HealPlayer() {
        float i;
        float lim = healPotion.useTime;
        healPotion.healSlider.value = 1f;
        for (i = 1; i <= lim; i += 1)
        {
            if (hpBar.value + healPotion.healFactor > hpBar.maxValue)
                break;
            hpBar.value += healPotion.healFactor * 25 * Time.fixedDeltaTime;
            healPotion.healSlider.value -= 1f / healPotion.useTime;
            yield return new WaitForSeconds(1);
        }

        healPotion.healSlider.gameObject.SetActive(false);
        useHeal = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ShopMenu.activeSelf)
            {
                ShopMenu.SetActive(false);
                canMove = true;
            }
            else
            {
                SmallMenu.SetActive(!SmallMenu.activeSelf);
            }
        }

        if (!canMove)
        {
            Animator.ResetTrigger("HitTaken");
            Animator.ResetTrigger("Attack");
            Animator.SetBool("isMoving", false);
            return;
        }


        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        Animator.SetFloat("Horizontal", xMove);
        Animator.SetFloat("Vertical", yMove);
        if (xMove == 0f && yMove == 0f)
            Animator.SetBool("isMoving", false);
        else Animator.SetBool("isMoving", true);

        if (xMove == -1)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        if (xMove == 1)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        movement = new Vector2(xMove, yMove);

        if (Input.GetMouseButtonDown(0) && canMove && Animator.GetBool("isMoving") == false)
            Animator.SetTrigger("Attack");
        else
            Animator.ResetTrigger("Attack");

        if (Input.GetKeyDown(KeyCode.Space))
            DialogueManager.DisplayNextLine();


        if (Input.GetKeyDown(KeyCode.Alpha1))
            UsePotion(PotionType.DAMAGE);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            UsePotion(PotionType.SPEED);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            UsePotion(PotionType.HEAL);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (talkToShop)
            {
                ShopMenu.gameObject.SetActive(true);
                canMove = false;
                talkToShop = true;
                DialogueManager.EndDialogue();
                ShopMenu.GetComponent<ShopMenuScripts>().currency.GetComponent<Text>().text =
                    SystemVariables.playerData.money + " Jz";
            }
        }
    }

    public void TakeDamage(float damage) {
        if (hpBar.value - damage <= 0)
        {
            hpBar.value = 0;
            if (inBoss01) KillPlayerAtBoss01();
            else KillPlayerMob();
        }
        else
        {
            hpBar.value -= damage;
            Animator.SetTrigger("HitTaken");
        }
    }

    //used by animator
    public void DealDamage() {
        Vector3 pos = transform.position;

        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D c = Physics2D.OverlapCircle(pos, attackRange, enemyLayerMask);
        if (c != null)
        {
            var boss = c.GetComponent<Boss01>();
            var monster = c.GetComponent<Monster>();
            if (boss)
                boss.TakeDamage(playerDamage);
            if (monster)
                monster.TakeDamage(playerDamage);
        }
    }


    public void KillPlayerMob() {
        hpBar.value = 100;
        SystemVariables.playerData.money -= 5;

        //Debug.LogError(SystemVariables.playerData.ZoneName);
        if (SystemVariables.playerData.ZoneName == "MapZone1")
            transform.position = new Vector3(-19.45f, -26.58f, 0f);
        if (SystemVariables.playerData.ZoneName == "MapZone2")
            transform.position = new Vector3(38.9f, -23.5f, 0f);
    }

    public void KillPlayerAtBoss01() {
        GetComponent<Teleport>().TeleportPlayer(transform);


        hpBar.value = 100f;
        boss01.GetComponent<Boss01>().bossHP = Boss01.bossMaxHP;
        boss01.GetComponent<Boss01>().bossHP_Slider.value = Boss01.bossMaxHP;
        boss01.GetComponent<Boss01>().attackRate += 0.75f;
        boss01.GetComponent<Boss01>().enraged = false;
        boss01.GetComponent<Boss01>().gameObject.GetComponent<Animator>().SetBool("Enraged", false);
        boss01.GetComponent<Boss01>().StopAllCoroutines();

        inBoss01 = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var dialogue = collision.gameObject.GetComponent<DialogueTrigger>();
        if (dialogue != null && collision.gameObject.name != "Boss01")
            dialogue.TriggerDialogue();

        if (collision.name == "Default_Friction_Element" || collision.name == "PolyCollider")
            movementSpeed -= 1.5f;

        if (collision.name == "Boss01")
        {
            bossContact = true;
            StartCoroutine(damageOverTime(collision.gameObject.GetComponent<Boss01>().collisionDamage,
                collision.gameObject.GetComponent<Boss01>().time_damageOverTime));
        }

        //Chest Handler
        switch (collision.name)
        {
            case "Chest 1":
                SystemVariables.playerData.chest1Opened = true;
                break;
            case "Chest 2":
                SystemVariables.playerData.chest2Opened = true;
                break;
            case "Chest 3":
                SystemVariables.playerData.chest3Opened = true;
                break;
            case "Hidden Chest":
                SystemVariables.playerData.chestHiddenOpened = true;
                break;
            case "ShopEntrance":
                interraction = collision.gameObject;
                enterShop = true;
                break;
            case "ShopExit":
                ExitShop = true;
                interraction = collision.gameObject;
                break;
            case "BossRoom":
                EnterBoss = true;
                interraction = collision.gameObject;
                break;
            case "ShopEnterMenu":
                talkToShop = true;
                interraction = collision.gameObject;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if
        (
            collision.name == "ShopEntrance" || collision.name == "ShopExit" ||
            collision.name == "BossRoom" || collision.name == "ShopEnterMenu"
        )
        {
            talkToShop = false;
            enterShop = false;
            EnterBoss = false;
            ExitShop = false;
            canMove = true;
            DialogueManager.EndDialogue();
        }

        if (collision.name == "Default_Friction_Element" || collision.name == "PolyCollider")
            movementSpeed += 1.5f;

        if (collision.name.StartsWith("Chest "))
            DialogueManager.EndDialogue();
        if (collision.name == "Boss01")
            bossContact = false;
    }

    private IEnumerator damageOverTime(float damage, float time) {
        while (bossContact)
        {
            yield return new WaitForSeconds(time);
            TakeDamage(damage);
        }
    }

    private void FixedUpdate() {
        playerBody.MovePosition(playerBody.position + movement * movementSpeed * Time.fixedDeltaTime);
        healPotion.healLabel.text = SystemVariables.playerData.healingPotions.ToString();
        damagePotion.damageLabel.text = SystemVariables.playerData.damagePotions.ToString();
        speedPotion.speedLabel.text = SystemVariables.playerData.speedPotions.ToString();
    }
}
