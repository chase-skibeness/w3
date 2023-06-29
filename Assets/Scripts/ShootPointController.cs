using UnityEngine;

public class ShootPointController : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse position
        Vector3 direction = mousePosition - player.transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the aim indicator to face the mouse direction
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse position
        Vector3 direction = mousePosition - player.transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the aim indicator to face the mouse direction
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
