using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BettingSystem : MonoBehaviour
{
    [SerializeField] private UIConnector uiConnector1;
    [SerializeField] private UIConnector uiConnector2;
    // Start is called before the first frame update
    void Start()
    {
        addListener();
    }

    private void addListener() {
        uiConnector1.minusButton.onClick.AddListener(SubtractBalance);
        uiConnector1.plusButton.onClick.AddListener(AddBalance);
        uiConnector1.button2x.onClick.AddListener(doubleBalance);
        uiConnector1.cashoutButton.onClick.AddListener(cashoutFun);
        uiConnector1.cashout50Button.onClick.AddListener(cashout50Fun);
        uiConnector2.minusButton.onClick.AddListener(SubtractBalance);
        uiConnector2.plusButton.onClick.AddListener(AddBalance);
        uiConnector2.button2x.onClick.AddListener(doubleBalance);
        uiConnector2.cashoutButton.onClick.AddListener(cashoutFun);
        uiConnector2.cashout50Button.onClick.AddListener(cashout50Fun);
    }
    private void SubtractBalance() {
        Debug.Log("Functioning");
    }
    private void AddBalance() {
        
    }
    private void doubleBalance() {
        
    }
    private void cashoutFun() {
        
    }
    private void cashout50Fun() {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}