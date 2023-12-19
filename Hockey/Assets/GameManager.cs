using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    private List<Team>[] _teamsToPlay;

    [SerializeField] private Animator _player1Animator, _player2Animator;

    [SerializeField] private AnimatorController[] _animatorControllers, _animatorControllersTop;
    [SerializeField] private Sprite[] _avatars;
    [SerializeField] private Image _upperTeamAvatar, _bottomTeamAvatar;
    [SerializeField] private ScoreMenuController _scoreMenuController;

    public List<Team> CurrentTeams { get; private set; } = new List<Team>();

    public bool GameEnded { get; set; } = false;

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start() {
        int numberOfMatches = TournamentSetup.NumberOfTeams * (TournamentSetup.NumberOfTeams - 1) / 2;
        _teamsToPlay = new List<Team>[numberOfMatches];

        int counter = 0;
        for(int i = 0; i < TournamentSetup.NumberOfTeams - 1; i++) {
            for(int y = i + 1; y < TournamentSetup.NumberOfTeams; y++) {
                List<Team> tempList = new List<Team> {
                    TournamentSetup.Teams[i],
                    TournamentSetup.Teams[y]
                };

                _teamsToPlay[counter] = tempList;
                counter++;
            }
        }
        
        PlayerPrefs.SetInt("Current Teams", PlayerPrefs.GetInt("Current Teams", -1) + 1);

        if(PlayerPrefs.GetInt("Current Teams", -1) < numberOfMatches){
            CurrentTeams.Add(_teamsToPlay[PlayerPrefs.GetInt("Current Teams", -1)][0]);
            CurrentTeams.Add(_teamsToPlay[PlayerPrefs.GetInt("Current Teams", -1)][1]);

            _upperTeamAvatar.sprite = CurrentTeams[0].Avatar;
            _bottomTeamAvatar.sprite = CurrentTeams[1].Avatar;

            for(int i = 0; i < _avatars.Length; i++){
                if(CurrentTeams[0].Avatar == _avatars[i]){
                    _player1Animator.runtimeAnimatorController = _animatorControllersTop[i];
                }
            }

            for(int i = 0; i < _avatars.Length; i++){
                if(CurrentTeams[1].Avatar == _avatars[i]){
                    _player2Animator.runtimeAnimatorController = _animatorControllers[i];
                }
            }
        }

        else {
            PlayerPrefs.SetInt("Current Teams", -1);

            GameEnded = true;

            _scoreMenuController.gameObject.SetActive(true);
            //_scoreMenuController.SetWinner();
        }
    }
}
