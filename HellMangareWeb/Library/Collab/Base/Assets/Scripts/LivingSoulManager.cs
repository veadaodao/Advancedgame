using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingSoulManager : Subject
{
    public int LivingSoul = 0;
    public Observer displayLivingSoul;

    private void Start()
    {
        registerObserver(displayLivingSoul);
    }
    public void updateLivingSoulin(int point)
    {     
        LivingSoul += point;
        Notify(LivingSoul, NotificationType.LivingSoulUpdated);
        Debug.Log("1");
    }
    public void updateLivingSoulout(int point)
    {
        LivingSoul -= point;
        Notify(LivingSoul, NotificationType.LivingSoulUpdated);
    }

}
