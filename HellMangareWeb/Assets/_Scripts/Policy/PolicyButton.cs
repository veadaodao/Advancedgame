using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PolicyButton : MonoBehaviour
{
    public TextMeshProUGUI policyNameText;
    public TextMeshProUGUI policyDesText;
    
    public int buttonId;
    
    
    // Clike button to display each policy Info
    public void PressPolicyButton()
    {
        FindObjectOfType<AudioManager>().Play("click");
        PolicyManager.instance.activePolicy = transform.GetComponent<Policy>();
        policyNameText.text = PolicyManager.instance.policies[buttonId].policyName;
        policyDesText.text = PolicyManager.instance.policies[buttonId].policyDes;
              
    }

}
