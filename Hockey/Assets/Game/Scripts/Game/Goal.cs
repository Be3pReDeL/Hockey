using UnityEngine;

public class Goal : MonoBehaviour {
    [SerializeField] private bool _upperGoal;

    private AudioSource _audioSource;

    private void Awake(){
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D) {
        if(collider2D.tag == "Puck") {
            if(_upperGoal)
                ScoreManager.Instance.UpperTeamScore += 1;
            else   
                ScoreManager.Instance.BottomTeamScore += 1;

            Puck.Instance.transform.position = Vector3.zero;
            Puck.Instance.RigidBody.velocity = Vector2.zero;

            _audioSource.Play();
            Puck.Instance.GetComponent<AudioSource>().Play();
        }
    }
}
