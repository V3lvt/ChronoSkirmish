using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Door : MonoBehaviour
{
    [Header("Slide Settings")]
    [Tooltip("Сколько единиц по Y подъём при открытии")]
    public float openHeight = 3f;
    [Tooltip("Скорость (в секундах) открытия/закрытия")]
    public float slideDuration = 1.5f;

    [Header("Visuals")]
    public SwitchLightVisual switchVisual;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;
    private Coroutine movementCoroutine;

    private void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.up * openHeight;

        if (switchVisual != null)
            switchVisual.SetState(false);
    }

    public void Toggle()
    {
        if (DoorManager.Instance == null)
        {
            Debug.LogError("DoorManager отсутствует в сцене.");
            return;
        }

        bool acted = DoorManager.Instance.TryToggleDoor(this);
        if (!acted)
        {
            // можно добавить фидбек типо "другая дверь уже открыта"
        }
    }

    public void Open()
    {
        if (isOpen) return;
        isOpen = true;
        if (movementCoroutine != null) StopCoroutine(movementCoroutine);
        movementCoroutine = StartCoroutine(Slide(transform.position, openPosition));
        if (switchVisual != null) switchVisual.SetState(true);
    }

    public void Close()
    {
        if (!isOpen) return;
        isOpen = false;
        if (movementCoroutine != null) StopCoroutine(movementCoroutine);
        movementCoroutine = StartCoroutine(Slide(transform.position, closedPosition));
        if (switchVisual != null) switchVisual.SetState(false);
        DoorManager.Instance.NotifyDoorClosed(this);
    }

    private IEnumerator Slide(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            float t = elapsed / slideDuration;
            transform.position = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = to;
    }
}
