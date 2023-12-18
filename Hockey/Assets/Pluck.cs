using UnityEngine;

public class Pluck : MonoBehaviour
{
    public GameObject attachedPlayer; // Игрок, к которому прикреплена шайба
    public float stickRadius = 1.0f; // Радиус, в котором шайба прикрепляется к игроку
    public float shootPower = 10.0f; // Сила удара по шайбе
    public float deceleration = 5.0f; // Замедление скольжения шайбы
    public float interceptionDelay = 2.0f; // Задержка перед возможностью перехвата
    public float noAttachmentTime = 1.0f; // Время, в течение которого шайба не может прикрепляться после удара

    private Vector2 velocity; // Текущая скорость шайбы
    private float timeSinceAttachment = 0; // Время с момента последнего прикрепления
    private float timeSinceLastShot = 0; // Время с момента последнего удара

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandlePuckMovement();
        CheckAttachment();

        if (attachedPlayer != null)
        {
            timeSinceAttachment += Time.deltaTime;
        }

        if (timeSinceLastShot < noAttachmentTime)
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }

    private void HandlePuckMovement()
    {
        if (attachedPlayer == null)
        {
            velocity = Vector2.Lerp(velocity, Vector2.zero, deceleration * Time.deltaTime);
            transform.Translate(velocity * Time.deltaTime);
        }
        else
        {
            transform.position = attachedPlayer.transform.position;
        }
    }

    private void CheckAttachment()
    {
        foreach (var player in FindObjectsOfType<PlayerController>())
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (player.gameObject != attachedPlayer && distance <= stickRadius)
            {
                // Условие для прикрепления: шайба не прикреплена, прошло достаточно времени с момента удара и перехвата
                if (attachedPlayer == null && timeSinceLastShot >= noAttachmentTime && timeSinceAttachment > interceptionDelay)
                {
                    attachedPlayer = player.gameObject;
                    timeSinceAttachment = 0;
                    break; // Прикрепляем шайбу и выходим из цикла
                }
            }
        }

        // Обновляем таймеры, если шайба прикреплена или был удар
        if (attachedPlayer != null)
        {
            timeSinceAttachment += Time.deltaTime;
        }
        else if (timeSinceLastShot < noAttachmentTime)
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }


    public void Shoot(Vector2 direction)
    {
        if (attachedPlayer != null)
        {
            attachedPlayer = null;
            rb.AddForce(direction * shootPower, ForceMode2D.Impulse);
            timeSinceLastShot = 0; // Сбрасываем таймер после удара
        }
    }
}
