using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // FIX: Calculate the offset based on the player's local orientation at spawn.
        // InverseTransformPoint converts the camera's world position into a position relative to the player.
        offset = player.transform.InverseTransformPoint(transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // 1. Rotate the local offset by the player's current rotation
        Vector3 rotatedOffset = player.transform.rotation * offset;

        // 2. Apply the rotated offset to the player's position
        transform.position = player.transform.position + rotatedOffset;

        // 3. Make the camera match the player's rotation
        transform.rotation = player.transform.rotation;
    }
}