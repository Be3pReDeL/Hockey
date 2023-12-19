using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowPreviousMatchInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _team1Text, _team2Text, _scoreText;
    [SerializeField] private int _number;

    private void Start(){
        _team1Text.text = PlayerPrefs.GetString("Team1" + _number.ToString(), "Team 1");
        _team2Text.text = PlayerPrefs.GetString("Team2" + _number.ToString(), "Team 2");
        _scoreText.text = PlayerPrefs.GetString("TeamScore" + _number.ToString(), "5:6");
    }
}
