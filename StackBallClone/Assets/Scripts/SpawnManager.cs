using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    #region Obstacles
    [SerializeField]
    private GameObject[] obstacleModels;
    //Each prefab have 4 instance(circle,flower etc.)
    private GameObject[] obstaclePrefabs = new GameObject[4];
    [SerializeField]
    private GameObject winPrefab;

    private GameObject tempObstacle1, tempObstacle2;
    private int level = 1, addNumber = 7;
    private float obstacleNumber;
    #endregion


    #region Player
    [SerializeField]
    private Material plateMat, baseMat;
    [SerializeField]
    private MeshRenderer playerMeshRenderer;
    #endregion

    void Start()
    {
        level=PlayerPrefs.GetInt("Level", 1);
        GenerataRandomObstacle();
        PlacingObstacles();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            plateMat.color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
            baseMat.color = plateMat.color + Color.gray;
            playerMeshRenderer.material.color = baseMat.color;
        }
    }
   
    private void PlacingObstacles()
    {
        float randomNumber = Random.value;
        for (obstacleNumber = 0; obstacleNumber > -level - addNumber; obstacleNumber -= 0.5f)
        {
            if (level <= 20)
            {
                tempObstacle1 = Instantiate(obstaclePrefabs[Random.Range(0, 2)]);
            }
            else if (level > 20 && level <= 50)
            {
                tempObstacle1 = Instantiate(obstaclePrefabs[Random.Range(1, 3)]);
            }
            else if (level > 50 && level <= 100)
            {
                tempObstacle1 = Instantiate(obstaclePrefabs[Random.Range(2, 4)]);
            }
            else if (level > 100)
            {
                tempObstacle1 = Instantiate(obstaclePrefabs[Random.Range(3, 4)]);
            }

            tempObstacle1.transform.position = new Vector3(0, obstacleNumber - 0.01f, 0);
            tempObstacle1.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);
            //Changing the orientation of added objects within themselves(45,90,180)
            if (Mathf.Abs(obstacleNumber) >= level * 0.3f && Mathf.Abs(obstacleNumber) <= level * 0.6f)
            {
                tempObstacle1.transform.eulerAngles += Vector3.up * 180;
            }
            else if (Mathf.Abs(obstacleNumber) >= level * 0.8f)
            {

                if (randomNumber > .75f)
                {
                    tempObstacle1.transform.eulerAngles += Vector3.up * 90;
                }
                else
                {
                    tempObstacle1.transform.eulerAngles += Vector3.up * 270;
                }

            }
            //Continuous rotation of inserted objects
            tempObstacle1.transform.parent = FindObjectOfType<RotateManager>().transform;
        }
        tempObstacle2 = Instantiate(winPrefab);
        tempObstacle2.transform.position = new Vector3(0, obstacleNumber - 0.01f, 0);
    }
    private void GenerataRandomObstacle()
    {
        //4  prefab 
        int random = Random.Range(0, 5);
        //Each prefab have 4 instance(circle,flower etc.) the number may increase but must always be equal
        int prefabCount = obstaclePrefabs.Length;
        switch (random)
        {
            case 0:
                for (int i = 0; i < prefabCount; i++)
                {
                    obstaclePrefabs[i] = obstacleModels[i];
                }
                break;
            case 1:
                for (int i = 0; i < prefabCount; i++)
                {
                    obstaclePrefabs[i] = obstacleModels[i + prefabCount];
                }
                break;
            case 2:
                for (int i = 0; i < prefabCount; i++)
                {
                    obstaclePrefabs[i] = obstacleModels[i + prefabCount * 2];
                }
                break;
            case 3:
                for (int i = 0; i < prefabCount; i++)
                {
                    obstaclePrefabs[i] = obstacleModels[i + prefabCount * 3];
                }
                break;
            case 4:
                for (int i = 0; i < prefabCount; i++)
                {
                    obstaclePrefabs[i] = obstacleModels[i + prefabCount * 4];
                }
                break;
            default:
                break;
        }
    }
   
}
