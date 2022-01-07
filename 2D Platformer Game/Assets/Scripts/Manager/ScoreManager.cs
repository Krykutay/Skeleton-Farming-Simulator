using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int tokenScore { get; private set; }
    public int orbScore { get; private set; }

    [SerializeField] TMP_Text _tokenText;
    [SerializeField] TMP_Text _orbText;

    const string TOKEN_COUNT = "tokenCount";

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        orbScore = 0;
        _orbText.SetText("x " + orbScore);

        Load_Token();
    }

    public void Orb_Collected()
    {
        orbScore++;
        _orbText.SetText("x " + orbScore);
    }
    
    public void Token_Earned(int earnedAmount)
    {
        tokenScore += earnedAmount;
        _tokenText.SetText("x " + tokenScore);
        PlayerPrefs.SetInt(TOKEN_COUNT, tokenScore);
    }

    public void Token_Spent(int spentAmount)
    {
        tokenScore -= spentAmount;
        _tokenText.SetText("x " + tokenScore);
        PlayerPrefs.SetInt(TOKEN_COUNT, tokenScore);
    }

    void Load_Token()
    {
        if (PlayerPrefs.HasKey(TOKEN_COUNT))
            tokenScore = PlayerPrefs.GetInt(TOKEN_COUNT);
        else
            tokenScore = 0;

        _tokenText.SetText("x " + tokenScore);
    }

}
