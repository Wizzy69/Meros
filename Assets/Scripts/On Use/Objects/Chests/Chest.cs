using UnityEngine;

public class Chest : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    public string ChestName;

    public float maxMoney;

    public float minMoney;
    private float money;
    private bool opened;

    private bool inRange;
    private void Start() {

        switch (ChestName)
        {
            case "Chest 1":
                if (SystemVariables.playerData.chest1Opened) opened = true;
                break;
            case "Chest 2":
                if (SystemVariables.playerData.chest2Opened) opened = true;
                break;
            case "Chest 3":
                if (SystemVariables.playerData.chest3Opened) opened = true;
                break;
            case "Hidden Chest":
                if (SystemVariables.playerData.chestHiddenOpened) opened = true;
                break;
        }


        money = Mathf.RoundToInt(Random.Range(minMoney, maxMoney));
        boxCollider = GetComponent<BoxCollider2D>();


    }

    private void Update() {
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueTrigger.TriggerDialogue(new Dialogue()
                {
                    NPCName = "Chest",
                    sentances = new string[]
                    {
                        "You have been rewarded with " + money + " Jz"
                    }
                });
                this.GetComponent<BoxCollider2D>().isTrigger = false;
                if (GetComponent<Achievement>() != null)
                    this.GetComponent<Achievement>().TriggerAchievement();
                SystemVariables.playerData.money += money;
                opened = true;
                inRange = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (opened) return;
        if (collider.name == "Player")
        {
            DialogueTrigger.TriggerDialogue(new Dialogue()
            {
                NPCName = "Chest",
                sentances = new string[]
                    {
                        "Press [E] to collect loot."
                    }
            });
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (opened) return;
        if (collision.name == "Player")
        {
            inRange = false;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        boxCollider = GetComponent<BoxCollider2D>();
        Gizmos.DrawWireCube(transform.position, new Vector3(boxCollider.size.x, boxCollider.size.y, 0f));
    }
}