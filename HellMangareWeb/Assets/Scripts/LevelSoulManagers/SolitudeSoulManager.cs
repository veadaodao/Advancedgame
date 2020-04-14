using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolitudeSoulManager : Subject
{
    public int SolitudeSoul = 0;
    public Observer displaySolitudeSoul;

    private void Start()
    {
        registerObserver(displaySolitudeSoul);
    }
    public void updateSolitudeSoulin(int point)
    {
        SolitudeSoul += point;
        Notify(SolitudeSoul, NotificationType.SolitudeSoulUpdated);
    }
    public void updateSolitudeSoulout(int point)
    {
        SolitudeSoul -= point;
        Notify(SolitudeSoul, NotificationType.SolitudeSoulUpdated);
    }

}

