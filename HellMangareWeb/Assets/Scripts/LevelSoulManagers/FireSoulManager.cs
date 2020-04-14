using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireSoulManager : Subject
{
    public int FireSoul = 0;
    public Observer displayFireSoul;

    private void Start()
    {
        registerObserver(displayFireSoul);
    }
    public void updateFireSoulin(int point)
    {
        FireSoul += point;
        Notify(FireSoul, NotificationType.FireSoulUpdated);
    }
    public void updateFireSoulout(int point)
    {
        FireSoul -= point;
        Notify(FireSoul, NotificationType.FireSoulUpdated);
    }

}
