using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreCalculationOnGameOver : MonoBehaviour
{
    /*
    [SerializeField] TMP_Text _coinScoreText;
    [SerializeField] TMP_Text _enemyScoreText;
    [SerializeField] TMP_Text _handScoreText;

    [SerializeField] TMP_Text _coinScorePointsText;
    [SerializeField] TMP_Text _enemyScorePointsText;
    [SerializeField] TMP_Text _handScorePointsText;

    [SerializeField] TMP_Text _totalScoreText;
    [SerializeField] TMP_Text _highScoreText;

    [SerializeField] Animator _gameoverAnimator;

    ScoreManager _scoreManagerInstance;

    int _coinScore;
    int _enemyScore;
    int _handScore;
    int _totalScore;
    int _highScore;

    void OnEnable()
    {
        _scoreManagerInstance = ScoreManager.GetInstance();

        _coinScore = _scoreManagerInstance.GetCoinScore();
        _enemyScore = _scoreManagerInstance.GetEnemyScore();
        _handScore = _scoreManagerInstance.GetHandScore();

        _coinScoreText.text = "x " + _coinScore;
        _enemyScoreText.text = "x " + _enemyScore;
        _handScoreText.text = "x " + _handScore;

        if (PlayerPrefs.HasKey("highScore"))
            _highScore = PlayerPrefs.GetInt("highScore");
        else
            _highScore = 0;

            _gameoverAnimator.SetTrigger("PlayerDied");
    }

    public void GameoverPart1Done()
    {
        CalculateScore();
    }

    void CalculateScore()
    {
        _totalScore = (_coinScore * 100) + (_enemyScore * 50) + (_handScore * 30);
        _totalScoreText.text = _totalScore.ToString();

        int tk = _coinScore + _enemyScore + _handScore;
        float waittime = 0.05f;

        if (tk < 60)
            waittime = 0.05f;
        else if (tk < 120)
            waittime = 0.035f;
        else if (tk < 180)
            waittime = 0.02f;
        else
            waittime = 0.012f;

        StartCoroutine(Countdown(waittime));
    }

    IEnumerator Countdown(float waittime)
    {
        for (int i = 1; i < _coinScore + 1; i++)
        {
            yield return new WaitForSecondsRealtime(waittime);

            _coinScorePointsText.text = (i * 100).ToString();
        }
        SoundManager.Instance.Play(SoundManager.SoundTags.ScoreCalculation);

        for (int i = 1; i < _enemyScore + 1; i++)
        {
            yield return new WaitForSecondsRealtime(waittime);
            
            _enemyScorePointsText.text = (i * 50).ToString();
        }
        SoundManager.Instance.Play(SoundManager.SoundTags.ScoreCalculation);

        for (int i = 1; i < _handScore + 1; i++)
        {
            yield return new WaitForSecondsRealtime(waittime);

            _handScorePointsText.text = (i * 25).ToString();
        }
        SoundManager.Instance.Play(SoundManager.SoundTags.ScoreCalculation);

        if (_totalScore > _highScore)
        {
            _highScoreText.text = _totalScore.ToString();
            PlayerPrefs.SetInt("highScore", _totalScore);
        }
        else
            _highScoreText.text = _highScore.ToString();

        yield return new WaitForSecondsRealtime(0.4f);
        _gameoverAnimator.SetTrigger("CalculationsDone");
    }
    */
}
