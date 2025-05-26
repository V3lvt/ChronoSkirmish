using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class WeaponPickup : MonoBehaviour
{
    [Header("References")]
    public Transform physicsRoot;

    [Header("Pickup Settings")]
    public string handAnchorName = "gun_socket";
    public float dropForwardForce = 5f;
    public float dropUpwardForce = 2f;
    public float pickupDelay = 1f;
    public float pickupRange = 2f;

    private Collider col;
    private Rigidbody rb;
    private Transform originalParent;
    private Collider playerCol;
    private bool isHeld;
    private bool canPickUp = true;

    void Awake()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        col.isTrigger = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        originalParent = transform.parent;
    }

    void Update()
    {
        if (isHeld || !canPickUp)
        {
            return;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        bool hitThis = Physics.Raycast(ray, out RaycastHit hit, pickupRange) && hit.collider == col;

        if (hitThis && Input.GetKeyDown(KeyCode.E))
            PickUp();
    }

    void PickUp()
    {
        if (!canPickUp || isHeld) return;
        var player = FindObjectOfType<PlayerController>();
        if (player == null || player.currentWeapon != null) return;

        playerCol = player.GetComponent<Collider>();

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

        col.isTrigger = false;
        col.enabled = true;
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(
            player.transform.forward * dropForwardForce +
            Vector3.up * dropUpwardForce,
            ForceMode.VelocityChange
        );

        if (playerCol != null)
            Physics.IgnoreCollision(col, playerCol, true);

        GetComponent<Gun>().enabled = false;
        Camera.main.GetComponent<CameraScript>().heldWeapon = null;

        canPickUp = false;
        StartCoroutine(ReenableCollider());
    }

    private IEnumerator ReenableCollider()
    {
        yield return new WaitForSeconds(pickupDelay);
        col.isTrigger = false;
        canPickUp = true;
        if (playerCol != null)
            Physics.IgnoreCollision(col, playerCol, false);
    }
}