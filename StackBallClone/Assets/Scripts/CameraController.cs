using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 cameraPos;
    private Transform player, win;

    [SerializeField]
    private float offset=4f;
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }
    void Update()
    {
        if (win==null)
        {
            win = GameObject.Find("win(Clone)").GetComponent<Transform>();
        }
        if (transform.position.y>player.position.y&&transform.position.y>win.position.y+offset)
        {
            cameraPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, cameraPos.y, -5);//-5 camera z pos in editor
        }
    }
}
