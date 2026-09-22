using UnityEngine;

public class MotorRotation : MonoBehaviour
{
    [SerializeField] private float rotationMultiplier = 150f;

    private void Update()
    {
        ConveyorTwinData data = ConveyorTwinData.Instance;

        if (data == null || !data.IsConveyorOperational())
        {
            return;
        }

        float rotationSpeed =
            data.conveyorSpeed * rotationMultiplier;

        transform.Rotate(
            Vector3.right,
            rotationSpeed * Time.deltaTime,
            Space.Self
        );
    }
}