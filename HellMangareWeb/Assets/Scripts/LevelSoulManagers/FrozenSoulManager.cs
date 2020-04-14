using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenSoulManager : Subject
{
    public int FrozenSoul = 0;
    public Observer displayFrozenSoul;

    private void Start()
    {
        registerObserver(displayFrozenSoul);
    }
    public void updateFrozenSoulin(int point)
    {
        FrozenSoul += point;
        Notify(FrozenSoul, NotificationType.FrozenSoulUpdated);
    }
    public void updateFrozenSoulout(int point)
    {
        FrozenSoul -= point;
        Notify(FrozenSoul, NotificationType.FrozenSoulUpdated);
    }

}

