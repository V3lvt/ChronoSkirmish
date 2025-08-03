using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance { get; private set; }

    private Door currentlyOpenDoor;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool TryToggleDoor(Door door)
    {
        if (currentlyOpenDoor == null)
        {
            OpenDoor(door);
            return true;
        }

        if (currentlyOpenDoor == door)
        {
            CloseDoor(door);
            return true;
        }

        return false;
    }

    private void OpenDoor(Door door)
    {
        currentlyOpenDoor = door;
        door.Open();
    }

    private void CloseDoor(Door door)
    {
        door.Close();
        if (currentlyOpenDoor == door)
            currentlyOpenDoor = null;
    }

    public void NotifyDoorClosed(Door door)
    {
        if (currentlyOpenDoor == door)
            currentlyOpenDoor = null;
    }
}
