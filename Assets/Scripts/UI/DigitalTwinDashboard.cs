using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DigitalTwinDashboard : MonoBehaviour
{
    [Header("Text Fields")]
    [SerializeField] private TMP_Text stateText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text temperatureText;
    [SerializeField] private TMP_Text currentText;
    [SerializeField] private TMP_Text productionText;
    [SerializeField] private TMP_Text entrySensorText;
    [SerializeField] private TMP_Text exitSensorText;
    [SerializeField] private TMP_Text alarmText;

    [Header("Visual Elements")]
    [SerializeField] private Image statusIndicator;
    [SerializeField] private Slider speedSlider;

    [Header("Status Colours")]
    [SerializeField] private Color stoppedColour = Color.gray;
    [SerializeField] private Color runningColour = Color.green;
    [SerializeField] private Color warningColour = Color.yellow;
    [SerializeField] private Color faultColour = Color.red;

    private void Start()
    {
        if (speedSlider != null &&
            ConveyorTwinData.Instance != null)
        {
            speedSlider.value =
                ConveyorTwinData.Instance.conveyorSpeed;
        }
    }

    private void Update()
    {
        RefreshDashboard();
    }

    private void RefreshDashboard()
    {
        ConveyorTwinData data = ConveyorTwinData.Instance;

        if (data == null)
        {
            return;
        }

        stateText.text = $"STATE: {data.currentState}";
        speedText.text = $"SPEED: {data.conveyorSpeed:F1} m/s";
        temperatureText.text =
            $"MOTOR TEMPERATURE: {data.motorTemperature:F1} °C";
        currentText.text =
            $"MOTOR CURRENT: {data.motorCurrent:F1} A";
        productionText.text =
            $"COMPLETED PRODUCTS: {data.productionCount}";

        entrySensorText.text =
            $"ENTRY SENSOR: {(data.entrySensorActive ? "DETECTED" : "CLEAR")}";

        exitSensorText.text =
            $"EXIT SENSOR: {(data.exitSensorActive ? "DETECTED" : "CLEAR")}";

        UpdateStatusVisuals(data.currentState);
    }

    private void UpdateStatusVisuals(
        ConveyorTwinData.MachineState state)
    {
        switch (state)
        {
            case ConveyorTwinData.MachineState.Running:
                statusIndicator.color = runningColour;
                alarmText.text = "SYSTEM OPERATING NORMALLY";
                break;

            case ConveyorTwinData.MachineState.Warning:
                statusIndicator.color = warningColour;
                alarmText.text =
                    "WARNING: MOTOR TEMPERATURE IS HIGH";
                break;

            case ConveyorTwinData.MachineState.Fault:
                statusIndicator.color = faultColour;
                alarmText.text =
                    "FAULT: MOTOR OVERHEATING — RESET REQUIRED";
                break;

            case ConveyorTwinData.MachineState.EmergencyStopped:
                statusIndicator.color = faultColour;
                alarmText.text =
                    "EMERGENCY STOP ACTIVATED";
                break;

            default:
                statusIndicator.color = stoppedColour;
                alarmText.text = "SYSTEM STOPPED";
                break;
        }
    }

    public void StartMachine()
    {
        ConveyorTwinData.Instance?.StartMachine();
    }

    public void StopMachine()
    {
        ConveyorTwinData.Instance?.StopMachine();
    }

    public void EmergencyStop()
    {
        ConveyorTwinData.Instance?.EmergencyStop();
    }

    public void ResetMachine()
    {
        ConveyorTwinData.Instance?.ResetMachine();

        if (speedSlider != null &&
            ConveyorTwinData.Instance != null)
        {
            speedSlider.value =
                ConveyorTwinData.Instance.conveyorSpeed;
        }
    }

    public void ChangeSpeed(float newSpeed)
    {
        if (ConveyorTwinData.Instance == null)
        {
            return;
        }

        ConveyorTwinData.Instance.conveyorSpeed = newSpeed;
    }
}