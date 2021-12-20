using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public int time;
    public Text text;
    private void Start()
    {
        StartCoroutine(startcounter());
      
        
    }
    public IEnumerator startcounter()
    {
        while(time>0)
        {
            text.text = time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }

        text.text = "Go!";
       /* while(time!=0)
        {
            Time.timeScale = 0;
        }*/
        yield return new WaitForSeconds(1f);
        text.gameObject.SetActive(false);
    }
}
