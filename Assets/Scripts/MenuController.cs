using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    public GameObject FadeOutPrefab;
    public GameObject TutorialPrefab;

    private int music, sound,level;
	public Text Switch1,Switch2,Music,Sound;
	public Button Sw1,Sw2;
    public bool terminate = false;
    Color myColor = new Color(1, 0.6f, 0.6f,1);
    // Use this for initialization

	void Start () {
		Sw1.onClick.RemoveAllListeners();
		Sw2.onClick.RemoveAllListeners();
		music = PlayerPrefs.GetInt ("Music", 1);
	//	sound = PlayerPrefs.GetInt ("Sound", 1);
		level = SceneManager.GetActiveScene ().buildIndex;
	//	Debug.Log (level);
		switch (level) {
		default:
			break;
		case 1:
			Switch1.text = "HARDCORE";
			Sw1.onClick.AddListener(()=>{SwHardcore ();});
			Switch2.text = "IMPOSSIBLE";
			Sw2.onClick.AddListener(()=>{SwImpossible ();});
			break;
		case 2:
			Switch1.text = "NORMAL";
			Sw1.onClick.AddListener(()=>{SwNormal ();});
			Switch2.text = "IMPOSSIBLE";
			Sw2.onClick.AddListener(()=>{SwImpossible ();});
			break;
		case 3:
			Switch1.text = "NORMAL";
			Sw1.onClick.AddListener(()=>{SwNormal ();});
			Switch2.text = "HARDCORE";
			Sw2.onClick.AddListener(()=>{SwHardcore ();});
			break;
		}
		if (music==0) {
			SetRedMusic ();
		}
	/*	if (sound==0) {
			SetRedSound ();
		}*/
	}

	public void SwNormal(){
        Instantiate(FadeOutPrefab);
        FadeOutController.targetScene = 1;

    }

	public void SwHardcore(){
        Instantiate(FadeOutPrefab);
        FadeOutController.targetScene = 2;
    }

	public void SwImpossible(){
        Instantiate(FadeOutPrefab);
        FadeOutController.targetScene = 3;
    }

	void SetRedMusic(){
		Music.color = myColor;
  
    }
	void SetRedSound(){
		Sound.color = myColor;
	}
	void SetWhiteMusic(){
		Music.color = Color.white;
     
	}
	void SetWhiteSound(){
		Sound.color = Color.white;
	}

	void SwitchMC(){
		if (Music.color == myColor) {
            GuardManager.doPlay();
            PlayerPrefs.SetInt("Music", 1);
            SetWhiteMusic ();
		} else {
            GuardManager.stopPlay();
            PlayerPrefs.SetInt("Music", 0);
            SetRedMusic ();
		}
	}

	void SwitchSC(){
		if (Sound.color == myColor) {
			SetWhiteSound ();
		} else {
			SetRedSound ();
		}
	}

	public void SwitchMusic(){
		PlayerPrefs.SetInt("Music",sw(ref music));
		SwitchMC ();
	}
	public void SwitchSound(){
		PlayerPrefs.SetInt ("Sound", sw (ref sound));
		SwitchSC ();
	}

	int sw(ref int x){
		if (x == 1) {
			x = 0;
		} else {
			x = 1;
		}
		return x;
	}

	public void Continue(){
        terminate = true;
	}
    public void Quit(){
		Application.Quit ();
	}

	public void Help(){
        Instantiate(TutorialPrefab);
    }
}
