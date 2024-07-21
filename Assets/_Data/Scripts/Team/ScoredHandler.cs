using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class ScoredHandler : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _scoredText;

    [SerializeField] int scored = 0;


    private void Awake()
    {
        _scoredText.text = scored.ToString();
    }

    public void UpdateScored(int number)
    {
        scored = scored + number;
        _scoredText.text = scored.ToString();
    }
}
