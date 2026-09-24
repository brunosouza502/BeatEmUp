using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [Header("Player References")]
    private GameObject player;
    private Vector3 playerPosition;

    [Header("Movement Limits")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    private void Start()
    {
        // Finds the player in the scene through the PlayerController component so the camera can follow it.
        player = FindObjectOfType<PlayerController>().gameObject;
        playerPosition = player.transform.position;
        
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        // Stores the player's position so the camera can follow it.
        playerPosition = player.transform.position;

        // Follows the player only on the X axis while preserving the camera's Y and Z positions.
        transform.position = new Vector3(playerPosition.x, transform.position.y, transform.position.z);

        // Prevents the camera from exceeding the movement limits configured in the Unity Inspector.
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
    }
}
