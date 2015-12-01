using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static int roundNumber = 0;

    public static int roundPointsP1 = 0;
    public static int roundPointsP2 = 0;

    public GameObject roundPopup;

    public void StartNextRound()
    {
        Instantiate(roundPopup); 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartNextRound();
    }
}
