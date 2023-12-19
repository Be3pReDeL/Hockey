using UnityEngine;
using UnityEngine.UI;

public class DisableVibration : MonoBehaviour
{
    [SerializeField] private Sprite _disableVibrationImage, _enableVibrationImage;

    private Button _disableVibrationButton;

    private void Awake()
    {
        _disableVibrationButton = GetComponent<Button>();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Vibrate", 0) == 0)
            _disableVibrationButton.image.sprite = _enableVibrationImage;

        else
            _disableVibrationButton.image.sprite = _disableVibrationImage;
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void DisableVibrationButton()
    {
        if (PlayerPrefs.GetInt("Vibrate", 0) == 0)
        {
            PlayerPrefs.SetInt("Vibrate", 1);

            _disableVibrationButton.image.sprite = _disableVibrationImage;
        }

        else
        {
            PlayerPrefs.SetInt("Vibrate", 0);

            _disableVibrationButton.image.sprite = _enableVibrationImage;
        }
    }
}
