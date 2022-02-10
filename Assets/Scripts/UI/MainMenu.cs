using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private int gameSceneBuildIndex;

        private void Awake()
        {
            playButton.onClick.AddListener(Play);
            quitButton.onClick.AddListener(Quit);
        }

        private void Play() => SceneManager.LoadScene(gameSceneBuildIndex);

        private static void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}