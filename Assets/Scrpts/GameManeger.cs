using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public static GameManeger instance;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject dusreKaGola;
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private float lerpDuration;
    [SerializeField] private TextMeshProUGUI timerText;

    private Vector3 dusreKaGolapos;

    private Vector2 starpointdkg;
    private Vector2 endpointdkg;
    private Vector2 uvpos = new Vector2(0f,0.2f);

    private float elapsedTime = 0f;
    private float remainingTime = 6f;
    private float t;
    private float selectedMultiplier;

    private int sessionCount = 0;

    private bool inMotion = false;
    private bool inSession = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        dusreKaGolapos = dusreKaGola.transform.position;
        timerpanelActivity(true);
        getposdkg();
    }
    void Start(){

        selectMultiplier();
    }
    void Update() {

        roundTimer();
        int time = Mathf.FloorToInt(remainingTime);
        timerText.text = $"Round Starts in: \n{time}";

        if (time <= 0) {
            timerpanelActivity(false);
        }
        if (time <= -1) {
            startSession();
        }

        if (inSession) {
            endSession();
        }
    }
    private void startSession() {
        
        inSession = true;
        elapsedTime += Time.deltaTime;
        t = Mathf.Clamp(elapsedTime / lerpDuration, 0, 1);
        Scroller.Instance.scrollingLogic(uvpos);
        inMotion = true;
        startingMotion(t);
    }
      
    private void endSession() {

        float multiplier = Mathf.Round(Rocket.instance.getMultiplier() * 100f) / 100f;

        float tolerance = 0.05f;//Set Tolerence for float Point Percision

        if (Mathf.Abs(multiplier - selectedMultiplier) < tolerance) { 
            Rocket.instance.crashJet();
            inMotion = false;
            resetSession();
            inSession = false;
        }
    }
   
    private void roundTimer() {
        if (!inSession) {
            timerpanelActivity(true);
            remainingTime -= Time.deltaTime;
        }
    }
    private void resetSession() {
        remainingTime = 6f;
        elapsedTime = 0f;
        selectMultiplier();
        Rocket.instance.resetMultiplier();
        sessionCount++;
        Debug.Log(sessionCount);
    }
    private void startingMotion(float t) {
        Rocket.instance.gameObject.SetActive(true);
        dusreKaGola.transform.position = new Vector3(
            dusreKaGolapos.x,
            Mathf.Lerp(starpointdkg.y, endpointdkg.y, t),
            dusreKaGolapos.z
        );
    }
    void getposdkg() {
        //Getting the starting and End point of the DusrekaGola
        starpointdkg = new Vector3(dusreKaGolapos.x, dusreKaGolapos.y, dusreKaGolapos.z);
        endpointdkg = new Vector3(dusreKaGolapos.x, starpointdkg.y - 2.5f, dusreKaGolapos.z);
    }
    
    private void selectMultiplier() {
        selectedMultiplier = (Mathf.Round(Random.Range(1.00f, 3.00f) * 100f)/100f);
        Debug.Log(selectedMultiplier);
    }
    public float getT() {
        return t;
    }
    public bool getInmotion() {
        return inMotion;
    }
    private void timerpanelActivity(bool active) {
        if (active) {
            timerPanel.SetActive(true);
        }
        else if (!active) {
            timerPanel.SetActive(false);
        }
    }
}
