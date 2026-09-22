using UnityEngine;

public class IndustrialSensor : MonoBehaviour
{
    public enum SensorType
    {
        Entry,
        Exit
    }

    [SerializeField] private SensorType sensorType;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Product") ||
            ConveyorTwinData.Instance == null)
        {
            return;
        }

        if (sensorType == SensorType.Entry)
        {
            ConveyorTwinData.Instance.entrySensorActive = true;
        }
        else
        {
            ConveyorTwinData.Instance.exitSensorActive = true;
            ConveyorTwinData.Instance.RegisterCompletedProduct();

            Destroy(other.gameObject, 0.25f);
        }

        ConveyorTwinData.Instance.NotifyDataChanged();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Product") ||
            ConveyorTwinData.Instance == null)
        {
            return;
        }

        if (sensorType == SensorType.Entry)
        {
            ConveyorTwinData.Instance.entrySensorActive = false;
        }
        else
        {
            ConveyorTwinData.Instance.exitSensorActive = false;
        }

        ConveyorTwinData.Instance.NotifyDataChanged();
    }
}