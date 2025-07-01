using Cavrnus.SpatialConnector.API;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TwinTempSensor : MonoBehaviour
{
    public string ContainerName;

    [SerializeField] private TMP_Text SensorName;
    [SerializeField] private TMP_Text Temperature;

    // Start is called before the first frame update
    void Start()
    {
        SensorName.text = ContainerName;

		CavrnusFunctionLibrary.AwaitAnySpaceConnection(spaceConn =>
        {
            spaceConn.BindBoolPropertyValue(ContainerName, "Crisis", ShowCrisisState);
			spaceConn.BindFloatPropertyValue(ContainerName, "Temp-C", tempC => Temperature.text = $"{tempC.ToString("f1")}°C");
		});
    }

    private void ShowCrisisState(bool crisis)
    {
		SensorHelpers.SetSensorCrisisColor(transform, crisis);
	}
}
