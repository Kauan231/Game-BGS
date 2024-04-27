using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mechanics;
using TMPro;

public class ShowStats : MonoBehaviour
{
    public TextMeshProUGUI Money;
    public void Update() {
        Money.text = GameObject.FindWithTag("WalletManager").GetComponent<WalletManager>().currentMoney.ToString();
    }
}
