using UnityEngine;

public class CameraHeadBob : MonoBehaviour
{
    public float amplitude = 0.04f;      // Высота покачивания
    public float frequency = 7f;         // Частота покачивания

    [Header("References")]
    public PlayerController playerController;

    private Vector3 startPos;
    private float bobTimer = 0f;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        if (playerController == null)
            return;

        // Получаем input
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");
        bool isMoving = (Mathf.Abs(xInput) > 0.1f || Mathf.Abs(zInput) > 0.1f) && playerController.isGrounded;

        if (isMoving)
        {
            bobTimer += Time.deltaTime * frequency;
            float yOffset = Mathf.Sin(bobTimer) * amplitude;
            float xOffset = Mathf.Sin(bobTimer * 2f) * amplitude * 0.5f;
            transform.localPosition = startPos + new Vector3(xOffset, yOffset, 0);
        }
        else
        {
            // Плавно возвращаем камеру в стартовую позицию
            bobTimer = 0;
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime * 8f);
        }
    }
}

