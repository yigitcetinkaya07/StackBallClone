using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    #region UI Variables
    //Level bar
    [SerializeField]
    private Image levelSlider;
    [SerializeField]
    private Image currentLevelImg;
    [SerializeField]
    private Image nextLevelImg;
    [SerializeField]
    private Text currentLevelText;
    [SerializeField]
    private Text nextLevelText;
    //Settings
    [SerializeField]
    private GameObject settingBtn;
    [SerializeField]
    private GameObject soundsBtn;
    [SerializeField]
    private GameObject soundOnBtn;
    [SerializeField]
    private GameObject soundOffBtn;
    private bool showButton = false;
    //UI parents
    [SerializeField]
    private GameObject homeUI;
    [SerializeField]
    private GameObject gameUI;
    #endregion

    #region Player Referances
    private Material playerMat;
    [SerializeField]
    private PlayerController player;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerMat = player.transform.GetChild(0).GetComponent<MeshRenderer>().material;

        levelSlider.color = playerMat.color+Color.gray;
        levelSlider.color = playerMat.color;

        currentLevelImg.color = playerMat.color;
        nextLevelImg.color = playerMat.color;

        currentLevelText.text = PlayerPrefs.GetInt("Level", 1).ToString();
        nextLevelText.text = (PlayerPrefs.GetInt("Level", 1)+1).ToString();

    }
    public void FillLevelSlider(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }
    public void ShowSettings()
    {
        showButton = !showButton;
        soundsBtn.SetActive(showButton);
    }
    public void SetSound()
    {
        SoundManager.instance.SoundOnOff();
        if (SoundManager.instance.soundOn)
        {
            soundOnBtn.SetActive(true);
            soundOffBtn.SetActive(false);
        }
        else
        {
            soundOnBtn.SetActive(false);
            soundOffBtn.SetActive(true);
        }
        
    }
    public void StartGame()
    {
        player.playerState = PlayerController.PlayerState.Playing;
        homeUI.SetActive(false);
    }
    public void RestartGame()
    {
        player.playerState = PlayerController.PlayerState.Prapare;
        homeUI.SetActive(true);
        SceneManager.LoadScene(0);

    }
    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(0);
    }
}

