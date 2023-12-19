using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _bottomTeamScoreText, _upperTeamScoreText, _timerText;
    [SerializeField] private GameObject _goalVFX;
    [SerializeField] private ScoreMenuController _scoreMenuController;

    [Serializable]
    private class UnityEventIntTMPUGUI : UnityEvent<int, TextMeshProUGUI> { }
    private UnityEventIntTMPUGUI _onTeamScoreChanged;

    private float _timer = 300;

    private int _bottomTeamScore = 0;
    public int BottomTeamScore {
        get { return _bottomTeamScore; }
        set {
            _bottomTeamScore = value;

            _onTeamScoreChanged?.Invoke(_bottomTeamScore, _bottomTeamScoreText);
        }
    }

    private int _upperTeamScore = 0;
    public int UpperTeamScore {
        get { return _upperTeamScore; }
        set{
            _upperTeamScore = value;

            _onTeamScoreChanged?.Invoke(_upperTeamScore, _upperTeamScoreText);
        }
    }

    private void OnEnable() {
        if (_onTeamScoreChanged == null)
            _onTeamScoreChanged = new UnityEventIntTMPUGUI();

        _onTeamScoreChanged.AddListener(ChangeTeamScore);
    }

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Update() {
        if(_timer > 0) {
            _timer -= Time.deltaTime;

            // Format the time as minutes:seconds
            float minutes = _timer / 60;
            float seconds = _timer % 60;
            _timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        else {
            // Call Win when the timer reaches zero
            if(!_scoreMenuController.gameObject.activeSelf) {
                Win();
            }
        }    
    }


    private void ChangeTeamScore(int amount, TextMeshProUGUI teamScoreText) {
        if(amount <= 6){
            teamScoreText.text = amount.ToString() + " goals";

            _goalVFX.SetActive(true);
        }

        else 
            Win();
    }

    private void Win(){
       _scoreMenuController.gameObject.SetActive(true);
    }

    private void OnDisable() {
        _onTeamScoreChanged.RemoveListener(ChangeTeamScore);
    }
}
