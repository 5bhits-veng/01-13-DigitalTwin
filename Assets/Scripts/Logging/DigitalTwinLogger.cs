using System;
using System.Globalization;
using System.IO;
using UnityEngine;

public class DigitalTwinLogger : MonoBehaviour
{
    [SerializeField] private float loggingInterval = 1f;

    private string filePath;
    private float timer;

    private void Start()
    {
        string fileName =
            $"ConveyorTwin_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

        filePath = Path.Combine(
            Application.persistentDataPath,
            fileName
        );

        string header =
            "Timestamp,State,Speed,Temperature,Current," +
            "ProductionCount,EntrySensor,ExitSensor\n";

        File.WriteAllText(filePath, header);

        Debug.Log($"Digital Twin log created at: {filePath}");
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer < loggingInterval)
        {
            return;
        }

        timer = 0f;
        WriteLogEntry();
    }

    private void WriteLogEntry()
    {
        ConveyorTwinData data = ConveyorTwinData.Instance;

        if (data == null)
        {
            return;
        }

        string line = string.Join(",",
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            data.currentState,
            data.conveyorSpeed.ToString(
                "F2",
                CultureInfo.InvariantCulture),
            data.motorTemperature.ToString(
                "F2",
                CultureInfo.InvariantCulture),
            data.motorCurrent.ToString(
                "F2",
                CultureInfo.InvariantCulture),
            data.productionCount,
            data.entrySensorActive,
            data.exitSensorActive
        );

        File.AppendAllText(filePath, line + Environment.NewLine);
    }

    public string GetLogFilePath()
    {
        return filePath;
    }
}