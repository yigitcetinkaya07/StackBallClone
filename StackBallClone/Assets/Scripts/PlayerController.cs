using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Player variables
    private Rigidbody playerRb;

    private bool smash;
    private bool invictable;
    private float currentTime;

    [field: SerializeField]
    private GameObject fireReinforcement;
    public enum PlayerState
    {
        Prapare,
        Playing,
        Died,
        Finish
    }
    [HideInInspector]
    public PlayerState playerState = PlayerState.Prapare;
    #endregion

    #region Sound
    [SerializeField]
    private AudioClip win, death, invicDestroy, destroy, bounce;
    #endregion


    #region Obstacle Numbers
    private int destroyedObstacleNumber;
    private int totalObstaclenumber;
    #endregion

    #region UI
    [SerializeField]
    private Image invitableSlider;
    [SerializeField]
    private GameObject invictableObject;

    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject finishGameUI;
    [SerializeField]
    private GameUI gameUI;
    #endregion

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        destroyedObstacleNumber = 0;
    }
    void Start()
    {
        //Carefull s word objects not object
        totalObstaclenumber = FindObjectsOfType<ObstacleController>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                smash = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                smash = false;
            }

            if (invictable)
            {
                currentTime -= Time.deltaTime * 0.35f;
                if (!fireReinforcement.activeInHierarchy)
                {
                    fireReinforcement.SetActive(true);
                }
            }
            else
            {
                if (fireReinforcement.activeInHierarchy)
                {
                    fireReinforcement.SetActive(false);
                }
                if (smash)
                {
                    currentTime += Time.deltaTime * 0.8f;
                }
                else
                {
                    currentTime -= Time.deltaTime * 0.5f;
                }
            }

            if (currentTime >= 0.15f || invitableSlider.color == Color.red)
            {
                invictableObject.SetActive(true);
            }
            else
            {
                invictableObject.SetActive(false);
            }

            if (currentTime >= 1)
            {
                currentTime = 1;
                invictable = true;
                invitableSlider.color = Color.red;
            }
            else if (currentTime <= 0)
            {
                currentTime = 0;
                invictable = false;
                invitableSlider.color = Color.white;
            }

            if (invictableObject.activeInHierarchy)
            {
                invitableSlider.fillAmount = currentTime;
            }
        }
        else if (playerState == PlayerState.Prapare)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameUI.StartGame();
            }
        }
        else if (playerState == PlayerState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                finishGameUI.SetActive(false);
                ScoreManager.instance.ResetScore();
                gameUI.NextLevel();
            }
        }
        else if (playerState == PlayerState.Died)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameOverUI.SetActive(false);
                playerRb.isKinematic = false;
                ScoreManager.instance.ResetScore();
                gameUI.RestartGame();
                
            }

        }

    }
    private void FixedUpdate()
    {
        if (playerState == PlayerState.Playing)
        {
            if (smash)
            {
                playerRb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }
        }

    }
    private void GetPoint()
    {
        if (invictable)
        {
            ScoreManager.instance.AddScore(10);
        }
        else
        {
            ScoreManager.instance.AddScore(20);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!smash)
        {
            playerRb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
        else
        {
            if (invictable)
            {
                if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "plane")
                {
                    collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                    GetPoint();
                    SoundManager.instance.PlaySoundFX(invicDestroy, 0.5f);
                    destroyedObstacleNumber++;
                }
            }
            else
            {
                if (collision.gameObject.tag == "enemy")
                {
                    collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                    GetPoint();
                    SoundManager.instance.PlaySoundFX(destroy, 0.5f);
                    destroyedObstacleNumber++;
                }
                else if (collision.gameObject.tag == "plane")
                {
                    gameOverUI.SetActive(true);
                    ScoreManager.instance.GameOver();
                    playerState = PlayerState.Died;
                    playerRb.isKinematic = true;
                    SoundManager.instance.PlaySoundFX(death, 0.5f);
                }
            }

        }
        gameUI.FillLevelSlider(destroyedObstacleNumber / (float)totalObstaclenumber);

        if (collision.gameObject.tag == "Finish" && playerState == PlayerState.Playing)
        {
            playerState = PlayerState.Finish;
            SoundManager.instance.PlaySoundFX(win, 0.5f);
            finishGameUI.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Level " + PlayerPrefs.GetInt("Level",1);
            finishGameUI.SetActive(true);
        }
    }
    //We prevented the ball from staying on the surface
    private void OnCollisionStay(Collision collision)
    {
        if (!smash || collision.gameObject.tag == "Finish")
        {
            playerRb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            SoundManager.instance.PlaySoundFX(bounce, 0.5f);
        }
    }
}
