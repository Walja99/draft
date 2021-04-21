using UnityEngine;
using System.Collections;

public class boxController : MonoBehaviour {
    public Material originMat;
    public float transparency = 0.2f;

    private Material updateMat;
    private Color originColor;
    private Color newColor;
	// Use this for initialization
	void Start () {
        originColor = originMat.color;
        transparency = Mathf.Clamp01(transparency);
        Debug.Log("transparency: " + transparency);
        newColor = new Color(originColor.r,originColor.g,originColor.b,transparency);
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Renderer>().material.color = newColor;
	}
}
