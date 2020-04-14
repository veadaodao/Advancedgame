using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Policy : MonoBehaviour
{
    public string policyName;
    [TextArea(1, 3)]
    public string policyDes;
    //public Policy[] previousPolicy;
    public int PolicyId;
    public bool isSigned = false;

}
