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

    //public Camera CameraThatFollowThePlayer;

    public void TeleportPlayer(Transform playerPosition)
    {
        Debug.LogWarning($"Teleporting Player to position : X {teleportLocation.x} Y : {teleportLocation.y}");
        if (particleSystem == null)
        {
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

    IEnumerator teleport(Transform playerPosition, Camera newCamera, Camera current, float seconds)
    {

        particleSystem.transform.position = new Vector3(playerPosition.position.x, playerPosition.position.y, particleSystem.transform.position.z);
        yield return particleSystem.startSpeed = -16;
        particleSystem.Play();
        yield return new WaitForSeconds(seconds);

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
