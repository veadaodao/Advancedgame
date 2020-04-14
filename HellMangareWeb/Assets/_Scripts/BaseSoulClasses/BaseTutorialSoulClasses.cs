using System.Collections;
using UnityEngine;

public class BaseTutorialSoulClasses : MonoBehaviour
{

    public string SoulName;
    public string SoulReason;
    public int SoulAge;
    public string SoulStory;

    public float MemoryNumber;
    public float SpiritNumber;
    public int FatigueNumber;

    public int PretenseYear;

    public string[] Name = new string[] { "John",};
    public string[] Reason = new string[] { "Road injuries",  };
    public string[] Story = new string[]
    {
    "    The soul invested much of his savings in the stock market. Unfortunately, he lost this money due to falling stock prices. He lied and borrowed lots of money to make up for the loss. He became depressed and began to drink heavily"
    };
    private void Start()
    {
        SoulAge = Random.Range(18, 90);

        MemoryNumber = Random.Range(4, 10);
        SpiritNumber = Random.Range(2, 10);
        FatigueNumber = Random.Range(4, 10);

        SoulName = Name[Random.Range(0, Name.Length)];
        SoulReason = Reason[Random.Range(0, Reason.Length)];
        SoulStory = Story[Random.Range(0, Story.Length)];


    }
}