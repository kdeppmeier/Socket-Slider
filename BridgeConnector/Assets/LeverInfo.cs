using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInfo : MonoBehaviour
{
    //Class that contains basic information on the levers

    public int leverIndex; //The lever's identification number
    public int[] segmentsControlled; //The segments the lever controls (indicies assigned in BridgeLever)

    public int[] movementMags; //How far the lever can move the segment.  Negative numbers to invert
}
