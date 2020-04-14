using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    public Sprite image;
    [TextArea(1, 2)]
    public string TipName;
    [TextArea(1, 8)]
    public string TipDes;
    public int TipId;


}
