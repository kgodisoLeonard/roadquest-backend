using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RoadLoader : MonoBehaviour
{
    [Header("Backend Settings")]
    public string backendUrl = "http://localhost:3000/api/roads";

    [Header("Test Bounding Box (Polokwane Area)")]
    public string boundingBox = "-24.1,29.3,-23.7,29.7"; // minLat,minLon,maxLat,maxLon

    private RoadParser roadParser;

    void Start()
    {
        // Find the RoadParser component attached to this same GameObject
        roadParser = GetComponent<RoadParser>();

        if (roadParser == null)
        {
            Debug.LogError("RoadParser component missing! Please attach RoadParser to the same GameObject.");
            return;
        }

        // Automatically fetch roads when the game starts
        StartCoroutine(FetchRoadsFromServer());
    }

    IEnumerator FetchRoadsFromServer()
    {
        string fullUrl = $"{backendUrl}?bbox={boundingBox}";
        Debug.Log($"Requesting roads from: {fullUrl}");

        using (UnityWebRequest request = UnityWebRequest.Get(fullUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to fetch roads from backend: {request.error}");
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("Successfully loaded live road data from backend!");
                
                // Pass the JSON data straight into your RoadParser script to draw the roads
                roadParser.BuildRoadsFromJson(jsonResponse);
            }
        }
    }
}