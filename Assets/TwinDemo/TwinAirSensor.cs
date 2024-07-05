using CavrnusSdk.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class SensorHelpers
{
	private static Color32 backgroundColor => new Color32(52, 63, 84, 255);
	private static Color32 backgroundColorCrisis => new Color32(62, 21, 20, 255);

	private static Color32 contentColor => new Color32(52, 63, 84, 255);
	private static Color32 contentColorCrisis => new Color32(241, 163, 154, 255);

	private static Color32 valueLableColor => new Color32(22, 26, 39, 255);
	private static Color32 valueLableColorCrisis => new Color32(30, 9, 9, 255);

	private static Color32 valueColor => new Color32(32, 38, 56, 255);
	private static Color32 valueColorCrisis => new Color32(38, 11, 11, 255);

	public static void SetSensorCrisisColor(Transform parent, bool crisis)
	{
		if(parent.GetComponent<TMP_Text>() != null)
			parent.GetComponent<TMP_Text>().color = SwapColor(parent.GetComponent<TMP_Text>().color, crisis);

		if (parent.GetComponent<Image>() != null)
			parent.GetComponent<Image>().color = SwapColor(parent.GetComponent<Image>().color, crisis);

		for (int i = 0; i < parent.childCount; i++)
		{
			SetSensorCrisisColor(parent.GetChild(i), crisis);
		}
	}

	private static Color SwapColor(Color color, bool crisis)
	{
		if(crisis && color == backgroundColor)
			return backgroundColorCrisis;
		if (!crisis && color == backgroundColorCrisis)
			return backgroundColor;

		if (crisis && color == contentColor)
			return contentColorCrisis;
		if (!crisis && color == contentColorCrisis)
			return contentColor;

		if (crisis && color == valueLableColor)
			return valueLableColorCrisis;
		if (!crisis && color == valueLableColorCrisis)
			return valueLableColor;

		if (crisis && color == valueColor)
			return valueColorCrisis;
		if (!crisis && color == valueColorCrisis)
			return valueColor;

		return color;
	}
}

public class TwinAirSensor : MonoBehaviour
{
	public string ContainerName;

	[SerializeField] private TMP_Text SensorName;
	[SerializeField] private TMP_Text CO;
	[SerializeField] private TMP_Text NO2;

	// Start is called before the first frame update
	void Start()
	{
		SensorName.text = ContainerName;

		CavrnusFunctionLibrary.AwaitAnySpaceConnection(spaceConn =>
		{
			spaceConn.BindBoolPropertyValue(ContainerName, "Crisis", ShowCrisisState);
			spaceConn.BindFloatPropertyValue(ContainerName, "CO-ppm", co => CO.text = $"{co.ToString("f1")}ppm");
			spaceConn.BindFloatPropertyValue(ContainerName, "NO2-ppb", no2 => NO2.text = $"{no2.ToString("f1")}ppb");
		});
	}

	private void ShowCrisisState(bool crisis)
	{
		SensorHelpers.SetSensorCrisisColor(transform, crisis);
	}
}
