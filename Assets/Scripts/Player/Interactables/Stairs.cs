using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : InteractableTile
{
    [SerializeField] SceneAsset scene;
    private void OnValidate()
    {
        if(scene != null && scene.name == gameObject.scene.name)
        {
            scene = null;
            EditorUtility.SetDirty(this);
        }
    }
    public override void Interact()
    {
        SceneManager.LoadScene(scene.name);
    }
}
