using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebirthSoulManager : Subject
{
    public int RebirthSoul = 0;
    public Observer displayRebirthSoul;

    private void Start()
    {
        registerObserver(displayRebirthSoul);
    }
    public void updateRebirthSoulin(int point)
    {
        RebirthSoul += point;
        Notify(RebirthSoul, NotificationType.RebirthSoulUpdated);

    }
    public void updateRebirthSoulout(int point)
    {
        RebirthSoul -= point;
        Notify(RebirthSoul, NotificationType.RebirthSoulUpdated);
    }

}