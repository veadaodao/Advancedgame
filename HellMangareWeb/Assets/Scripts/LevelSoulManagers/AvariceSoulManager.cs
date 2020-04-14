using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvariceSoulManager : Subject
{
    public int AvariceSoul = 0;
    public Observer displayAvariceSoul;

    private void Start()
    {
        registerObserver(displayAvariceSoul);
    }
    public void updateAvariceSoulin(int point)
    {
        AvariceSoul += point;
        Notify(AvariceSoul, NotificationType.AvariceSoulUpdated);
    }
    public void updateAvariceSoulout(int point)
    {
        AvariceSoul -= point;
        Notify(AvariceSoul, NotificationType.AvariceSoulUpdated);
    }

}
