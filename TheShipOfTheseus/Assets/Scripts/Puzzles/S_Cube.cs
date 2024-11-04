using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_Cube : MonoBehaviour
{
    [SerializeField] private List<string> charList;
    [SerializeField] private List<TextMeshProUGUI> textList;

    private void Start()
    {
        for (int i = 0; i < charList.Count; i++)
        {
            textList[i].text = charList[i].ToString();
        }
    }
}
