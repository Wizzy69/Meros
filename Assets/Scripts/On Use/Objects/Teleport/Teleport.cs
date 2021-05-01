using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Teleport : MonoBehaviour
{

    public Camera currentCamera;
    public Camera newCamera;
    public float teleportDelay;
    public Vector2 teleportLocation;
    public ParticleSystem particleSystem;

    public GameObject newRegion;
    public GameObject currentRegion;

    private bool inRange = false;
    private Transform colTransform;
    private bool isPlayer;

    private void Start() {
        if (GetComponent<PlayerMovement>() != null) isPlayer = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (isPlayer) return;
        if (collision.name == "Player")
        {
            inRange = true;
            colTransform = collision.transform;
            GetComponent<DialogueTrigger>().TriggerDialogue();

        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (isPlayer) return;
        if (collision.name == "Player" && inRange)
        {
            inRange = false;
            FindObjectOfType<DialogueManager>().DisplayNextLine();
        }
    }

    private void Update() {
        if (inRange)
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<DialogueManager>().DisplayNextLine();
                inRange = false;
                TeleportPlayer(colTransform);
            }
    }

    //public Camera CameraThatFollowThePlayer;

    public void TeleportPlayer(Transform playerPosition) {
        Debug.LogWarning($"Teleporting Player to position : X {teleportLocation.x} Y : {teleportLocation.y}");
        if (particleSystem == null)
        {
            //Debug.LogError(newRegion.name);
            if (newRegion.name == "Map02")
            {
                SystemVariables.playerData.ZoneName = "MapZone2";
                playerPosition.gameObject.GetComponent<PlayerMovement>().hpBar.value = playerPosition.gameObject.GetComponent<PlayerMovement>().hpBar.maxValue;
            }
            if (newRegion.name == "Harta")
            {
                SystemVariables.playerData.ZoneName = "MapZone1";
                playerPosition.gameObject.GetComponent<PlayerMovement>().hpBar.value = playerPosition.gameObject.GetComponent<PlayerMovement>().hpBar.maxValue;
            }

            newRegion.SetActive(true);
            playerPosition.position = new Vector3(teleportLocation.x, teleportLocation.y, playerPosition.position.z);
            currentRegion.SetActive(false);
            if (newCamera != null && currentCamera != null)
            {
                currentCamera.gameObject.SetActive(false);
                newCamera.gameObject.SetActive(true);
            }
            return;
        }
        StartCoroutine(teleport(playerPosition, newCamera, currentCamera, teleportDelay));
    }

    IEnumerator teleport(Transform playerPosition, Camera newCamera, Camera current, float seconds) {

        particleSystem.transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, particleSystem.transform.position.z);
        yield return particleSystem.startSpeed = -16;
        particleSystem.Play();
        yield return new WaitForSeconds(seconds);
        //Debug.LogError(newRegion.name);
        if (newRegion.name == "Map02")
        {
            SystemVariables.playerData.ZoneName = "MapZone2";
            playerPosition.gameObject.GetComponent<PlayerMovement>().hpBar.value = playerPosition.gameObject.GetComponent<PlayerMovement>().hpBar.maxValue;
        }
        if (newRegion.name == "Harta")
        {
            SystemVariables.playerData.ZoneName = "MapZone1";
            playerPosition.gameObject.GetComponent<PlayerMovement>().hpBar.value = playerPosition.gameObject.GetComponent<PlayerMovement>().hpBar.maxValue;
        }
        if (newCamera != null && current != null)
        {
            current.gameObject.SetActive(false);
            newCamera.gameObject.SetActive(true);
        }

        newRegion.SetActive(true);
        playerPosition.position = new Vector3(teleportLocation.x, teleportLocation.y, playerPosition.position.z);
        currentRegion.SetActive(false);
        particleSystem.transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, particleSystem.transform.position.z);
        yield return particleSystem.startSpeed = 16;
        yield return new WaitForSeconds(seconds);
        particleSystem.Stop();


    }
}
