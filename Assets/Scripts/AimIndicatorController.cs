using UnityEngine;

public class AimIndicatorController : MonoBehaviour
{
    public GameObject player;
    public float rotationSpeed = 10f;

    private Vector3 offset;

    private void Start()
    {
        // Calculate the initial offset between the player and the aim indicator
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse position
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Rotate the player character to face the mouse direction
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
