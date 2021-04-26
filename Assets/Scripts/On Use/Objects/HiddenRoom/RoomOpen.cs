using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RoomOpen : MonoBehaviour
{
    public GameObject hiddenRoom;
    public Camera playerCamera;
    private bool isOpened;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "Player" && !isOpened)
        {
            isOpened = !isOpened;
            hiddenRoom.gameObject.SetActive(isOpened);
            playerCamera.GetComponent<Camera_FollowPlayer>().ignoreBorders = isOpened;
        }
    }
}
