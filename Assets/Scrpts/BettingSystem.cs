using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UIConnector;

public class BettingSystem : MonoBehaviour
{
    [SerializeField] private UIConnector uiConnector1;
    [SerializeField] private UIConnector uiConnector2;
    [SerializeField] private TextMeshProUGUI balanceText;

   
    private int betAmount1;
    private int betAmount2;
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
        uiC.autoCashout.onValueChanged.AddListener((bool IsOn) => onAutoCashoutON(uiC,IsOn));
        uiC.autoBet.onValueChanged.AddListener((bool IsOn) => onAutoBetON(uiC, IsOn));
    }
    private void SubtractBalance(UIConnector connector) {
        if (connector.getuid() == UIConnector.UID.ONE) {
            betAmount1--;
            connector.betAmount.text = betAmount1.ToString();
        }
        else if(connector.getuid() == UIConnector.UID.TWO) {
            betAmount2--;
            connector.betAmount.text = betAmount2.ToString();
        }
    }
    private void AddBalance(UIConnector connector) {
        if (connector.getuid() == UIConnector.UID.ONE) {
            betAmount1++;
            connector.betAmount.text = betAmount1.ToString();
        }
        else if (connector.getuid() == UIConnector.UID.TWO) {
            betAmount2++;
            connector.betAmount.text = betAmount2.ToString();
        }
    }
    private void doubleBalance(UIConnector connector) {
        if (connector.getuid() == UIConnector.UID.ONE) {
            betAmount1 *= 2;
            connector.betAmount.text = betAmount1.ToString();

        }
        else if (connector.getuid() == UIConnector.UID.TWO) {
            betAmount2 *= 2;
            connector.betAmount.text = betAmount2.ToString();
        }
    }
    private void onAutoCashoutON(UIConnector connector , bool IsOn) {
        RectTransform rect = connector.betAmount.GetComponent<RectTransform>();
        GameObject aCOM = connector.autoCashOutMUL.gameObject;
        if (IsOn) {
            rect.anchorMin = new Vector2(0.29f,0.12f);
            aCOM.SetActive(true);
        }
        else {
            rect.anchorMin = new Vector2(0.03f, 0.12f);
            aCOM.SetActive(false);
        }
    }
    private void onAutoBetON(UIConnector connector, bool IsOn) {
        GameObject button2X = connector.button2x.gameObject;
        GameObject aBO = connector.autoBetNo.gameObject;
        if(IsOn) {
            button2X.SetActive(false);
            aBO.SetActive(true);
        }
        else {
            button2X.SetActive(true);
            aBO.SetActive(false);
        }
    }
    private void cashoutFun(UIConnector connector) {
        
    }
    private void cashout50Fun(UIConnector connector) {
        
    }
    private void applyBet(UIConnector connector) {
        GUIsetforAppliedbet(connector);
    }
    private void onInpurFieldChanged(UIConnector connector, string arg0) {
        if (int.TryParse(connector.betAmount.text, out betAmount1)) {
            Debug.Log("Conversion successful: " + betAmount1); 
        }
        else {
            Debug.Log("Conversion failed.");
        }
        Debug.Log($"{arg0}");
    }
    private void ValidateInput(UIConnector connector) {
        if (string.IsNullOrEmpty(connector.betAmount.text) || int.Parse(connector.betAmount.text) < 1) {
            if (connector.getuid() == UIConnector.UID.ONE) {
                betAmount1 = 1;
                Debug.Log(betAmount1);
                connector.betAmount.text = betAmount1.ToString();

            }
            else if (connector.getuid() == UIConnector.UID.TWO) {
                betAmount2 = 1;
                Debug.Log(betAmount2);
                connector.betAmount.text = betAmount2.ToString();
            }
        }
    }

    private void GUIsetforAppliedbet(UIConnector connector) {
        connector.plusButton.interactable = false;
        connector.minusButton.interactable = false;
        connector.betAmount.interactable = false;
        connector.autoCashout.interactable = false;
        connector.autoBet.interactable = false;
        connector.button2x.interactable = false;
        connector.betButton.gameObject.SetActive(false);
        connector.cashoutButton.gameObject.SetActive(true);
        connector.cashout50Button.gameObject.SetActive(true);
    }
}