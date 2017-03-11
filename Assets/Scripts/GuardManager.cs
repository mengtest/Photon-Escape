using UnityEngine;

public class GuardManager : MonoBehaviour {
	public static GuardManager sGuard;
    public static AudioSource audio;
	// Use this for initialization
	void Start () {
       
        if (!sGuard) {
            sGuard = this;
            DontDestroyOnLoad(this.gameObject);
            audio = GetComponent<AudioSource>();
            audio.Stop();
            if (PlayerPrefs.GetInt("Music", 1) == 1)
            {
                doPlay();
            }
            
		} else {
			Destroy (this.gameObject);
		}
	}
	void onDestroy(){
		sGuard = null;
	}

    public static void doPlay()
    {
        audio.Play();
    }

    public static void stopPlay()
    {
        audio.Stop();
    }

}
