using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {
    public GameObject[] TutorialElements;
    private int ElemsSize, index=0;


	// Use this for initialization
	void Start () {
        TutorialElements[index].SetActive(true);
        ElemsSize = TutorialElements.Length-1;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (index < ElemsSize)
            {
                TutorialElements[index].SetActive(false);
                ++index;
                TutorialElements[index].SetActive(true);
            }
            else
            {
                PlayerPrefs.SetInt("Tutorial", 1);
                PlayerPrefs.Save();
                Destroy(this.gameObject);
            }
        }
    }
}
