using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DialogManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    private Queue<string> sentences;
    private bool IsFinishTutotial = false;
    public GameObject FirstAnswer;
    public GameObject SecondAnswer;
    public GameObject ThirdAnswer;
    public UnitInfoTutorial unitTutorial;
    public HellScoreTutorial PayLight;
    public GameObject ConversationPanel;
    public GameObject SoulTutorial;
    public GameObject NPC1;
    public GameObject NPC2;
    public GameObject NPC3;
    public GameObject NPC4;
    public bool BeginConversation = true;
    public bool IsReset = false;
    public int ConversationRound = 0;
    public Image NextImage;
    public GoPunish goPunish;
    // Start is called before the first frame update
    void Start()
    {
        sentences=new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {

        nameText.text = dialogue.name;

        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence=sentences.Dequeue();
        //Debug.Log(sentence);
        dialogueText.text = sentence;

    }
    void EndDialogue()
    {
        FindObjectOfType<AudioManager>().Play("click");
        ChangeScene();
        Debug.Log("End of conversation.");
    }
    public void HideText1()
    {
        FindObjectOfType<AudioManager>().Play("click");
        if (BeginConversation && ConversationRound==0)
        {
            FirstAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            FirstAnswer.GetComponent<Button>().enabled = false;
            SecondAnswer.gameObject.SetActive(true);
        }
        if (ConversationRound == 1)
        {
            FirstAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            FirstAnswer.GetComponent<Button>().enabled = false;
            SecondAnswer.gameObject.SetActive(true);
            PayLight.HellLight += 2;
        }
        if(ConversationRound == 2)
        {
            ConversationPanel.gameObject.SetActive(false);
            FirstAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            FirstAnswer.GetComponent<Button>().enabled = false;
        }
        if (ConversationRound == 3)
        {
            FirstAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            FirstAnswer.GetComponent<Button>().enabled = false;
            SecondAnswer.gameObject.SetActive(true);
        }
        if (ConversationRound == 4)
        {
            FirstAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            FirstAnswer.GetComponent<Button>().enabled = false;
            SecondAnswer.gameObject.SetActive(true);
        }
    }
    public void HideText2()
    {
        FindObjectOfType<AudioManager>().Play("click");
        if (BeginConversation&&ConversationRound == 0)
        {
            SecondAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            SecondAnswer.GetComponent<Button>().enabled = false;
            ThirdAnswer.gameObject.SetActive(true);
        }
        if(ConversationRound == 1)
        {
            SecondAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            SecondAnswer.GetComponent<Button>().enabled = false;
            ThirdAnswer.gameObject.SetActive(true);
        }
        if (ConversationRound == 3)
        {
            SecondAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            SecondAnswer.GetComponent<Button>().enabled = false;
            ThirdAnswer.gameObject.SetActive(true);
        }
        if (ConversationRound == 4)
        {
            SecondAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            SecondAnswer.GetComponent<Button>().enabled = false;
            ThirdAnswer.gameObject.SetActive(true);
        }
    }
    public void HideText3()
    {
        FindObjectOfType<AudioManager>().Play("click");
        if (BeginConversation&&ConversationRound == 0)
        {
            ThirdAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            //ThirdAnswer.GetComponent<Button>().enabled = false;
            BeginConversation = false;
            DisplayNextSentence();
            NPC1.GetComponent<NPCTutorial>().HoverEffect.gameObject.SetActive(true);
            SoulTutorial.gameObject.SetActive(true);
        }
        if (ConversationRound == 1)
        {
            ThirdAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            ThirdAnswer.GetComponent<Button>().enabled = false;
            StartCoroutine(Disappear());
        }
        if (ConversationRound == 3)
        {
            ThirdAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            ThirdAnswer.GetComponent<Button>().enabled = false;
            StartCoroutine(Disappear2());
        }
        if (ConversationRound == 4)
        {
            ThirdAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            ThirdAnswer.GetComponent<Button>().enabled = false;
            SoulTutorial.gameObject.SetActive(true);
            goPunish.TurnOnPunishPanel();
            IsFinishTutotial = true;
            StartCoroutine(Disappear3());
        }
    }
    public void ResetButton()
    {
        if (!IsReset)
        {
            FirstAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
            FirstAnswer.GetComponent<Button>().enabled = true;
            SecondAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
            SecondAnswer.GetComponent<Button>().enabled = true;
            ThirdAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
            ThirdAnswer.GetComponent<Button>().enabled = true;
            SecondAnswer.gameObject.SetActive(false);
            ThirdAnswer.gameObject.SetActive(false);
            IsReset = true;
        }
    }

    private void Update()
    {
        if (unitTutorial.Convicted )
        {
            ConversationRound = 1;
            Destroy(NPC1);
            ResetButton();
        }
        else if(!unitTutorial.Convicted && !BeginConversation)
        {
            ConversationRound = 2;
        }
        if(unitTutorial.Convicted && PayLight.HellLight == 0)
        {
            ResetButton();
            ConversationRound = 3;
            if (NPC4.gameObject.activeInHierarchy)
            {
                ConversationRound = 4;
            }
            if (!SoulTutorial.gameObject.activeInHierarchy)
            {
                NPC4.gameObject.SetActive(true);
                if (NPC4.gameObject.activeInHierarchy)
                {
                    ConversationRound = 4;
                }
            }
        }
        if (IsFinishTutotial)
        {
            NextImage.gameObject.SetActive(true);
        }
        switch (ConversationRound)
        {
            case 0:
                FirstAnswer.GetComponent<TextMeshProUGUI>().text = "What should I do next?".ToString();
                SecondAnswer.GetComponent<TextMeshProUGUI>().text = "OK, I got it. Let me try.".ToString();
                ThirdAnswer.GetComponent<TextMeshProUGUI>().text = "That's nice!".ToString();
                break;
            case 1:
                FirstAnswer.GetComponent<TextMeshProUGUI>().text = "Yes, I have.".ToString();
                SecondAnswer.GetComponent<TextMeshProUGUI>().text = "Sure, I can. But can you tell me what is policy?".ToString();
                ThirdAnswer.GetComponent<TextMeshProUGUI>().text = "Ok, after I go into the real hell, I will check that.".ToString();
                break;
            case 2:
                FirstAnswer.GetComponent<TextMeshProUGUI>().text = "No, I need more time.".ToString();
                SecondAnswer.GetComponent<TextMeshProUGUI>().text = "".ToString();
                ThirdAnswer.GetComponent<TextMeshProUGUI>().text = "".ToString();

                FirstAnswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                FirstAnswer.GetComponent<Button>().enabled = true;
                break;
            case 3:
                FirstAnswer.GetComponent<TextMeshProUGUI>().text = "Ok, I know what is visiting level.".ToString();
                SecondAnswer.GetComponent<TextMeshProUGUI>().text = "Let me think. I can use soul's profile right?".ToString();
                ThirdAnswer.GetComponent<TextMeshProUGUI>().text = "Make sense, let me try.".ToString();
                break;
            case 4:
                FirstAnswer.GetComponent<TextMeshProUGUI>().text = "Wow,I have a lot of resources.".ToString();
                SecondAnswer.GetComponent<TextMeshProUGUI>().text = "So I need keep Heat and Cool all the time.".ToString();
                ThirdAnswer.GetComponent<TextMeshProUGUI>().text = "I got it.".ToString();
                break;
        }
    }


    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(8);
        ConversationPanel.gameObject.SetActive(false);
        Destroy(NPC2);
        yield return new WaitForSeconds(2);
        NPC3.gameObject.SetActive(true);
        IsReset = false;
    }

    IEnumerator Disappear2()
    {
        yield return new WaitForSeconds(15);
        ConversationPanel.gameObject.SetActive(false);
        Destroy(NPC3);
        //NPC4.gameObject.SetActive(true);
        IsReset = false;
    }

    IEnumerator Disappear3()
    {
        yield return new WaitForSeconds(15);
        ConversationPanel.gameObject.SetActive(false);
        IsReset = false;
    }


    public void ChangeScene()
    {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadScene("SampleScene");
    }
}
