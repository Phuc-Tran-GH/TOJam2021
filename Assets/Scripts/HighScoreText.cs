using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreText : MonoBehaviour
{

	[SerializeField] private TextMeshProUGUI distanceText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceText.text = $"{GameManager.instance.FurthestDistance.ToString("F1")}M";
    }
}
