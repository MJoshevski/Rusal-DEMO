using UnityEngine;
using System.Collections;

public class ScrollScript : MonoBehaviour {

    public Transform[] backgrounds; //Lista od site ramnini za parallax
    private float[] parallaxScales;  //proporcija za dvizenje na kamera paralelno so pozadina
    public float smoothing = 1f;  //kolku glatko ke se dvizi parallax. Setiraj nad 0.
    private Transform cam;  // referenca do transform od glavna kamera
    private Vector3 prevCamPos; //pozicija na kamerata od predhoden frame

    void Awake()        //povikana pred Start. Koristi za referenci
    {
        //postavi referenci za kamera
        cam = Camera.main.transform;

    }

	void Start () {
        //prev Frame  ima pozicija na CameraPos
        prevCamPos = cam.position;
        parallaxScales = new float[backgrounds.Length];

        //dodeli korespondiracki parallax golemni (parallaxScale)
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }	
	}
	
	
	void Update () {

        for(int i = 0; i < backgrounds.Length; i++)
        {
            //parallaxot e sprotivno od dvizenje na kamera, bidejki prevFrame se mnozi so goleminata
            float parallax = (prevCamPos.x - cam.position.x) * parallaxScales[i];

            // postavi celna x-pozicija koja e zbir od momentalnata i parallax pozicija
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //kreiraj celna pozicija koja e momentalnata pozicija so celnata x-pozicija
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade in/out pomegju celna pozicija i momentalna pozicija
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

         }
        //setiraj prevCamPos vo pozicijata na krajot od frejmot
        prevCamPos = cam.position;
	    
	}
}
