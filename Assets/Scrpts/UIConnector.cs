using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIConnector : MonoBehaviour
{
    public Toggle autoCashout;
    public Toggle autoBet;
    public Button minusButton;
    public Button plusButton;
    public TMP_InputField betAmount;
    public TMP_InputField autoCashOutMUL;
    public TMP_InputField autoBetNo;
    public TextMeshProUGUI betText;
    public TextMeshProUGUI CashoutText;
    public Button button2x;
    public Button cashoutButton;
    public Button cashout50Button;
    public Button betButton;
    public Button cancelButton;

    [SerializeField] private UID uID;

    public enum UID { ONE, TWO }

    public UID getuid() {
        return uID;
    }
}
