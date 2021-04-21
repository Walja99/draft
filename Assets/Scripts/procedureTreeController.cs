using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class procedureTreeController : MonoBehaviour {
    public GameObject tree1;
    public GameObject tree2;
    public GameObject plane;
    public GameObject canvas;
    public GameObject text1;
    public GameObject text2;

    private float timer = 0.0f;
	// Use this for initialization
	void Start () {
        tree1.SetActive(false);
        tree2.SetActive(false);
        plane.SetActive(false);
        canvas.SetActive(false);
        text1.SetActive(true);
        text2.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 4 && timer <= 8 )
        {
            tree1.SetActive(false);
            tree2.SetActive(false);
            plane.SetActive(false);
            canvas.SetActive(false);
            text1.SetActive(false);
            text2.SetActive(true);
        }
        else if ( timer > 8 && timer <= 12 )
        {
            tree1.SetActive(true);
            tree2.SetActive(false);
            plane.SetActive(true);
            canvas.SetActive(true);
            text1.SetActive(false);
            text2.SetActive(false);
        }
        else if (timer > 14)
        {
            tree1.SetActive(false);
            tree2.SetActive(true);
            plane.SetActive(true);
            canvas.SetActive(true);
            text1.SetActive(false);
            text2.SetActive(false);
        }
        
	}
}
