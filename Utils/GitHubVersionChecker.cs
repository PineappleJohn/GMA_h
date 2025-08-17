using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace mahak.Utils;
public class GitHubVersionChecker : MonoBehaviour
{
    // Example: "https://api.github.com/repos/owner/repo/releases/latest"
    public string githubApiUrl = "https://api.github.com/repos/owner/repo/releases/latest";

    public string latestVersion;

    public void Awake()
    {
        StartCoroutine(GetLatestVersion());
    }

    private IEnumerator GetLatestVersion()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(githubApiUrl))
        {
            www.SetRequestHeader("User-Agent", "UnityWebRequest");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching version: " + www.error);
            }
            else
            {
                var json = www.downloadHandler.text;
                var version = ParseTagName(json);
                Debug.Log("Latest GitHub version: " + version);

                latestVersion = version;

                if (MyPluginInfo.PLUGIN_VERSION != version)
                {
                    Plugin.outdated = true;
                }
            }
        }
    }

    private string ParseTagName(string json)
    {
        // Simple parsing for "tag_name":"v1.2.3"
        var tagKey = "\"version\":\"";
        int start = json.IndexOf(tagKey);
        if (start == -1) return "Unknown";
        start += tagKey.Length;
        int end = json.IndexOf("\"", start);
        if (end == -1) return "Unknown";
        return json.Substring(start, end - start);
    }
}
