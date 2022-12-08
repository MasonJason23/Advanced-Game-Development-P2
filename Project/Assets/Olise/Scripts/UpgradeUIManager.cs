using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeUIManager : MonoBehaviour
{
    [SerializeField] private GameObject details1;
    [SerializeField] private GameObject details2;
    [SerializeField] private GameObject details3;

    public void ChangeCardName(int cardNumber, string upgradeName)
    {
        switch (cardNumber)
        {
            case(1):
                details1.GetComponent<TextMeshProUGUI>().text = upgradeName;
                break;
            case(2):
                details2.GetComponent<TextMeshProUGUI>().text = upgradeName;
                break;
            case(3):
                details3.GetComponent<TextMeshProUGUI>().text = upgradeName;
                break;
        }
    }
}
