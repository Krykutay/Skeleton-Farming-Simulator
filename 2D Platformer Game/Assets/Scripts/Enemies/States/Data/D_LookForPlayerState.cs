using UnityEngine;

[CreateAssetMenu(fileName = "NewLookForPlayerData", menuName = "Data/State Data/Look For Player State")]
public class D_LookForPlayerState : ScriptableObject
{
    [SerializeField] int _amountOfTurns = 2;
    [SerializeField] float _timeBetweenTurns = 0.75f;

    public int amountOfTurns { get { return _amountOfTurns; } }
    public float timeBetweenTurns { get { return _timeBetweenTurns; } }
}
