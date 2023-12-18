using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public bool IsPuckAttached {get; set;} = false;
    public static bool CanGrabPuck {get; set;} = true;

    [SerializeField] private float _acceleration = 5.0f;
    [SerializeField] private float _deceleration = 2.0f;
    [SerializeField] private float _grabRadius = 1.0f;
    [SerializeField] private float _shootPower = 10.0f; 
    [SerializeField] private bool _isTopPlayer = false; 

    private const float TAPTIMETHRESHOLD = 0.2f, TAPTHRESHOLD = 0.2f, DELAYAFTERGRAB = 2.0f, DELAYAFTERSHOOT = 5.0f; 

    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _startPosition, _movementDirection;
    private float _startTouchTime;

    private static bool _isTimerRunning = false;

    private bool _toogle = false;

    private Vector2 _previousPosition, _lastMoveDirection;    

    private void Awake() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        _previousPosition = transform.position;
        _lastMoveDirection = Vector3.zero;
    }

    private void Update() {
        HandleTouches();
    }

    private void FixedUpdate() {
        if((Vector2) transform.position != _previousPosition) {
            _lastMoveDirection = ((Vector2) transform.position - _previousPosition).normalized;
            _previousPosition = transform.position;
        }

        _animator.SetFloat("x", _lastMoveDirection.x);
        _animator.SetFloat("y", _lastMoveDirection.y);

        _spriteRenderer.flipX = _lastMoveDirection.y < 0;

        _rigidBody.velocity = Vector2.Lerp(_rigidBody.velocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);
    }

    private void HandleTouches() {
        if(Input.touchCount == 0){
            _animator.SetFloat("x", 0f);
            _animator.SetFloat("y", 0f);
        }

        for (int i = 0; i < Input.touchCount; i++) {
            Touch touch = Input.GetTouch(i);

            bool inPlayerZone = _isTopPlayer ? (touch.position.y > Screen.height / 2) : (touch.position.y < Screen.height / 2);

            if (inPlayerZone) {
                switch (touch.phase) {
                    case TouchPhase.Began:
                        _startTouchTime = Time.time;
                        _startPosition = touch.position;
                        break;

                    case TouchPhase.Moved:
                        Vector2 touchDeltaPosition = touch.deltaPosition;
                        _movementDirection = touchDeltaPosition.normalized;
                        _rigidBody.AddForce(_movementDirection * _acceleration, ForceMode2D.Force);
                        break;

                    case TouchPhase.Ended:
                        if (Time.time - _startTouchTime < TAPTIMETHRESHOLD && (touch.position - _startPosition).magnitude < TAPTHRESHOLD) {
                            if (_toogle) {
                                // Обновляем направление движения перед атакой
                                _movementDirection = (touch.position - _startPosition).normalized;
                                Attack();
                            } else {
                                GrabPuck();
                            }
                            _toogle = !_toogle;
                        }
                        break;
                }
            }
        }
    }

    private void GrabPuck() {
        if (CanGrabPuck && Vector2.Distance(transform.position, Puck.Instance.transform.position) < _grabRadius) {
            CanGrabPuck = false;

            if(!_isTimerRunning)
                StartCoroutine(SetCanGrabPuck(DELAYAFTERGRAB, true));

            IsPuckAttached = true;
            Puck.Instance.AttachTo(gameObject);
        }
    }

    private void Attack() {
        if (IsPuckAttached) {
            IsPuckAttached = false;

            CanGrabPuck = false;

            if(!_isTimerRunning)
                StartCoroutine(SetCanGrabPuck(DELAYAFTERSHOOT, true));

            _animator.SetBool("Attack", true);

            Puck.Instance.Detach();
            Puck.Instance.Shoot(_lastMoveDirection, _shootPower);
        }
    }

    private IEnumerator SetCanGrabPuck(float delay, bool canGrabPuck){
        _isTimerRunning = true;

        yield return new WaitForSeconds(delay);

        _isTimerRunning = false;

        CanGrabPuck = canGrabPuck;
    }
}
