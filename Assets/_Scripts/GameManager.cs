using UnityEngine;

public class GameManager : MonoBehaviour
{


    #region Rounds
    Round _currentRound;
    public void SetActiveRound(Round round) => _currentRound = round;
    public Round GetActiveRound() => _currentRound;

    void VerifySuccessRound()
    {
        // logique
    }

    #endregion

}
