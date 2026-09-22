using UnityEngine;

public class MachineConditionSimulator : MonoBehaviour
{
    [Header("Temperature Simulation")]
    [SerializeField] private float heatingRate = 2f;
    [SerializeField] private float coolingRate = 4f;
    [SerializeField] private float ambientTemperature = 25f;

    [Header("Electrical Simulation")]
    [SerializeField] private float baseCurrent = 2f;
    [SerializeField] private float speedCurrentMultiplier = 1.5f;

    private void Update()
    {
        ConveyorTwinData data = ConveyorTwinData.Instance;

        if (data == null)
        {
            return;
        }

        SimulateTemperature(data);
        SimulateMotorCurrent(data);
        EvaluateMachineCondition(data);
    }

    private void SimulateTemperature(ConveyorTwinData data)
    {
        if (data.IsConveyorOperational())
        {
            float loadFactor = Mathf.Max(0.5f, data.conveyorSpeed);

            data.motorTemperature +=
                heatingRate * loadFactor * Time.deltaTime;
        }
        else
        {
            data.motorTemperature = Mathf.MoveTowards(
                data.motorTemperature,
                ambientTemperature,
                coolingRate * Time.deltaTime
            );
        }
    }

    private void SimulateMotorCurrent(ConveyorTwinData data)
    {
        if (data.IsConveyorOperational())
        {
            data.motorCurrent =
                baseCurrent +
                data.conveyorSpeed * speedCurrentMultiplier;
        }
        else
        {
            data.motorCurrent = 0f;
        }
    }

    private void EvaluateMachineCondition(ConveyorTwinData data)
    {
        if (data.currentState ==
            ConveyorTwinData.MachineState.EmergencyStopped)
        {
            return;
        }

        if (data.motorTemperature >= data.criticalTemperature)
        {
            data.currentState = ConveyorTwinData.MachineState.Fault;
            return;
        }

        if (data.motorTemperature >= data.warningTemperature &&
            data.IsConveyorOperational())
        {
            data.currentState = ConveyorTwinData.MachineState.Warning;
            return;
        }

        if (data.motorTemperature < data.warningTemperature &&
            data.currentState == ConveyorTwinData.MachineState.Warning)
        {
            data.currentState = ConveyorTwinData.MachineState.Running;
        }
    }
}