using UnityEngine;

public class TimeZone : MonoBehaviour
{
    [Header("Time Zone Settings")]
    public float ageRate = 0f;

    private void OnTriggerEnter(Collider other)
    {
        var character = other.GetComponent<Character>();
        if (character != null)
            character.SetAgeRate(ageRate);
    }

    private void OnTriggerExit(Collider other)
    {
        var character = other.GetComponent<Character>();
        if (character != null)
            character.SetAgeRate(character.defaultAgeRate);
    }
}