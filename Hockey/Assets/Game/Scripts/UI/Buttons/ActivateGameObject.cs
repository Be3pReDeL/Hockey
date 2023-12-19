using UnityEngine;

public class ActivateGameObject : MonoBehaviour
{
    [OPS.Obfuscator.Attribute.DoNotRename]
    public void Activate(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
}
