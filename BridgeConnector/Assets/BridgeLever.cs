using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BridgeLever : MonoBehaviour
{
    public GameObject[] bridgeSegments; //The segments of the bridge controlled by the levers
    public GameObject[] slots; //The final positions for the segments

    public AudioClip click; //A click sound clip
    AudioSource audioSource; //The audio source

    public int nextLevel; //The scene index for the next level

    float[] prevVal; //The previous value of the levers
    public int numLevers; //The number of levers

    public GameObject popup; //The end-of-level popup

    bool freeze = false; //True when the end of level is reached.  Freezes the scrollbar

    void Start()
    {
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        prevVal = new float[numLevers];
    }

    public void MoveSegment(GameObject scr) //Maybe integer parameter could be used to pair lever with bridge segments
    {
        if (!freeze)
        {
            //Alters the position of the bridgeSegments depending on how the user adjusts the lever
            float val = scr.GetComponent<Scrollbar>().value;
            int levDex = scr.GetComponent<LeverInfo>().leverIndex;

            //Gets the segments to be moved as well as the distance they can be moved
            int[] segsToMove = scr.GetComponent<LeverInfo>().segmentsControlled;
            int[] movementMag = scr.GetComponent<LeverInfo>().movementMags;

            //Moves the bridge segments based on how much the lever is moved
            for (int i = 0; i < segsToMove.Length; i++)
            {
                bridgeSegments[segsToMove[i]].GetComponent<RectTransform>().position += movementMag[i] * (prevVal[levDex] - val) * Vector3.up;
            }

            //Checks to see if all of the segments are in place to finish the level
            bool levelComplete = true;
            for (int i = 0; i < slots.Length; i++)
            {
                //False if at least one of the segments is out of place
                if ((bridgeSegments[i].GetComponent<RectTransform>().position - slots[i].GetComponent<RectTransform>().position).magnitude > 5F)
                {
                    levelComplete = false;
                }
            }
            if (levelComplete)
            {
                freeze = true; //Locks the scrollbars

                //Snaps all of the segments to their sockets
                for (int i = 0; i < bridgeSegments.Length; i++)
                {
                    bridgeSegments[i].GetComponent<RectTransform>().position = slots[i].GetComponent<RectTransform>().position;
                }
                StartCoroutine(EndOfLevel());  //Starts end of Level sequence
            }

            prevVal[levDex] = val;
        }
    }

    //End of Level sequence
    IEnumerator EndOfLevel()
    {
        //Debug.Log("Starting coroutine...");
        audioSource.PlayOneShot(click);//CLICK!
        popup.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            popup.GetComponent<Image>().color += new Color(0, 0, 0, 0.02F);
            //Debug.Log(popup.GetComponent<Image>().color);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nextLevel);
        //Trigger next scene
    }
}
