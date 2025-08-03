using UnityEngine;

public class Switch : MonoBehaviour
{
    [Tooltip("Дверь, которую этот переключатель управляет")]
    public Door linkedDoor;

    public void Activate()
    {
        if (linkedDoor == null)
        {
            Debug.LogWarning("Switch: linkedDoor не задан.");
            return;
        }

        linkedDoor.Toggle();

    }
}
