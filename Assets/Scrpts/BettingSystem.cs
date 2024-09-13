using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BettingSystem : MonoBehaviour
{
    [SerializeField] private UIConnector uiConnector1;
    [SerializeField] private UIConnector uiConnector2;
    [SerializeField] private TextMeshProUGUI balanceText;

    private int betAmount1;
    private Dictionary<UIConnector, float> bets = new Dictionary<UIConnector, float>();

    private GameManeger gm;
    private void Awake() {
        gm = GameManeger.instance;
    }
    void Start()
    {
        bets[uiConnector1] = 0;
        bets[uiConnector2] = 0;
        addListener(uiConnector1);
        addListener(uiConnector2);
        
    }
    void Update() {
        balanceText.text = $"Balance : {GameManeger.instance.getBalance()}";
    }
    private void addListener(UIConnector uiC) {
        uiC.minusButton.onClick.AddListener(() =>SubtractBalance(uiC));
        uiC.plusButton.onClick.AddListener(() => AddBalance(uiC));
        uiC.button2x.onClick.AddListener(() => doubleBalance(uiC));
        uiC.cashoutButton.onClick.AddListener(() => cashoutFun(uiC));
        uiC.cashout50Button.onClick.AddListener(() => cashout50Fun(uiC));
        uiC.betButton.onClick.AddListener(()=>applyBet(uiC));
        uiC.betAmount.onValueChanged.AddListener((string value) => onInpurFieldChanged(uiC, value));
        uiC.betAmount.onEndEdit.AddListener(delegate { ValidateInput(uiC); });
    }
    private void SubtractBalance(UIConnector connector) {
        betAmount1--;
        connector.betAmount.text = betAmount1.ToString();
    }
    private void AddBalance(UIConnector connector) {
        betAmount1++;
        connector.betAmount.text = betAmount1.ToString();
    }
    private void doubleBalance(UIConnector connector) {
        betAmount1 *= 2;
        connector.betAmount.text = betAmount1.ToString();
    }
    private void cashoutFun(UIConnector connector) {
        
    }
    private void cashout50Fun(UIConnector connector) {
        
    }
    private void applyBet(UIConnector connector) {
        connector.betButton.gameObject.SetActive(false);
        connector.cashoutButton.gameObject.SetActive(true);
        connector.cashout50Button.gameObject.SetActive(true);
    }
    private void onInpurFieldChanged(UIConnector connector, string arg0) {
        if (int.TryParse(connector.betAmount.text, out betAmount1)) {
            Debug.Log("Conversion successful: " + betAmount1);  // Output: Conversion successful: 123
        }
        else {
            Debug.Log("Conversion failed.");
        }
        Debug.Log($"{arg0}");
    }
    private void ValidateInput(UIConnector connector) {
        // If the input is empty or less than 1, revert to 1
        if (string.IsNullOrEmpty(connector.betAmount.text) || int.Parse(connector.betAmount.text) < 1) {
            betAmount1 = 1;
            connector.betAmount.text = betAmount1.ToString(); 
        }
    }
}