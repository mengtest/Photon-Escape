using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotonEngine_Play : MonoBehaviour {

    public GameObject FadeOutPrefab;
	public float DarkHolesSpeed = -7f;
	public int holesNUM=51;
	public int effectNUM=15;
	public float RrLimit= 15f;
	public int decision = 1;
	public GameObject TimeSlow,InvulnSphere;
	public GameObject gameOverPrefab;
	public GameObject pauseMenu;
	private int invuln = 0;
	public int mnu=0;
	public GameObject FeedbackAnimSource;
	private Animation FeedbackAnim;
	GameObject leavesSource;
	GameObject DarkHole;
	GameObject[] spheres;
	GameObject[] leaves;
	GameObject gameOver;
	float Rlimit,Llimit,Tlimit,Blimit,offset;
	Vector3 aux;
	float randscale;
	float dir=0.1f;
	int i,j,actualFILL;
	float space;
	int points=0;

#if UNITY_EDITOR
    public bool DInvulnerability = false;
    public bool DTSlow = false;
#endif

    Text pointsIndicator,slowIndicator,invulnIndicator;
	class GRID{
		public bool full=false;
		public int grs=1;
		public int mx=5;
		public Vector3 pos = Vector3.zero;
	};
	GRID grila = new GRID ();

    void Vpos(ref GameObject obj,float x,float y,float z)
    {
        aux = obj.transform.position;
        aux.Set(x, y, z);
        obj.transform.position = aux;
    }
    void Vscale(ref GameObject obj,float x,float y,float z)
    {
        aux = obj.transform.localScale;
        aux.Set(x, y, z);
        obj.transform.localScale = aux;
    }
		

	public int getPoints(){
		return points;
	}


	void Start () {
		FeedbackAnim = FeedbackAnimSource.GetComponent<Animation>();
        Time.timeScale = 1;
        Application.targetFrameRate = 60; ///TOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO CHEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEECK
        QualitySettings.vSyncCount = 1;
		gameOver = gameOverPrefab;
        gameOver.SetActive(false);
        pointsIndicator = GameObject.Find("Points").GetComponent<Text>();
        invulnIndicator = GameObject.Find("InvulnIndicator").GetComponent<Text>();
        slowIndicator = GameObject.Find("SlowIndicator").GetComponent<Text>();
        slowIndicator.enabled = false;
        invulnIndicator.enabled = false;
        aux = new Vector3(0, 0, 0);
        Llimit = -19.0f;
        Rlimit = 15.0f;
        Blimit = -8.5f;
        Tlimit = 8.5f;
        actualFILL = 0;
        spheres = new GameObject[holesNUM];
        leaves = new GameObject[effectNUM];
		leavesSource = GameObject.Find ("holeleave");
        DarkHole = GameObject.Find("darkholebase");
        spheres[0] = GameObject.Find("bluehole") as GameObject;
        grila.pos = new Vector3(Rlimit + 5f, Tlimit - 1f, -0.01f);
        space = (Tlimit - Blimit) / grila.mx;
        TimeSlow = Instantiate(TimeSlow, new Vector3(-20, 0, DarkHole.transform.position.z), Quaternion.identity) as GameObject;
        InvulnSphere = Instantiate(InvulnSphere, new Vector3(-20, 0, DarkHole.transform.position.z), Quaternion.identity) as GameObject;
        for (i = 1; i <= holesNUM - 1; i++)
        {
            actualFILL++;
            spheres[i] = Instantiate(DarkHole, new Vector3(Random.Range(grila.pos.x - 2.5f, grila.pos.x + 2.5f), Random.Range(Tlimit - space * grila.grs + 0.5f, Tlimit - space * (grila.grs - 1) - 0.5f), -0.01f), Quaternion.identity) as GameObject;
            grila.grs++;
            if (grila.grs > grila.mx)
            {
                grila.grs = 1;
                grila.pos.x += 5.5f;
            }

        }


        for (i = 0; i <= effectNUM - 1; i++)
        {
            leaves[i] = Instantiate(leavesSource, DarkHole.transform.position, Quaternion.identity) as GameObject;
            leaves[i].transform.Translate(0, 0, 1);
            leaves[i].transform.localScale = new Vector3(1f / effectNUM * i, 1f / effectNUM * i, 1f / effectNUM * i);
        }
        DarkHole.gameObject.SetActive(false);
        StartCoroutine("SpawnTimeSlow");
		leavesSource.SetActive (false);
    }
		
	private IEnumerator FadeTime(){
		slowIndicator.enabled = true;
		slowIndicator.text = "TimeSlow: 5";
		while (Time.timeScale > 0.5f) {
			Time.timeScale -= 0.1f;
			yield return new WaitForSeconds (0.1f);


		}
		slowIndicator.text = "TimeSlow: 4";
		yield return new WaitForSeconds (1f * Time.timeScale);
		slowIndicator.text = "TimeSlow: 3";
		yield return new WaitForSeconds (1f * Time.timeScale);
		slowIndicator.text = "TimeSlow: 2";
		yield return new WaitForSeconds (1f * Time.timeScale);
		slowIndicator.text = "TimeSlow: 1";
		yield return new WaitForSeconds (1f * Time.timeScale);
		slowIndicator.text = "TimeSlow: 0";
		slowIndicator.enabled = false;
		while (Time.timeScale != 1) {
			Time.timeScale += 0.1f;
			yield return new WaitForSeconds (0.1f);
		}
	}

	private IEnumerator Invuln(){
		invuln = 1;
		invulnIndicator.enabled = true;
		invulnIndicator.text = "Invulnerability: 5";
		yield return new WaitForSeconds (1);
		invulnIndicator.text = "Invulnerability: 4";
		yield return new WaitForSeconds (1);
		invulnIndicator.text = "Invulnerability: 3";
		yield return new WaitForSeconds (1);
		invulnIndicator.text = "Invulnerability: 2";
		yield return new WaitForSeconds (1);
		invulnIndicator.text = "Invulnerability: 1";
		yield return new WaitForSeconds (1);
		invulnIndicator.enabled = false;
		invuln = 0;
	}

    void Update () {

		if (mnu != 1) {
#if UNITY_EDITOR
            if(DInvulnerability)
            {
                DInvulnerability = false;
               StartCoroutine("Invuln");
            }
            if(DTSlow)
            {
                DTSlow = false;
                StartCoroutine("TimeSlow");
            }
#endif
            TimeSlow.transform.Translate (-7f * Time.deltaTime, 0, 0);
			InvulnSphere.transform.Translate (-7f * Time.deltaTime, 0, 0);
			for (i = 1; i <= holesNUM - 1; i++) {
				if (spheres [i].transform.position.x < (Llimit - 0.5f)) {
					if (grila.grs > grila.mx) {
						grila.grs = 1;
						grila.pos.x += 5.5f;
					}
					Vpos (ref spheres [i], Random.Range (grila.pos.x - 2.5f, grila.pos.x + 2.5f), Random.Range (Tlimit - space * grila.grs + 0.5f, Tlimit - space * (grila.grs - 1) - 0.5f), -0.01f);
					grila.grs++;



				} else if (spheres [i].transform.position.x < -7.0f && spheres [i].transform.position.x > -10.0f) {
					if (Vector3.Distance (spheres [i].transform.position, spheres [0].transform.position) < 0.95f && invuln != 1) {
						Time.timeScale = 0;
					mnu = 1;
                        pointsIndicator.text = "";
                        gameOver.SetActive (true);
					} else {
						spheres [i].transform.Translate (-7f * Time.deltaTime, 0, 0);
					}
				} else if (spheres [0].transform.position.y >= Tlimit - 0.5f || spheres [0].transform.position.y <= Blimit + 0.5f) {	
					Time.timeScale = 0;
					mnu = 1;
                    pointsIndicator.text = "";
                    gameOver.SetActive (true);
				} else {
					spheres [i].transform.Translate (-7f * Time.deltaTime, 0, 0);
				}
				spheres [0].transform.Translate (0, dir * Time.deltaTime, 0);
			}
			if (TimeSlow.transform.position.x < -7.0f && TimeSlow.transform.position.x > -10.0f) {
				if (Vector3.Distance (TimeSlow.transform.position, spheres [0].transform.position) < 0.95f) {
					TimeSlow.transform.Translate (-20, 0, 0);
					StartCoroutine ("FadeTime");
				}
			}
			if (InvulnSphere.transform.position.x < -7.0f && InvulnSphere.transform.position.x > -10.0f) {
				if (Vector3.Distance (InvulnSphere.transform.position, spheres [0].transform.position) < 0.95f) {
					InvulnSphere.transform.Translate (-20, 0, 0);
					StartCoroutine ("Invuln");
				}
			}
			if (Input.GetMouseButtonDown (0)) {
				if (mnu == 0) {
					FeedbackAnim.Play ();
					dir *= -1f;
				}

			}


		
		// -----------------------------EFECT 
		for(i=0;i<=effectNUM-1;i++)
		{
			if (leaves [i].transform.localScale.x < 0.1f) {
				randscale = Random.Range (0.1f, 0.5f);
				Vscale(ref leaves[i],randscale, randscale, randscale);
				Vpos (ref leaves [i], spheres [0].transform.position.x + Random.Range(-0.5f,0.1f), spheres [0].transform.position.y  + Random.Range(-0.2f,0.2f),-0.1f);
			} else {
				Vscale(ref leaves[i],leaves [i].transform.localScale.x - 0.02f, leaves [i].transform.localScale.y - 0.02f, leaves [i].transform.localScale.z - 0.02f);
				leaves [i].transform.Translate (-7f * Time.deltaTime, 0, 0);
			}

		}
		grila.pos.x -= 7f * Time.deltaTime;

	}
	if (Input.GetMouseButtonDown (1) && mnu == 0) {
            FeedbackAnim.Stop();
            dir *= -1f;
		    Time.timeScale = 0;
		    mnu = 1;
		    pauseMenu.SetActive (true);
	    }
	}
		
	private IEnumerator SpawnTimeSlow()
	{
		while(true)
		{
			yield return new WaitForSeconds(Random.Range(15.0f,20.0f));
			if (grila.grs > grila.mx) {
				grila.grs = 1;
				grila.pos.x += 5.5f;
			}
			StartCoroutine ("SpawnInvulnerability");
			Vpos (ref TimeSlow,Random.Range (grila.pos.x-2.5f, grila.pos.x+2.5f), Random.Range (Tlimit-space*grila.grs+0.5f,Tlimit-space*(grila.grs-1)-0.5f),-0.01f);
			grila.grs++;
		}
	}
	private IEnumerator SpawnInvulnerability()
	{
			yield return new WaitForSeconds(Random.Range(15.0f,25.0f));
			if (grila.grs > grila.mx) {
				grila.grs = 1;
				grila.pos.x += 5.5f;
			}
			Vpos (ref InvulnSphere,Random.Range (grila.pos.x-2.5f, grila.pos.x+2.5f), Random.Range (Tlimit-space*grila.grs+0.5f,Tlimit-space*(grila.grs-1)-0.5f),-0.01f);
		grila.grs++;
	}




	public void Tutorial()
	{


	}

	public void Retry(){
        Instantiate(FadeOutPrefab);
        FadeOutController.targetScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void Menu()
    {
        Instantiate(FadeOutPrefab);
        FadeOutController.targetScene = 0;
    }

	void FixedUpdate()
	{
		points++;
        if (mnu != 1) {
            pointsIndicator.text = "Points: " + points.ToString();
}
	}
	void OnDestroy(){
		Resources.UnloadUnusedAssets ();
	}
}
