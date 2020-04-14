using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSoulManager : Subject
{
    public int DeathSoul = 0;
    public Observer displayDeathSoul;

    private void Start()
    {
        registerObserver(displayDeathSoul);
    }
    public void updateDeathSoulin(int point)
    {
        DeathSoul += point;
        Notify(DeathSoul, NotificationType.DeathSoulUpdated);

    }
    public void updateDeathSoulout(int point)
    {
        DeathSoul -= point;
        Notify(DeathSoul, NotificationType.DeathSoulUpdated);
    }

}