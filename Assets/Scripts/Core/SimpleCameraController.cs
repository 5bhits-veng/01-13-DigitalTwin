using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float zoomSpeed = 10f;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement =
            transform.right * horizontal +
            transform.forward * vertical;

        transform.position +=
            movement * movementSpeed * Time.deltaTime;

        float mouseScroll = Input.mouseScrollDelta.y;

        transform.position +=
            transform.forward *
            mouseScroll *
            zoomSpeed *
            Time.deltaTime;

        if (Input.GetMouseButton(1))
        {
            float mouseX =
                Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            float mouseY =
                Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, mouseX, Space.World);
            transform.Rotate(Vector3.right, -mouseY, Space.Self);
        }
    }
}