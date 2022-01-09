using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreCalculationOnGameOver : MonoBehaviour
{
    [SerializeField] TMP_Text _enemyScoreText;
    [SerializeField] TMP_Text _orbsScoreText;
    [SerializeField] TMP_Text _tokensEarnedText;

    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _highScoreText;

    Animator _anim;

    int _tokensEarned;
    bool _isEarned;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        _isEarned = false;
        _tokensEarned = (int)Mathf.Ceil(ScoreManager.Instance.orbScore / 5);

        int highScore;
        int score = ScoreManager.Instance.orbScore * 100;

        _enemyScoreText.text = "x " + ScoreManager.Instance.enemyKillCount.ToString();
        _orbsScoreText.text = "x " + ScoreManager.Instance.orbScore.ToString();
        _tokensEarnedText.text = "x 0";

        _scoreText.text = score.ToString();

        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetInt("highScore");
            if (highScore < score)
            {
                highScore = score;
                PlayerPrefs.SetInt("highScore", highScore);
            }
            _highScoreText.text = highScore.ToString();
        }
        else
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
            _highScoreText.text = highScore.ToString();
        }
    }

    void OnDisable()
    {
        if (!_isEarned)
            ScoreManager.Instance.Token_Earned(_tokensEarned);
    }

    void ScoreCalculationPart1Done()
    {
        StartCoroutine(EarnToken());
    }

    IEnumerator EarnToken()
    {
        for (int i = 1; i < _tokensEarned + 1; i++)
        {
            yield return new WaitForSecondsRealtime(0.05f);

            _tokensEarnedText.text = "x " + i.ToString();
        }
        SoundManager.Instance.Play(SoundManager.SoundTags.ScoreCalculation);
        _anim.SetTrigger("part1Complete");

        _isEarned = true;
        ScoreManager.Instance.Token_Earned(_tokensEarned);
    }
}
