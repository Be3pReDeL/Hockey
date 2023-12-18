using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TournamentSetup : MonoBehaviour
{
    public static TournamentSetup Instance {get; private set;}
    public static int NumberOfTeams { get; private set; }
    public static List<Team> Teams { get; private set; } = new List<Team>();

    private void Awake(){
        DontDestroyOnLoad(this);

        if(Instance == null)
            Instance = this;

        else 
            Destroy(this);
    }

    public void SetNumberOfTeams(int number) {
        NumberOfTeams = number;
    }

    public void RegisterTeam(string name, Sprite avatar) {
        if (Teams.Count < NumberOfTeams)
        {
            Teams.Add(new Team(name, avatar));

            if (Teams.Count == NumberOfTeams) {
                LoadScene.LoadNextScene();
            }
        }
    }
}
