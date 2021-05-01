using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject mainMenu;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}
