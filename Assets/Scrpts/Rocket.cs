using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public static Rocket instance;

    [SerializeField] private TextMeshProUGUI mulitiplerText;

    private float multiplier = 1.00f;
    private float multiplierRate = 0.10f;

    private Vector3 starpointroc;
    private Vector3 endpointroc;
    private Vector3 selfpos;

    private bool _isCrashed;
    private bool _canCashout;

    public bool isCrashed { get { return _isCrashed; } set { if (value) { _isCrashed = true; } else { _isCrashed = false; } } }
    public bool canCashout { get { return _canCashout; } set { if (value) { _canCashout = true; } else { _canCashout = false; } } }
    private void Awake() {
        
        if (instance == null) {
            instance = this;
        }
        starpointroc = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endpointroc = new Vector3(transform.position.x, starpointroc.y + 2.5f, transform.position.z);
    }

    private void Update() { 
        if(GameManager.instance.getInmotion()) {
            gameObject.SetActive(true);
            multiplier += Time.deltaTime * multiplierRate;
            mulitiplerText.text = multiplier.ToString("F2") + "x";
            canCashout = true;
            startingRoc(GameManager.instance.getT());
        }
    }
    public void startingRoc(float t) {

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Lerp(starpointroc.y, endpointroc.y, t),
            transform.position.z
            );
    }
    public void crashJet() {
        isCrashed = true;
        canCashout = false;
        gameObject.SetActive(false);
        transform.position = starpointroc;
    }
    public float getMultiplier() {
        return multiplier;
    }
    public void resetMultiplier() {
        multiplier = 1.00f; // Enable rocket for new session
    }
    public bool getIsCrashed() {
        return isCrashed;
    }
}
