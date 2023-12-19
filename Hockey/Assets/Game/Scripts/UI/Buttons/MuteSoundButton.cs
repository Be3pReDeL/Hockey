using UnityEngine;
using UnityEngine.UI;

public class MuteSoundButton : MonoBehaviour
{
    [SerializeField] private Sprite _muteImage, _unmuteImage;

    private Button _muteButton;

    private void Awake()
    {
        _muteButton = GetComponent<Button>();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 0)
            _muteButton.image.sprite = _unmuteImage;

        else
            _muteButton.image.sprite = _muteImage;

        AudioListener.volume = 1 - PlayerPrefs.GetInt("Mute", 0);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void MuteSound()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 0)
        {
            PlayerPrefs.SetInt("Mute", 1);

            _muteButton.image.sprite = _muteImage;
        }

        else
        {
            PlayerPrefs.SetInt("Mute", 0);

            _muteButton.image.sprite = _unmuteImage;
        }

        AudioListener.volume = 1 - PlayerPrefs.GetInt("Mute", 0);
    }
}
