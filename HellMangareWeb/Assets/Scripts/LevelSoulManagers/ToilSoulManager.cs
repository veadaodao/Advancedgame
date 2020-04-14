using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToilSoulManager : Subject
{
    public int ToilSoul = 0;
    public Observer displayToilSoul;

    private void Start()
    {
        registerObserver(displayToilSoul);
    }
    public void updateToilSoulin(int point)
    {
        ToilSoul += point;
        Notify(ToilSoul, NotificationType.ToilSoulUpdated);
    }
    public void updateToilSoulout(int point)
    {
        ToilSoul -= point;
        Notify(ToilSoul, NotificationType.ToilSoulUpdated);
    }

}
