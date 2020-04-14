using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitingSoulManager : Subject
{
    public int VisitingSoul = 0;
    public Observer displayVisitingSoul;

    private void Start()
    {
        registerObserver(displayVisitingSoul);
    }
    public void updateVisitingSoulin(int point)
    {
        VisitingSoul += point;
        Notify(VisitingSoul, NotificationType.VisitingSoulUpdated);
    }
    public void updateVisitingSoulout(int point)
    {
        VisitingSoul -= point;
        Notify(VisitingSoul, NotificationType.VisitingSoulUpdated);
    }

}

