using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _coinText;
    [SerializeField] TextMeshProUGUI _enemyText;
    [SerializeField] TextMeshProUGUI _handText;

    int _coinScore = 0;
    int _enemyScore = 0;
    int _handscore = 0;

    static ScoreManager _instance;

    public static ScoreManager GetInstance()
    {
        return _instance;
    }

    void Awake()
    {
        _instance = this;
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    void Coin_Collected()
    {
        _coinScore++;
        _coinText.SetText("x " + _coinScore);
    }

    void Enemy_Died()
    {
        _enemyScore++;
        _enemyText.SetText("x " + _enemyScore);
    }

    void HandObstacle_Died()
    {
        _handscore++;
        _handText.SetText("x " + _handscore);
    }

    public int GetCoinScore()
    {
        return _coinScore;
    }

    public int GetEnemyScore()
    {
        return _enemyScore;
    }

    public int GetHandScore()
    {
        return _handscore;
    }

}
