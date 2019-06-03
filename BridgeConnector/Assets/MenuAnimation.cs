using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAnimation : MonoBehaviour
{
    //This class animates the menu - decorative purposes
    
    public GameObject scrBar;  //The scrollbar
    public GameObject moveable;  //The white square

    void Start()
    {
        StartCoroutine(Animate());
    }

    //Animates the fake scrollbar and moveable on the menu
    IEnumerator Animate()
    {
        Scrollbar scr = scrBar.GetComponent<Scrollbar>();
        RectTransform rect = moveable.GetComponent<RectTransform>();

        while (true)
        {
            //Moves scrollbar and moveable up and down in a timed fashion
            for (int i = 0; i < 100; i++)
            {
                scr.value += 0.01F;
                rect.position += Vector3.down * 2;
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 100; i++)
            {
                scr.value -= 0.01F;
                rect.position -= Vector3.down * 2;
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
