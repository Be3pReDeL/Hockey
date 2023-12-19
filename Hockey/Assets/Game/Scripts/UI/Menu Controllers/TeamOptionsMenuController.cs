using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TeamOptionsMenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerNumberText;
    [SerializeField] private TMP_InputField TMP_InputField;

    [SerializeField] private Button _continueButton; 

    private Sprite _selectedSprite;
    private string _teamName;
    private int _teamNumber = 1;

    private void Start(){
        _teamName = TMP_InputField.text;
    }

    public void ChooseAvatar(Sprite sprite) {
        _selectedSprite = sprite;

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;

        _continueButton.interactable = true;
    }

    public void GetTextInput(){
        _teamName = TMP_InputField.text;
    }

    public void Continue() {
        TournamentSetup.Instance.RegisterTeam(_teamName, _selectedSprite);

        TMP_InputField.text = "";
        _playerNumberText.text = "Player " + (++_teamNumber).ToString(); 

        _selectedSprite = null;
        _teamName = null;        

        _continueButton.interactable = false;
    }
}
