using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform target; // Target object for the camera to follow
    [SerializeField] private float speed = 0.125f;
    [SerializeField] private float distance = 5f; // Initial distance from the camera to the target

    private void LateUpdate()
    {
        // Calculate the new position based on the target's position and the offset
        Vector3 newPosition = target.position - target.forward * distance;

        // Smoothly move the camera towards the new position
       //transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);

        // Look at the target from the new position
        //transform.LookAt(target.position);
    }
}
