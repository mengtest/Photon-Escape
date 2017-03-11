using UnityEngine;
public class TransitionController2 : MonoBehaviour {
    Animator Controller;
    MenuController menuScript;
    Material menuMaterial;
    public float Completion = 0.0001f;

	void Start () {
        Completion = 0.0001f;
        menuScript = GetComponent<MenuController>() as MenuController;
        menuMaterial = Resources.Load("UI") as Material;
        menuMaterial.SetVector("_CPoint", new Vector4(Screen.width / 2, Screen.height / 2, 1, 0));
        menuMaterial.SetVector("_TPoint", new Vector4(Screen.width / 2, - (Screen.height/2), 1, 0));
         Controller = GetComponent<Animator>();
    }
	
	void Update () {
        menuMaterial.SetFloat("_Completion", Completion);
        if(menuScript.terminate)
        { 
            menuScript.terminate = false;
            Controller.SetTrigger("MenuFadeOut");
        }
        if(Completion==0.0f)
        {
            Time.timeScale = 1;
            PhotonEngine_Play d = GameObject.Find("Main Camera").GetComponent<PhotonEngine_Play>() as PhotonEngine_Play;
            d.mnu = 0;
            gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        menuMaterial.SetFloat("_Completion", 0.01f);
    }

}
