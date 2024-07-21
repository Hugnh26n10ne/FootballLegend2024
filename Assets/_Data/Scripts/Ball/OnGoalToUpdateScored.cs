using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGoalToUpdateScored : MonoBehaviour
{
    [SerializeField] TeamManager homeManager;
    [SerializeField] TeamManager guestManager;
    [SerializeField] MatchManager matchManager;

    [SerializeField] bool isApplyGoal = true;

    private void Awake()
    {
        isApplyGoal = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("HomeGoal") && isApplyGoal)
            {
                //Debug.Log("Ghi bàn");
                homeManager.UpdateScore(1);


                //homeScored = homeScored + 1;
                //_homeScoredText.text = homeScored.ToString();

            }
            if (other.gameObject.CompareTag("GuestGoal") && isApplyGoal)
            {
                //Debug.Log("Ghi bàn");
                guestManager.UpdateScore(1);

                //guestScored = guestScored + 1;
                //_guestScoredText.text = guestScored.ToString();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("HomeGoal") && isApplyGoal)
            {
                isApplyGoal = false;
                matchManager.OnGoalScored();
            }
            if (other.gameObject.CompareTag("GuestGoal") && isApplyGoal)
            {
                isApplyGoal = false;
                matchManager.OnGoalScored();
            }
        }

    }

    public void SetStatusApplyGoal(bool status)
    {
        isApplyGoal = status;
    }
}
