using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class WeaponPickup : MonoBehaviour
{
    [Header("Settings")]
    public string handAnchorName = "gun_socket";
    public float dropForwardForce = 5f;
    public float dropUpwardForce = 2f;
    public float pickupDelay = 1f;

    private Collider col;
    private Rigidbody rb;
    private Transform originalParent;
    private bool isHeld;
    private bool canPickUp = true;

    void Awake()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        col.isTrigger = true;
        rb.isKinematic = true;
        rb.useGravity = false;

        originalParent = transform.parent;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!canPickUp || isHeld) return;
        var player = other.GetComponentInParent<PlayerController>();
        if (player != null && player.currentWeapon == null)
            PickUp(player);
    }

    void PickUp(PlayerController player)
    {
        var hand = player.transform.Find(handAnchorName);
        if (hand == null) return;
        transform.SetParent(hand, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        col.enabled = false;
        rb.isKinematic = true;
        rb.useGravity = false;

        GetComponent<Gun>().enabled = true;
        Camera.main.GetComponent<CameraScript>().heldWeapon = transform;

        isHeld = true;
        player.currentWeapon = this;
    }

    public void Drop()
    {
        if (!isHeld) return;
        var player = GetComponentInParent<PlayerController>();
        if (player.currentWeapon == this)
            player.currentWeapon = null;

        isHeld = false;
        transform.SetParent(originalParent, true);

        transform.position = player.transform.position
                           + player.transform.forward * 0.5f
                           + Vector3.up * 0.5f;

        col.enabled = true;
        col.isTrigger = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(
            player.transform.forward * dropForwardForce +
            Vector3.up * dropUpwardForce,
            ForceMode.VelocityChange
        );

        GetComponent<Gun>().enabled = false;
        Camera.main.GetComponent<CameraScript>().heldWeapon = null;

        canPickUp = false;
        StartCoroutine(Reenable());
    }

    private IEnumerator Reenable()
    {
        yield return new WaitForSeconds(pickupDelay);
        col.isTrigger = true;
        canPickUp = true;
    }
}
