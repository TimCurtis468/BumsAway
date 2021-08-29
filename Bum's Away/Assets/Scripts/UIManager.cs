using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ScoreText;
    public Text LivesText;

    public Sprite[] backgrounds;

    private SpriteRenderer spriteRenderer;

    public GameObject background_obj;

    public int Score { get; set; }

    private void Start()
    {
        GameManager.OnLifeGained += OnLifeGained;
//        GameManager.OnLifeLost += OnLifeLost;
        // add OnTarget Hit subscriptions and call back functions

        UpdateScoreText(0);
    }

    private void Awake()
    {
        spriteRenderer = background_obj.GetComponent<SpriteRenderer>();

        OnLifeLost(GameManager.Instance.AvailableLives);
    }


    private void OnLifeLost(int remainingLives)
    {

        string txt = "LIVES: " + remainingLives.ToString();
        LivesText.text = txt;


    }

    private void OnLevelComplete()
    {
        if (backgrounds.Length > 0)
        {
            int background_num = UnityEngine.Random.Range(0, backgrounds.Length);
            spriteRenderer.sprite = backgrounds[background_num];
        }
    }

    private void OnLifeGained(int remainingLives)
    {
        string txt = "LIVES: " + remainingLives.ToString();
        LivesText.text = txt;
    }

    private void UpdateScoreText(int increment)
    {
        this.Score += increment;
        string scoreString = this.Score.ToString().PadLeft(5, '0');
        ScoreText.text = "SCORE: " + scoreString;
        GameManager.Instance.SetScore(Score);
    }




    private void OnDisable()
    {
//        GameManager.OnLifeLost -= OnLifeLost;
        GameManager.OnLifeGained -= OnLifeGained;

    }

}
