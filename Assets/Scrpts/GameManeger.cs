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
    private float remainingTime = 5f;
    private float t;
    private float selectedMultiplier;

    private bool inMotion = false;
    private bool inSession = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        dusreKaGolapos = dusreKaGola.transform.position;
        timerPanel.SetActive(true);
        getposdkg();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        
        
        int time = Mathf.FloorToInt(remainingTime);
        timerText.text = string.Format("Round Starts in: \n{0}",time);
        if (time <= 0) {
            timerPanel.SetActive(false);
        }
        if (time <= -1) {
            startSession();
        }
    }
    private void startSession() {
        elapsedTime += Time.deltaTime;
        t = Mathf.Clamp(elapsedTime / lerpDuration, 0, 1);
        Scroller.Instance.scrollingLogic(uvpos);
        startingMotion(t);
    }
    private void endSession() {

    }
    private void startingMotion(float t) {
        inMotion = true;
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
        selectedMultiplier = Mathf.Round((Random.Range(1.00f, 10.00f)*100f)/100f);
    }
    public float getT() {
        return t;
    }
    public bool getInmotion() {
        return inMotion;
    }
}
