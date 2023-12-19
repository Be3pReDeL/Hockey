using UnityEngine;

public class Puck : MonoBehaviour {
    public static Puck Instance {get; private set;} 
    [SerializeField] private GameObject _attachedTarget;
    [SerializeField] private float _deceleration = 5.0f;

    private bool _isAttached = false, _isShooting = false;

    public Rigidbody2D RigidBody;
    private CapsuleCollider2D _capsuleCollider2D;

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);

        RigidBody = Instance.GetComponent<Rigidbody2D>();
        _capsuleCollider2D = Instance.GetComponent<CapsuleCollider2D>();
    }

    private void Update() {
        if(_isAttached){
            FollowTarget();
        }
    }

    private void FixedUpdate() {
        RigidBody.velocity = Vector2.Lerp(RigidBody.velocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);
    }

    public void AttachTo(GameObject target) {
        if(!_isAttached){
            _isAttached = true;

            _attachedTarget = target;
        }
    }

    public void Detach(){
        if(_isAttached){
            _isAttached = false;

            _attachedTarget = null;
        }
    }

    public void Shoot(Vector2 direction, float shootPower) {
        RigidBody.AddForce(direction * shootPower, ForceMode2D.Impulse);

        _capsuleCollider2D.isTrigger = false;
    }

    private void FollowTarget(){
        if(_attachedTarget != null)
            transform.position = _attachedTarget.transform.position;

        _capsuleCollider2D.isTrigger = true;
    }
}
