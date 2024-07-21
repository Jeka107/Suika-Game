using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerMovementBoundary;
    [SerializeField] private float extraWidth;

    private float currntMovementBoundary;
    private void Awake()
    {
        currntMovementBoundary = playerMovementBoundary;
    }
    private void Update()
    {
        Vector3 newPosition = transform.position + new Vector3(InputManager.instance.moveInput.x * moveSpeed * Time.deltaTime, 0f, 0f);
        newPosition.x = Mathf.Clamp(newPosition.x, -currntMovementBoundary, currntMovementBoundary);
        transform.position = newPosition;
    }
    public void ChangeBoundry()
    {
        currntMovementBoundary = playerMovementBoundary - BallsController.instance.bounds.extents.x+ extraWidth;
    }
}
