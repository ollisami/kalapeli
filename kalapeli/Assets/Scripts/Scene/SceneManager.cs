using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {
    public Text input;
    public GameObject button;
    public GameObject inputObj;
    public GameObject loadingText;

    void Start()
    {
        string savedName = PlayerPrefs.GetString("username", "");
        if (savedName.Length > 0) {
            input.text = savedName;
        }
    }

    public void LoadGameScene()
    {
        if (input.text.Length == 0) return;

        button.SetActive(false);
        inputObj.SetActive(false);
        loadingText.SetActive(true);
        PlayerPrefs.SetString("username", input.text);
        Application.LoadLevel(1);
    }
}
