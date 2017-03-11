using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutController : MonoBehaviour {
    public static int targetScene = 0;
    Material fadeMaterialoriginal;
    Material fadeMaterial;
    public float Completion = 0.0f;

	void Start () {
        Time.timeScale = 1.0f;
        fadeMaterialoriginal = Resources.Load("FadeOutMaterialoriginal") as Material;
        fadeMaterial = Resources.Load("FadeOutMaterial") as Material;
        fadeMaterial.CopyPropertiesFromMaterial(fadeMaterialoriginal);
    }
	
	void Update () {
        fadeMaterial.SetFloat("_Completion", Completion);
        if(Completion==1.0f)
        {
            Destroy(this);
        }
    }

    void OnDestroy()
    {
        SceneManager.LoadScene(targetScene);
    }
}
