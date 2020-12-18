using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventManager : MonoBehaviour
{
    public float minionCount;

    //delegate and event fucntions
    public delegate void playerAction();
    public static event playerAction levelUp;

    // Start is called before the first frame update
    void Start()
    {
        minionCount = 4;
    }

    //when object is enabled...
    //assigns countUpdate function to the minionDied event from Minion
    private void OnEnable()
    {
        Minion.minionDied += countUpdate;
    }

    //when object is disabled...
    //removes countUpdate function from the minionDied event from Minion
    private void OnDisable()
    {
        Minion.minionDied -= countUpdate;
    }

    //declares levelUp event when conditions are met
    void countUpdate()
    {
        minionCount -= 1;
        if (minionCount == 1)
        {
            levelUp();
        }
        else if (minionCount == 0)
        {
            levelUp();
        }
    }
}
