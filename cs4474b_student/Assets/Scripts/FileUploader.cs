using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;
using System.IO;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class FileUploader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText; // optional feedback label

    public void OpenFileBrowser()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("JSON Files", ".json"));
        FileBrowser.SetDefaultFilter(".json");
        StartCoroutine(ShowFileBrowser());
    }

    IEnumerator ShowFileBrowser()
    {
        yield return FileBrowser.WaitForLoadDialog(
            FileBrowser.PickMode.Files,
            false,
            null,
            null,
            "Select Input JSON",
            "Load"
        );

        if (FileBrowser.Success)
        {
            string sourcePath = FileBrowser.Result[0];
            string destPath = Path.Combine(Application.streamingAssetsPath, "input.json");

            File.Copy(sourcePath, destPath, overwrite: true);

            if (statusText != null)
                statusText.text = "File loaded successfully!";

            Debug.Log($"JSON copied to: {destPath}");
        }
        else
        {
            if (statusText != null)
                statusText.text = "No file selected.";
        }
    }

    public void startGame()
    {
        SceneManager.LoadScene("Dashboard");
    }
}