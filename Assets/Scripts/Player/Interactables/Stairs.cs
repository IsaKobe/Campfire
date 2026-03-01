#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : InteractableTile
{
    [SerializeField] string sceneName;

#if UNITY_EDITOR
    [SerializeField] SceneAsset scene;
    private void OnValidate()
    {
        if(scene != null && scene.name == gameObject.scene.name)
        {
            scene = null;
            EditorUtility.SetDirty(this);
        }
        else if(scene.name != sceneName)
        {
            sceneName = scene.name;
            EditorUtility.SetDirty(this);
        }
    }
#endif

    public override void Interact()
    {
        SceneManager.LoadScene(sceneName);
    }
}
