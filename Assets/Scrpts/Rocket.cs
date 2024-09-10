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
        Debug.Log(selfpos.y);
        Debug.Log(endpointroc.y);
    }

    private void Update() {

        //if (GameManeger.Instance.getinMotion()) {
        //    multiplier += Time.deltaTime * multiplierRate;
        //    mulitiplerText.text = multiplier.ToString("F2") + "x";
        //    startingRoc(GameManeger.Instance.getT());
        //}
    }
    public void startingRoc(float t) {
        Debug.Log(t);

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Lerp(starpointroc.y, endpointroc.y, t),
            transform.position.z
            );
    }

}
