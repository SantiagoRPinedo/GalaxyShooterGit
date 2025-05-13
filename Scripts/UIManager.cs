using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] LivesImages;
    public Image livesImage;
    public Text scoreText;
    public int Score;
    public GameObject titlescreen;

    public void UpdateLives(int currentLives){
        livesImage.sprite = LivesImages[currentLives];
    }

    public void UpdateScore(){
        Score+= 5; // Aumentar el puntaje en 5
        scoreText.text = "Score: " + Score;
    }

    public void ShowTitleScreen(){
        titlescreen.SetActive(true);
        Score = 0;
        scoreText.text = "Score: " + Score;
    }

    public void HideTitleScreen(){ // Esconde la pantalla de titulo
        titlescreen.SetActive(false);
        Score = 0;
    }

}
