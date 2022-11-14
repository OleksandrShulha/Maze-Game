using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class GuideScripts : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textInfo;
    void Start()
    {
        StartCoroutine(DeactiveGuideText());
    }

    IEnumerator DeactiveGuideText()
    {
        textInfo.GetComponent<Text>().color = new Color(1f, 1f, 1f, textInfo.GetComponent<Text>().color.a - 0.01f);
        yield return new WaitForSeconds(0.05f);
        if(textInfo.GetComponent<Text>().color.a >= 0f)
        {
            StartCoroutine(DeactiveGuideText());
            Debug.Log("Work");
        }
    }
}
