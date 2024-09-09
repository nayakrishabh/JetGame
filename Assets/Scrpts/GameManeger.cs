using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject dusreKaGola;
    [SerializeField] private float lerpDuration;

    private Vector3 rocketpos;
    private Vector3 dusreKaGolapos;

    private Vector2 starpointroc;
    private Vector2 endpointroc;
    private Vector2 starpointdkg;
    private Vector2 endpointdkg;
    private Vector2 uvpos; 

    private float elapsedTime = 0f;
    private float remainingTime = 5f;

    private void Awake() {
        rocketpos = rocket.transform.position;
        dusreKaGolapos = dusreKaGola.transform.position;
        uvpos = new Vector2(0f,0.2f);
    }
    void Start()
    {
        getposroc();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp(elapsedTime / lerpDuration, 0, 1);
        Scroller.Instance.scrollingLogic(uvpos);
        startingMotion(t);

    }

    void getposroc() {
        //Getting the starting and End point of the Rocket and Dusre
        //kaGola
        starpointroc = new Vector3(rocketpos.x, rocketpos.y, rocketpos.z);
        endpointroc = new Vector3(rocketpos.x, starpointroc.y + 2.5f, rocketpos.z);

        starpointdkg = new Vector3(dusreKaGolapos.x, dusreKaGolapos.y, dusreKaGolapos.z);
        endpointdkg = new Vector3(dusreKaGolapos.x, starpointdkg.y - 2.5f, dusreKaGolapos.z);
    }
    private void startingMotion(float t) {
        /*
         For Rocket Movement 
         */
        rocket.transform.position = new Vector3(
            rocketpos.x,
            Mathf.Lerp(starpointroc.y, endpointroc.y, t),
            rocketpos.z
        );
        /*
         For DusreKaGola Movement 
         */
        dusreKaGola.transform.position = new Vector3(
            dusreKaGolapos.x,
            Mathf.Lerp(starpointdkg.y, endpointdkg.y, t),
            dusreKaGolapos.z
        );
    }
}
