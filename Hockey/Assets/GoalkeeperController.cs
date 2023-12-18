using UnityEngine;

public class GoalkeeperController : MonoBehaviour
{
    public Transform goalCenter; // Центр ворот
    public GameObject puck; // Шайба
    public float radius = 1.0f; // Радиус окружности движения
    public float speed = 1.0f; // Скорость движения по окружности
    public bool isTopGoalkeeper = true; // Вратарь на верхней половине поля

    void Update()
    {
        // Направление от центра ворот к шайбе
        Vector2 directionToPuck = puck.transform.position - goalCenter.position;

        // Вычисляем угол, основанный на положении шайбы
        float targetAngle = Mathf.Atan2(directionToPuck.y, directionToPuck.x);

        // Ограничиваем угол для движения только перед воротами
        if (isTopGoalkeeper)
        {
            targetAngle = Mathf.Clamp(targetAngle, -Mathf.PI, Mathf.PI / 24);
        }
        else
        {
            targetAngle = Mathf.Clamp(targetAngle, -Mathf.PI / 24, Mathf.PI);
        }

        // Плавно поворачиваем вратаря в направлении шайбы
        float currentAngle = Mathf.Atan2(transform.position.y - goalCenter.position.y, 
                                         transform.position.x - goalCenter.position.x);
        float newAngle = Mathf.Lerp(currentAngle, targetAngle, speed * Time.deltaTime);

        // Вычисляем новую позицию вратаря
        float x = goalCenter.position.x + radius * Mathf.Cos(newAngle);
        float y = goalCenter.position.y + radius * Mathf.Sin(newAngle);

        // Обновляем позицию вратаря
        transform.position = new Vector3(x, y, transform.position.z);

        // Поворачиваем вратаря в сторону шайбы
        float angleToPuck = Mathf.Atan2(directionToPuck.y, directionToPuck.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToPuck - 90));
    }
}
