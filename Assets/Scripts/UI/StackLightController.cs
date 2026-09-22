using UnityEngine;

public class StackLightController : MonoBehaviour
{
    [SerializeField] private Renderer redLight;
    [SerializeField] private Renderer yellowLight;
    [SerializeField] private Renderer greenLight;

    [SerializeField]
    private Color offColour =
        new Color(0.08f, 0.08f, 0.08f);

    [SerializeField] private Color redColour = Color.red;
    [SerializeField] private Color yellowColour = Color.yellow;
    [SerializeField] private Color greenColour = Color.green;

    private void Update()
    {
        if (ConveyorTwinData.Instance == null)
        {
            return;
        }

        TurnAllLightsOff();

        switch (ConveyorTwinData.Instance.currentState)
        {
            case ConveyorTwinData.MachineState.Running:
                SetLight(greenLight, greenColour);
                break;

            case ConveyorTwinData.MachineState.Warning:
                SetLight(yellowLight, yellowColour);
                break;

            case ConveyorTwinData.MachineState.Fault:
            case ConveyorTwinData.MachineState.EmergencyStopped:
                SetLight(redLight, redColour);
                break;
        }
    }

    private void TurnAllLightsOff()
    {
        SetLight(redLight, offColour);
        SetLight(yellowLight, offColour);
        SetLight(greenLight, offColour);
    }

    private void SetLight(Renderer targetRenderer, Color colour)
    {
        if (targetRenderer == null)
        {
            return;
        }

        targetRenderer.material.color = colour;

        if (colour != offColour)
        {
            targetRenderer.material.EnableKeyword("_EMISSION");
            targetRenderer.material.SetColor(
                "_EmissionColor",
                colour * 2f
            );
        }
        else
        {
            targetRenderer.material.DisableKeyword("_EMISSION");
        }
    }
}