using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class boundedTreeController_with_out : MonoBehaviour {
    public GameObject plane;
    public GameObject cube;
    public Text text1;
    public Text text2;
    public Text title;
    public GameObject unboundTexture;
    public GameObject boundTexture;
    public GameObject unbound;
    public GameObject bound;
    public Camera cam0;
    public Camera cam1;
    public Camera cam2;

    private float timer = 0.0f;

	// Use this for initialization
	void Start () {
        text1.text = "";
        text2.text = "";
        title.text = "";
        cam0.enabled = true;
        cam1.enabled = false;
        cam2.enabled = false;
        plane.SetActive(false);
        cube.SetActive(false);
        unboundTexture.SetActive(false);
        boundTexture.SetActive(false);
        unbound.SetActive(false);
        bound.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        //0-4s text1
        if (timer <= 4)
        {
            text1.text = "Part 2";
            text2.text = "BOUNDED TREE WITHOUT TEXTURE";
            title.text = "";
            cam0.enabled = true;
            cam1.enabled = false;
            cam2.enabled = false;
            plane.SetActive(false);
            cube.SetActive(false);
            unboundTexture.SetActive(false);
            boundTexture.SetActive(false);
            unbound.SetActive(false);
            bound.SetActive(false);
        }
        //4-8s untext_unbound
        else if (timer > 4 && timer <= 6)
        {
            text1.text = "";
            text2.text = "";
            title.text = "Unbounded Tree without Texture";
            cam0.enabled = false;
            cam1.enabled = true;
            cam2.enabled = false;
            plane.SetActive(true);
            cube.SetActive(false);
            unboundTexture.SetActive(false);
            boundTexture.SetActive(false);
            unbound.SetActive(true);
            bound.SetActive(false);
        }
        //8-12s untext_bound
        else if (timer > 6 && timer <= 12)
        {
            text1.text = "";
            text2.text = "";
            title.text = "Bounded Tree without Texture";
            cam0.enabled = false;
            cam1.enabled = true;
            cam2.enabled = false;
            plane.SetActive(true);
            cube.SetActive(true);
            unboundTexture.SetActive(false);
            boundTexture.SetActive(false);
            unbound.SetActive(false);
            bound.SetActive(true);
        }

        //12-16s text2
        else if (timer > 12 && timer <= 16)
        {
            text1.text = "Part 3";
            text2.text = "BOUNDED TREE WITH TEXTURE";
            title.text = "";
            cam0.enabled = true;
            cam1.enabled = false;
            cam2.enabled = false;
            plane.SetActive(false);
            cube.SetActive(false);
            unboundTexture.SetActive(false);
            boundTexture.SetActive(false);
            unbound.SetActive(false);
            bound.SetActive(false);
        }
        //16-20s text_unbound
        else if (timer > 16 && timer <= 18)
        {
            text1.text = "";
            text2.text = "";
            title.text = "Unbounded Tree with Texture";
            cam0.enabled = true;
            cam1.enabled = false;
            cam2.enabled = false;
            plane.SetActive(true);
            cube.SetActive(false);
            unboundTexture.SetActive(true);
            boundTexture.SetActive(false);
            unbound.SetActive(false);
            bound.SetActive(false);
        }
        //20-24s text_bound
        else if (timer > 18)
        {
            text1.text = "";
            text2.text = "";
            title.text = "Bounded Tree with Texture";
            cam0.enabled = true;
            cam1.enabled = false;
            cam2.enabled = false;
            plane.SetActive(true);
            cube.SetActive(true);
            unboundTexture.SetActive(false);
            boundTexture.SetActive(true);
            unbound.SetActive(false);
            bound.SetActive(false);
        }
	
	}
}
