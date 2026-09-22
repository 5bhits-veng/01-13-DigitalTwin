using UnityEngine;

public class ConveyorMover : MonoBehaviour
{
    private Rigidbody productRigidbody;

    private void Awake()
    {
        productRigidbody = GetComponent<Rigidbody>();

        if (productRigidbody == null)
        {
            Debug.LogError(
                $"ConveyorMover requires a Rigidbody on {gameObject.name}."
            );
        }
    }

    private void FixedUpdate()
    {
        if (productRigidbody == null ||
            ConveyorTwinData.Instance == null ||
            !ConveyorTwinData.Instance.IsConveyorOperational())
        {
            return;
        }

        float speed = ConveyorTwinData.Instance.conveyorSpeed;
        Vector3 movement = Vector3.right * speed * Time.fixedDeltaTime;

        productRigidbody.MovePosition(
            productRigidbody.position + movement
        );
    }
}