using System;
using UnityEngine;

public class ConveyorTwinData : MonoBehaviour
{
    public static ConveyorTwinData Instance { get; private set; }

    public enum MachineState
    {
        Stopped,
        Running,
        Warning,
        Fault,
        EmergencyStopped
    }

    [Header("Machine State")]
    public MachineState currentState = MachineState.Stopped;

    [Header("Operational Data")]
    [Range(0f, 3f)]
    public float conveyorSpeed = 1.5f;

    public float motorTemperature = 25f;
    public float motorCurrent = 2f;
    public int productionCount;
    public int rejectedCount;

    [Header("Sensor States")]
    public bool entrySensorActive;
    public bool exitSensorActive;

    [Header("Temperature Thresholds")]
    public float warningTemperature = 65f;
    public float criticalTemperature = 80f;

    public event Action DataChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public bool IsConveyorOperational()
    {
        return currentState == MachineState.Running ||
               currentState == MachineState.Warning;
    }

    public void StartMachine()
    {
        if (currentState == MachineState.Fault ||
            currentState == MachineState.EmergencyStopped)
        {
            return;
        }

        currentState = MachineState.Running;
        NotifyDataChanged();
    }

    public void StopMachine()
    {
        if (currentState == MachineState.EmergencyStopped)
        {
            return;
        }

        currentState = MachineState.Stopped;
        NotifyDataChanged();
    }

    public void EmergencyStop()
    {
        currentState = MachineState.EmergencyStopped;
        conveyorSpeed = 0f;
        NotifyDataChanged();
    }

    public void ResetMachine()
    {
        currentState = MachineState.Stopped;
        motorTemperature = 25f;
        conveyorSpeed = 1.5f;
        entrySensorActive = false;
        exitSensorActive = false;
        NotifyDataChanged();
    }

    public void RegisterCompletedProduct()
    {
        productionCount++;
        NotifyDataChanged();
    }

    public void RegisterRejectedProduct()
    {
        rejectedCount++;
        NotifyDataChanged();
    }

    public void NotifyDataChanged()
    {
        DataChanged?.Invoke();
    }
}