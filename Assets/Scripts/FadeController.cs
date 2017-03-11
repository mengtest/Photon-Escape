using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {
    Material fadeMaterial;
    public float Completion=0.01f;// = 1.0f;

	void Start () {
        fadeMaterial = Resources.Load("FadeInMaterial") as Material;
    }
	

	void Update () {
        fadeMaterial.SetFloat("_Completion", Completion);
        if(Completion==0.0f)
        {
            Destroy(this.gameObject);
        }
    }

   
}
