using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainMenu : MonoBehaviour
{
    UIDocument uiDocument;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        VisualElement el = uiDocument.rootVisualElement.Q<VisualElement>("Menu-Buttons");
        el.Q<Button>("Start").clicked += () => SceneManager.LoadScene("Home");
        el.Q<Button>("Load").clicked += () => SceneManager.LoadScene("Fishing");
        el.Q<Button>("Settings");
        el.Q<Button>("Quit").clicked += () => Application.Quit();
    }
}
