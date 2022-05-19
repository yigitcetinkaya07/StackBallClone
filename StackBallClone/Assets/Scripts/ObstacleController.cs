using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [field:SerializeField]
    private Obstacle[] obstacles = null;

    public void ShatterAllObstacles()
    {
        if (transform.parent!=null)
        {
            transform.parent = null;           
        }
        foreach (Obstacle item in obstacles)
        {
            item.Shatter();
        }
        StartCoroutine(RemoveAllShatteredObj());
    }

    private void Awake()
    {
        obstacles = gameObject.GetComponentsInChildren<Obstacle>();
        //Debug.Log(obstacles.Length);
    }

    IEnumerator RemoveAllShatteredObj()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
