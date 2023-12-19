using UnityEngine;
using UnityEngine.UI;

public class CountOfTeamsButtons : MonoBehaviour
{
    [SerializeField] private Button[] _countButtons;
    [SerializeField] private Button _startButton;
    [SerializeField] private Sprite[] _blueSprites;
    [SerializeField] private Sprite[] _greenSprites;

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void CountButton(int number) {
        for(int i = 0; i < _countButtons.Length; i++)
            _countButtons[i].image.sprite = _blueSprites[i];

        _countButtons[number - 2].image.sprite = _greenSprites[number - 2];

        _startButton.interactable = true;

        TournamentSetup.Instance.SetNumberOfTeams(number);
    }
}
