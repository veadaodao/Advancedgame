using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoulData
{

    public List<string> SoulName = new List<string>();
    public List<string> SoulReason = new List<string>();
    public List<int> SoulAge = new List<int>();
    public List<string> SoulStory = new List<string>();
    public List<float> MemoryNumber = new List<float>();
    public List<float> SpiritNumber = new List<float>();
    public List<int> FatigueNumber = new List<int>();

    public List<float> PositionX = new List<float>();
    public List<float> PositionY = new List<float>();
    public List<float> PositionZ = new List<float>();
    
    public List<bool> ChooseLevel = new List<bool>();
    public List<bool> TransLevel = new List<bool>();

    public List<bool> ifOnLivingLevel = new List<bool>();
    public List<bool> ifOnFireLevel = new List<bool>();
    public List<bool> ifOnFrozenLevel = new List<bool>();
    public List<bool> ifOnVisitingLevel = new List<bool>();
    public List<bool> ifOnSolitudeLevel = new List<bool>();
    public List<bool> ifOnToilLevel = new List<bool>();
    public List<bool> ifOnAvariceLevel = new List<bool>();
    public List<bool> ifOnRebirthLevel = new List<bool>();
    public List<bool> ifOnDeathLevel = new List<bool>();

    public List<bool> Convicted = new List<bool>();
    public List<int> PretenseYear = new List<int>();
    public List<int> SlothYear = new List<int>();
    public List<int> StealYear = new List<int>();
    public List<int> MurderYear = new List<int>();
    public List<int> BetrayelYear = new List<int>();
    public List<int> GreedYear = new List<int>();
    public List<int> LustYear = new List<int>();
    public List<int> AllYear = new List<int>();

    public int HellHeat;
    public int HellCool;
    public int HellSoul;
    public int HellYear;

    public int HellCoal;
    public int HellCoin;
    public int HellIce;
    public int HellLight;
    public int HellWater;

}
