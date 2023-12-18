using UnityEngine;

public class GoalkeeperController : MonoBehaviour
{
    [SerializeField] private Transform _goalCenter;
    [SerializeField] private float _radius = 1.0f; // Радиус окружности движения
    [SerializeField] private float _speed = 1.0f; // Скорость движения по окружности
    [SerializeField] private bool _isTopGoalkeeper = true; // Вратарь на верхней половине поля

    private void Update()
    {
        Vector2 directionToPuck = Puck.Instance.transform.position - _goalCenter.position;
        float targetAngle = Mathf.Atan2(directionToPuck.y, directionToPuck.x);

        if (_isTopGoalkeeper)
            targetAngle = Mathf.Clamp(targetAngle, -Mathf.PI, Mathf.PI / 24);

        else
            targetAngle = Mathf.Clamp(targetAngle, -Mathf.PI / 24, Mathf.PI);

        float currentAngle = Mathf.Atan2(transform.position.y - _goalCenter.position.y, 
                                         transform.position.x - _goalCenter.position.x);
        float newAngle = Mathf.Lerp(currentAngle, targetAngle, _speed * Time.deltaTime);

        float x = _goalCenter.position.x + _radius * Mathf.Cos(newAngle);
        float y = _goalCenter.position.y + _radius * Mathf.Sin(newAngle);

        transform.position = new Vector3(x, y, transform.position.z);

        float angleToPuck = Mathf.Atan2(directionToPuck.y, directionToPuck.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToPuck - 90));
    }
}
