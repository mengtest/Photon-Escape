using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
	private int cScore,bScore,level;
	Text tcScore,tbScore;
    public GameObject FadeOutPrefabInstance;
    private GameObject refer;

	void OnEnable(){
        refer = Instantiate(FadeOutPrefabInstance);
		level = SceneManager.GetActiveScene ().buildIndex;
		tcScore = GameObject.Find("points_score").GetComponent<Text>();
		tbScore = GameObject.Find("best_score").GetComponent<Text>();
		PhotonEngine_Play aux = GameObject.Find("Main Camera").GetComponent<PhotonEngine_Play> ();
		cScore = aux.getPoints ();
		bScore = PlayerPrefs.GetInt ("bScore"+level, 0);
		if (cScore > bScore) {
			bScore = cScore;
			PlayerPrefs.SetInt ("bScore"+level, bScore);
		}
		tcScore.text = cScore.ToString();
		tbScore.text = bScore.ToString();
	}
    private void OnDisable()
    {
        PlayerPrefs.Save();
        Destroy(refer);
    }
}
