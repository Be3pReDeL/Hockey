using UnityEngine;
using OPS;

public class TutorialMenuController : UIController
{
    [SerializeField] private float _duration = 1f;

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void OnEnable()
    {
        for (int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Appear(_duration);
        }
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void Back()
    {
        for(int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Disappear(_duration);
        }
    }
}
