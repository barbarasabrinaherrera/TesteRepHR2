using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextChangeUICanvas : MonoBehaviour
{
    // UI Text GameObjects
    public GameObject textObjective;
    public GameObject textDistance;
    public GameObject textSuccess;

    // Game Variables
    public string objectiveA;
    public float distance;
    public bool success;

    // Text Components
    TextMeshProUGUI tmpObjectiveText;
    TextMeshProUGUI tmpDistanceText;
    TextMeshProUGUI tmpSuccessText;

    // Start is called before the first frame update
    void Start()
    {
        // Text Mesh Pro initialization
        tmpObjectiveText = textObjective.GetComponent<TextMeshProUGUI>();
        tmpDistanceText = textDistance.GetComponent<TextMeshProUGUI>();
        tmpSuccessText = textSuccess.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Text Mesh Pro
        tmpObjectiveText.text = objectiveA;
        tmpDistanceText.text = distance.ToString(); //converte para String pq é float
        tmpSuccessText.text = success.ToString(); //converte para String pq é boolean
    }
}
