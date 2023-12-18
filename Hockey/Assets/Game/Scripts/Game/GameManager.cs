using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    
    [SerializeField] private TextMeshProUGUI _text;

    private int _leftTeamScore = 0, _rightTeamScore = 0;

    public void AddScore(bool isTopGoal){
        if(isTopGoal)
            _leftTeamScore += 1;
        else
            _rightTeamScore += 1;
    }   
}
