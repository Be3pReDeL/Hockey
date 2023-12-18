using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private bool _upperGoal;

    private void OnTriggerEnter2D(Collider2D collider2D){
        GameManager.Instance.AddScore(_upperGoal);
    }
}
