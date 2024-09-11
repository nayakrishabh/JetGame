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
    private void Awake() {
        
        if (instance == null) {
            instance = this;
        }
        starpointroc = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endpointroc = new Vector3(transform.position.x, starpointroc.y + 2.5f, transform.position.z);
    }

    private void Update() { 

        if(GameManeger.instance.getInmotion()) {
            gameObject.SetActive(true);
        }
            if (GameManeger.instance.getInmotion()) {
                multiplier += Time.deltaTime * multiplierRate;
                mulitiplerText.text = multiplier.ToString("F2") + "x";
                startingRoc(GameManeger.instance.getT());
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
        gameObject.SetActive(false);
    }
    public float getMultiplier() {
        return multiplier;
    }
    public void resetMultiplier() {
        multiplier = 1.00f; // Enable rocket for new session
    }
}
