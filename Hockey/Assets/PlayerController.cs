using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 5.0f; // Ускорение персонажа
    public float deceleration = 2.0f; // Скорость замедления
    public bool isTopPlayer = false; // Определяет, управляется ли персонаж с верхней половины экрана
    public float tapThreshold = 0.2f; // Пороговое время для определения тапа
    public Pluck puck; // Объект шайбы
    public float shootPower = 10.0f; // Сила удара по шайбе

    private Vector2 movementDirection; // Направление движения
    private Vector2 velocity; // Текущая скорость
    private float startTouchTime; // Время начала касания
    private Vector2 startPosition; // Начальная позиция касания

    void Update()
    {
        // Обработка касаний
        HandleTouches();

        // Применение замедления
        velocity = Vector2.Lerp(velocity, Vector2.zero, deceleration * Time.deltaTime);

        // Перемещение персонажа
        transform.Translate(velocity * Time.deltaTime);
    }

    private void HandleTouches()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // Определение зоны экрана для игрока
            bool inPlayerZone = isTopPlayer ? (touch.position.y > Screen.height / 2) : (touch.position.y < Screen.height / 2);

            if (inPlayerZone)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startTouchTime = Time.time;
                        startPosition = touch.position;
                        break;

                    case TouchPhase.Moved:
                        // Определение направления движения
                        Vector2 touchDeltaPosition = touch.deltaPosition;
                        movementDirection = touchDeltaPosition.normalized;

                        // Ускорение в направлении движения
                        velocity += movementDirection * acceleration * Time.deltaTime;
                        break;

                    case TouchPhase.Ended:
                        // Проверяем, является ли это касание тапом
                        if (Time.time - startTouchTime < tapThreshold && (touch.position - startPosition).magnitude < tapThreshold)
                        {
                            Debug.Log("TAP");
                            // Атака или удар по шайбе
                            Attack();
                        }
                        break;
                }
            }
        }
    }

    private void Attack()
    {
        // Логика атаки или удара по шайбе
        if (Vector2.Distance(transform.position, puck.transform.position) < shootPower)
        {
            Vector2 direction = (puck.transform.position - transform.position).normalized;
            puck.Shoot(direction * shootPower);
        }
    }
}
