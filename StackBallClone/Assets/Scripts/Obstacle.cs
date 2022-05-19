using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody enemyRb;
    private MeshRenderer meshRenderer;
    private Collider enemyCollider;
    private ObstacleController obstacleController;
    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        enemyCollider = GetComponent<Collider>();
        obstacleController = transform.parent.GetComponent<ObstacleController>();
    }
    public void Shatter()
    {
        enemyRb.isKinematic = false;
        //We turned off collision after fragmentation
        enemyCollider.enabled = false;
        //We took the center position
        Vector3 forcePoint = transform.parent.position;
        float parentXPos = transform.parent.position.x;
        //Midpoint of the child object in the mesh renderer
        float XPos = meshRenderer.bounds.center.x;
        //The direction it will go after fragmentation is outward from the center
        Vector3 subDir = (parentXPos - XPos < 0)? Vector3.right : Vector3.left;
        Vector3 dir = (Vector3.up * 1.5f+subDir).normalized;

        float force = Random.Range(20, 35);
        float torque = Random.Range(100, 180);

        enemyRb.AddForceAtPosition(dir * force, forcePoint,ForceMode.Impulse);
        enemyRb.AddTorque(Vector3.left * torque);

        enemyRb.velocity = Vector3.down;

    }
}
