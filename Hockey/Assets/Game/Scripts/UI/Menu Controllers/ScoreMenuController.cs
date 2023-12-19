using UnityEngine;
using OPS;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
using System.Text.RegularExpressions;

public class ScoreMenuController : UIController
{
    [SerializeField] private float _duration = 1f;

    [SerializeField] private TextMeshProUGUI _winnerTeamText, _looserTeamText;

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void OnEnable()
    {
        for (int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Appear(_duration);
        }

        SetWinner();
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void Back()
    {
        for(int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Disappear(_duration);
        }
    }

    public void SetWinner(){
        if(GameManager.Instance.GameEnded){
            _winnerTeamText.text = "Tournament Over!";
            _looserTeamText.text = "Thank you!";

            LoadScene.LoadPreviousScene();
        }
            

        else{
            if(ScoreManager.Instance.BottomTeamScore < ScoreManager.Instance.UpperTeamScore){
                _winnerTeamText.text = GameManager.Instance.CurrentTeams[1].Name;
                _looserTeamText.text = GameManager.Instance.CurrentTeams[0].Name;
            }

            else {
                _winnerTeamText.text = GameManager.Instance.CurrentTeams[0].Name;
                _looserTeamText.text = GameManager.Instance.CurrentTeams[1].Name;
            }
        }
        

        GameManager.Instance.CurrentTeams.Clear();

        StartCoroutine(LoadNext());
    }

    private IEnumerator LoadNext(){
        yield return new WaitForSeconds(3f);

        Debug.Log("HEYYY");

        if(GameManager.Instance.GameEnded)
            LoadScene.LoadPreviousScene();
        
        else
            LoadScene.LoadSceneByRelativeIndex(0);
    }
}
