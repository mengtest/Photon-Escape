using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TransitionController : MonoBehaviour {
    Material menuMaterial;
    public float Completion = 0.0001f;
	void Start () {
        menuMaterial = Resources.Load("UI") as Material;
        menuMaterial.SetVector("_CPoint", new Vector4(Screen.width / 2, Screen.height / 2, 1, 0));
        menuMaterial.SetVector("_TPoint", new Vector4(Screen.width / 2, - (Screen.height/2), 1, 0));
    }

    void Awake()
    {
    }
	
	void Update () {
        menuMaterial.SetFloat("_Completion", Completion);
    }

    void OnDestroy()
    {
        menuMaterial.SetFloat("_Completion", 0);
    }
}
