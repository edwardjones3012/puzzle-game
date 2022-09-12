using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TMP_Text))]
public class TextJumble : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool jumble;
    TMP_Text text;
    string originalText;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        originalText = text.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumble) text.text = GetRandomString(12);
        else if (text.text != originalText)
        {
            text.text = originalText;
        }
    }

    public static string GetRandomString(int length)
    {
        var r = new System.Random();
        return new String(Enumerable.Range(0, length).Select(n => (Char)(r.Next(32, 127))).ToArray());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        jumble = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        jumble = false;
    }
}
