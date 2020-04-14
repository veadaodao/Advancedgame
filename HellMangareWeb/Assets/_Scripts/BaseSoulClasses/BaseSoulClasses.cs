using System.Collections;
using UnityEngine;

public class BaseSoulClasses : MonoBehaviour
{

    public string SoulName;
    public string SoulReason;
    public int SoulAge;
    public string SoulStory;

    public float MemoryNumber;
    public float SpiritNumber;
    public int FatigueNumber;

    public int PretenseYear;

    public string[] Name=new string[] { "John", "Lucas", "Mason", "Logan", "Ethan", "Jacob", "Daniel", "Henry", "Alex", "Yang", "Harumi", "Adi", "Neo" };
    public string[] Reason= new string[] { "Cardiovascular diseases", "Cancers", "Respiratory diseases", "Dementia", "Road injuries", "Suicide", "Homicide Drowning", "Alcohol use disorders", "Drug use disorders", "Conflict", "Fire", "Poisonings", "Terrorism", "Natural disasters" };
    public string[] Story = new string[] 
    {"   The soul was a soldier and killed some people in the war following his command's order. After the war, he always felt guilty about having killed people although he was always respected by others.",
    "    The soul and his family went to the zoo. He broke the rules and got off the bus during the tour. A tiger dragged him away. His mother got out to chase him and was mauled to death by the tiger. He lived with regret until he died.",
    "    The soul was a teacher in a middle school. He was teaching in class when the earthquake happened. He ran out of the building first, but most of the students in his class died in the incident.",
    "    The soul was a soldier. In order to leave the army, he deliberately exposed himself in a battle, his arm was disabled by an enemy shot. So he returned to his hometown.",
    "    The soul was a doctor. He saved many lives during his life. One day, he needed to treat lots of patients in an unexpected disaster. However, he had to choose between those who were seriously ill, but to treat the patients who were more likely be survive. He was depressed because he thought he killed them.",
    "    The soul wife developed mental illness. He took good care of her for more than ten years. However, as his wife’s condition worsened, he even concerned to murder his wife. Finally, he left his wife. He felt regret when he heard that she died.",
    "    The soul was a talented computer programmer. He hacked the database servers of his company and got core codes of products. Then he started another company and used these codes to achieve success in his business.",
    "    The soul stole medicine from a clinic because he did not have money to buy it to cure his mother’s illness.",
    "    The soul sold lower price drugs imported through unregulated channels, but cured many people who could not afford to buy official and expensive drugs.",
    "    The soul could never forgive his parents for not letting him inherit the family business.",
    "    The soul was addicted to video games and never went to work, and his parents took care of him all the time. ",
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