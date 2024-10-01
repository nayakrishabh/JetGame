using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UIConnector;

public class BettingSystem : MonoBehaviour {

    [SerializeField] private UIConnector uiConnector1;
    [SerializeField] private UIConnector uiConnector2;
    [SerializeField] private TextMeshProUGUI balanceText;

    private int betAmount1 = 1, betAmount2 = 1, autoBetTimes1, autoBetTimes2;
    private float cashOutMul1, cashOutMul2;
    private Dictionary<UIConnector, float> bets = new Dictionary<UIConnector, float>();

    private GameManager gm;

    private void Awake() {
        gm = GameManager.instance;
    }
    void Start() {
        bets[uiConnector1] = 0;
        bets[uiConnector2] = 0;
        addListener(uiConnector1);
        addListener(uiConnector2);

    }
    void Update() {
        balanceText.text = $"Balance : {gm.getBalance()}";
        updateBetRounds(uiConnector1);
        updateBetRounds(uiConnector2);
        cashoutMulUPdate(uiConnector1);
        cashoutMulUPdate(uiConnector2);
        allowCashout(uiConnector1);
        allowCashout(uiConnector2);
        lossBet(uiConnector1);
        lossBet(uiConnector2);

    }

    #region UPDATE BET AMOUNT
    private void SubtractBalance(UIConnector connector) {
        if (connector.getuid() == UIConnector.UID.ONE) {
            betAmount1--;
            connector.betAmount.text = betAmount1.ToString();
        }
        else if (connector.getuid() == UIConnector.UID.TWO) {
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
    #endregion

    #region AUTOCASHOUT
    private void onAutoCashoutON(UIConnector connector, bool IsOn) {
        RectTransform rect = connector.betAmount.GetComponent<RectTransform>();
        GameObject aCOM = connector.autoCashOutMUL.gameObject;
        if (IsOn) {
            rect.anchorMin = new Vector2(0.29f, 0.12f);
            aCOM.SetActive(true);
        }
        else {
            rect.anchorMin = new Vector2(0.03f, 0.12f);
            aCOM.SetActive(false);
        }
    }
    private void onAutoCashoutChanged(UIConnector connector, string value) {
        if (connector.getuid() == UIConnector.UID.ONE) {
            cashOutMul1 = float.Parse(value);
            connector.autoCashOutMUL.text = $"{value}";
        }
        else if (connector.getuid() == UIConnector.UID.TWO) {
            cashOutMul2 = float.Parse(value);
            connector.autoCashOutMUL.text = $"{value}";
        }
    }
    private void ValidateInputCashoutMul(UIConnector connector) {
        if (string.IsNullOrEmpty(connector.autoCashOutMUL.text) || float.Parse(connector.autoCashOutMUL.text) < 1.00f) {
            if (connector.getuid() == UIConnector.UID.ONE) {
                cashOutMul1 = 2.50f;
                connector.autoCashOutMUL.text = $"{cashOutMul1.ToString()}";
            }
            else if (connector.getuid() == UIConnector.UID.TWO) {
                cashOutMul2 = 2.50f;
                connector.autoCashOutMUL.text = $"{cashOutMul2.ToString()}";
            }
        }
    }
    #endregion

    #region AUTOBET Logic
    private void onAutoBetON(UIConnector connector, bool IsOn) {
        GameObject button2X = connector.button2x.gameObject;
        GameObject aBO = connector.autoBetNo.gameObject;
        if (IsOn) {
            button2X.SetActive(false);
            aBO.SetActive(true);
        }
        else {
            button2X.SetActive(true);
            aBO.SetActive(false);
        }
    }
    private void onAutoBetNOChanged(UIConnector connector, string value) {
        if (connector.getuid() == UIConnector.UID.ONE) {
            autoBetTimes1 = int.Parse(value);
            connector.autoBetNo.text = value;
        }
        else if (connector.getuid() == UIConnector.UID.TWO) {
            autoBetTimes2 = int.Parse(value);
            connector.autoBetNo.text = value;
        }
    }
    private void ValidateInputBetNo(UIConnector connector) {
        if (string.IsNullOrEmpty(connector.autoBetNo.text) || int.Parse(connector.autoBetNo.text) < 1) {
            if (connector.getuid() == UIConnector.UID.ONE) {
                autoBetTimes1 = 1;
                connector.autoBetNo.text = autoBetTimes1.ToString();
            }
            else if (connector.getuid() == UIConnector.UID.TWO) {
                autoBetTimes2 = 1;
                connector.autoBetNo.text = autoBetTimes2.ToString();
            }
        }
    }
    #endregion

    #region CASHOUT LOGIC
    private void cashoutFun(UIConnector connector) {

    }
    private void cashoutMulUPdate(UIConnector connector) {
        if (!Rocket.instance.isCrashed) {
            float multi = MathF.Round(Rocket.instance.getMultiplier() * 100f) / 100f;
            if (gm.getInSession()) {
                connector.CashoutText.text = $"Cashout\n{bets[connector] * multi}";
            }
        }
    }
    private void lossBet(UIConnector connector) {
        if (!Rocket.instance.canCashout) {
            GUIsetforOFFSession(connector);
        }
    }
    private void allowCashout(UIConnector connector) {
        if (Rocket.instance.canCashout) {
            GUIsetforONSession(connector);
        }
    }
    private void cashout50Fun(UIConnector connector) {

    }
    #endregion

    private void applyBet(UIConnector connector) {
        if (connector.getuid() == UIConnector.UID.ONE) {
            gm.setBalance(-betAmount1);
            bets[connector] = betAmount1;
        }
        else if (connector.getuid() == UIConnector.UID.TWO) {
            gm.setBalance(-betAmount2);
            bets[connector] = betAmount2;
        }
        GUIsetforAppliedbet(connector);
    }
    private void onCanceled(UIConnector connector) {
        if (connector.getuid() == UIConnector.UID.ONE) {
            Debug.Log(betAmount1);
            gm.setBalance(betAmount1);
        }
        else if (connector.getuid() == UIConnector.UID.TWO) {
            Debug.Log(betAmount2);
            gm.setBalance(betAmount2);
        }
        bets[connector] = 0;
        GUIsetforCanceledBet(connector);
    }
    private void onInputFieldChanged(UIConnector connector, string arg0) {
        if (connector.getuid() == UIConnector.UID.ONE) {
            if (int.TryParse(connector.betAmount.text, out betAmount1)) {
                Debug.Log("Conversion successful: " + betAmount1);
            }
            else {
                Debug.Log("Conversion failed.");
            }
        }
        else if (connector.getuid() == UIConnector.UID.TWO) {
            if (int.TryParse(connector.betAmount.text, out betAmount2)) {
                Debug.Log("Conversion successful: " + betAmount2);
            }
            else {
                Debug.Log("Conversion failed.");
            }
        }
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
    private void updateBetRounds(UIConnector connector) {
        if (!gm.getInSession()) {
            connector.betText.text = "BET";
        }
        else {
            connector.betText.text = "BET for Next Round";
        }
    }
    
    private void addListener(UIConnector uiC) {
        uiC.minusButton.onClick.AddListener(() => SubtractBalance(uiC));
        uiC.plusButton.onClick.AddListener(() => AddBalance(uiC));
        uiC.button2x.onClick.AddListener(() => doubleBalance(uiC));
        uiC.cashoutButton.onClick.AddListener(() => cashoutFun(uiC));
        uiC.cashout50Button.onClick.AddListener(() => cashout50Fun(uiC));
        uiC.betButton.onClick.AddListener(() => applyBet(uiC));
        uiC.cancelButton.onClick.AddListener(() => onCanceled(uiC));

        uiC.betAmount.onValueChanged.AddListener((string value) => onInputFieldChanged(uiC, value));
        uiC.betAmount.onEndEdit.AddListener(delegate { ValidateInput(uiC); });

        uiC.autoCashout.onValueChanged.AddListener((bool IsOn) => onAutoCashoutON(uiC, IsOn));
        uiC.autoCashOutMUL.onValueChanged.AddListener((string value) => onAutoCashoutChanged(uiC, value));
        uiC.autoCashOutMUL.onEndEdit.AddListener(delegate { ValidateInputCashoutMul(uiC); });

        uiC.autoBet.onValueChanged.AddListener((bool IsOn) => onAutoBetON(uiC, IsOn));
        uiC.autoBetNo.onValueChanged.AddListener((string value) => onAutoBetNOChanged(uiC, value));
        uiC.autoBetNo.onEndEdit.AddListener(delegate { ValidateInputBetNo(uiC); });

    }
    private void AllGUIOn(UIConnector connector) {
        connector.plusButton.interactable = true;
        connector.minusButton.interactable = true;
        connector.betAmount.interactable = true;
        connector.autoCashout.interactable = true;
        connector.autoBet.interactable = true;
        connector.button2x.interactable = true;
        connector.autoBetNo.interactable = true;
        connector.autoCashOutMUL.interactable = true;
        connector.betButton.gameObject.SetActive(true);


        connector.cashoutButton.gameObject.SetActive(false);
        connector.cashout50Button.gameObject.SetActive(false);
        connector.cancelButton.gameObject.SetActive(false);
    }
    private void GUIsetforAppliedbet(UIConnector connector) {
        connector.plusButton.interactable = false;
        connector.minusButton.interactable = false;
        connector.betAmount.interactable = false;
        connector.autoCashout.interactable = false;
        connector.autoBet.interactable = false;
        connector.button2x.interactable = false;
        connector.autoBetNo.interactable = false;
        connector.autoCashOutMUL.interactable = false;
        connector.betButton.gameObject.SetActive(false);
        connector.cashoutButton.gameObject.SetActive(false);
        connector.cashout50Button.gameObject.SetActive(false);
        connector.cancelButton.gameObject.SetActive(true);
    }
    private void GUIsetforCanceledBet(UIConnector connector) {
        connector.plusButton.interactable = true;
        connector.minusButton.interactable = true;
        connector.betAmount.interactable = true;
        connector.autoCashout.interactable = true;
        connector.autoBet.interactable = true;
        connector.button2x.interactable = true;
        connector.autoBetNo.interactable = true;
        connector.autoCashOutMUL.interactable = true;
        connector.betButton.gameObject.SetActive(true);
        connector.cancelButton.gameObject.SetActive(false);
        connector.cashoutButton.gameObject.SetActive(false);
        connector.cashout50Button.gameObject.SetActive(false);
    }
    private void GUIsetforONSession(UIConnector connector) {
        connector.plusButton.interactable = false;
        connector.minusButton.interactable = false;
        connector.betAmount.interactable = false;
        connector.autoCashout.interactable = false;
        connector.autoBet.interactable = false;
        connector.button2x.interactable = false;
        connector.autoBetNo.interactable = false;
        connector.autoCashOutMUL.interactable = false;
        connector.cancelButton.gameObject.SetActive(false);
        connector.betButton.gameObject.SetActive(false);

        connector.cashoutButton.gameObject.SetActive(true);
        connector.cashout50Button.gameObject.SetActive(true);
    }
    private void GUIsetforOFFSession(UIConnector connector) {
        connector.plusButton.interactable = true;
        connector.minusButton.interactable = true;
        connector.betAmount.interactable = true;
        connector.autoCashout.interactable = true;
        connector.autoBet.interactable = true;
        connector.button2x.interactable = true;
        connector.autoBetNo.interactable = true;
        connector.autoCashOutMUL.interactable = true;
        connector.betButton.gameObject.SetActive(true);

        connector.cashoutButton.gameObject.SetActive(false);
        connector.cashout50Button.gameObject.SetActive(false);
    }
}