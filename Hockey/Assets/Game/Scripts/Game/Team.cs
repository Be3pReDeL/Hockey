using UnityEngine;

[System.Serializable]
public class Team
{
    public string Name;
    public Sprite Avatar;

    public Team(string name, Sprite avatar)
    {
        Name = name;
        Avatar = avatar;
    }
}
