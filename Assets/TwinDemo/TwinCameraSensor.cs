using Cavrnus.SpatialConnector.API;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TwinCameraSensor : MonoBehaviour
{
    public string ContainerName;

    [SerializeField] private TMP_Text SensorName;
    [SerializeField] private Image Picture;

    // Start is called before the first frame update
    void Start()
    {
        SensorName.text = ContainerName;

        CavrnusFunctionLibrary.AwaitAnySpaceConnection(spaceConn =>
        {
            spaceConn.BindStringPropertyValue(ContainerName, "Picture", picUrl => StartCoroutine(DownloadImage(picUrl)));
        });
    }

    IEnumerator DownloadImage(string url)
    {
        if (string.IsNullOrEmpty(url))
            yield break;

        Debug.Log($"Fetching url {url}");

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error loading image: " + request.error);
            yield break;
        }

        Texture2D texture = DownloadHandlerTexture.GetContent(request);

        // Convert Texture2D to Sprite
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        Sprite sprite = Sprite.Create(texture, rect, pivot);

        // Assign to Image component
        Picture.sprite = sprite;
    }
}
