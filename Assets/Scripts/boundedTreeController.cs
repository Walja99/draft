using UnityEngine;
using System.Collections;

public class boundedTreeController : MonoBehaviour {


    public int depth = 3;
    public string axiom = "F";
    public string varFRewriting = "F[-&<F][<++&F]||F[--&>F][+&F]";
    public string varXRewriting = "";
    public Material leafMat;
    public GameObject leaf;

    //modify the variable name
    public float originLength = 1.0f;
    public float originRadius = 0.6f;
    public int originGrowthAngle = 25;
    public float originScaleLength = 0.9f;
    public float originScaleRadius = 0.99f;
    public float xFrom = -2.0f;
    public float xTo = 2.0f;
    public float zFrom = -2.0f;
    public float zTo = 2.0f;
    public float yFrom = 0.0f;
    public float yTo = 7.5f;

    public Material mat;

    private Vector3 lastBranchPosition;
    public string[] wholeAlphabet;

    private float xzAngle; //for roll left and right
    private float xyAngle; //for up and down(forward and backward)
    private float yzAngle; //for left and righ
    private Vector3 lastAngle;//.x .y .z for angle rotate along with X-Y-Z
    private Stack savedPositions;
    private Stack savedAngles;
    private Stack savedLength;
    private Stack savedRadius;
    //private float timer = 0.0f;

    public static PrimitiveType branchType = PrimitiveType.Cylinder;
    private Quaternion rotationAngle;
    private Vector3 fromDirection = new Vector3(0, 1, 0);
    private Vector3 toDirection;
    //private GameObject[] branches;
    private GameObject branchTree;
    private GameObject branch;
    private GameObject leaves;
    //private int branchNum = 0;
    private Vector3 branchOffset;
    public float radiusThreshold = 0.05f;
    private Mesh leafMesh;

	// Use this for initialization
	void Start () {

        savedPositions = new Stack();
        savedAngles = new Stack();
        savedLength = new Stack();
        savedRadius = new Stack();
        //branches = new GameObject[depth + 1];
        lastBranchPosition = new Vector3(0f, 0f, 0f);
        lastAngle = new Vector3(0f, 0f, 0f);
        xzAngle = 0;
        xyAngle = 0;
        yzAngle = 0;
        //radiusThreshold = originRadius * Mathf.Pow(originScaleRadius, depth - 1); // radiusThreshold
        leafMesh = leaf.GetComponent<MeshFilter>().mesh;
        getWholeAlphabet(depth);
        /*
        for (int n = 0; n <= depth; n++)
        {
            branches[n] = new GameObject();
            //Debug.Log(wholeAlphabet[n]);
            createTree(branches[n], n, n.ToString());
            branches[n].name = "branch " + n.ToString();
        }
        */
        // Debug.Log(branches.Length);
        branchTree = new GameObject();
        createTree(branchTree, depth, depth.ToString());
        branchTree.name = "branchTree";
        branchTree.transform.parent = this.transform;
        for (int n = 0; n <= depth; n++)
        {
            //branches[n].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void createTree(GameObject branchRoot, int dep, string ss)
    {
        float Length = originLength;
        float Radius = originRadius;
        float growthAngle = originGrowthAngle;
        float scaleLength = originScaleLength;
        float scaleRadius = originScaleRadius;
        //lastBranchPosition = new Vector3(0, 0, -10 + dep * 5); //should add interafce
        lastBranchPosition = new Vector3(0, 0, 0);
        for (int k = 0; k < wholeAlphabet[dep].Length; k++)
        {
            Vector3 position = new Vector3();
            string symbol = wholeAlphabet[dep][k].ToString();
            switch (symbol)
            {
                case "F":
                    // Moving
                    position.x = lastBranchPosition.x + Length * (Mathf.Sin(lastAngle.z) * Mathf.Cos(lastAngle.x)
                        - Mathf.Cos(lastAngle.z) * Mathf.Sin(lastAngle.y) * Mathf.Sin(lastAngle.x));
                    position.y = lastBranchPosition.y + Length * (Mathf.Cos(lastAngle.z) * Mathf.Cos(lastAngle.x)
                        + Mathf.Sin(lastAngle.z) * Mathf.Sin(lastAngle.y) * Mathf.Sin(lastAngle.x));
                    position.z = lastBranchPosition.z + Length * Mathf.Cos(lastAngle.y) * Mathf.Sin(lastAngle.x);
                    //clamp branches position
                    position.x = clampFloat(position.x, xFrom, xTo);
                    position.y = clampFloat(position.y, yFrom, yTo);
                    position.z = clampFloat(position.z, zFrom, zTo);
                    Debug.Log(position);
                    
                    

                    toDirection = position - lastBranchPosition;
                    //Debug.Log("toDirection" + toDirection);
                    branchOffset.x = toDirection.x / 2;
                    branchOffset.y = toDirection.y / 2;
                    branchOffset.z = toDirection.z / 2;
                    rotationAngle = Quaternion.FromToRotation(fromDirection, toDirection);
                    branch = GameObject.CreatePrimitive(branchType);
                    branch.transform.position = lastBranchPosition;
                    branch.transform.localScale = new Vector3(Radius, toDirection.magnitude / 2, Radius);
                    branch.transform.Translate(branchOffset);
                    branch.transform.rotation = rotationAngle;
                    branch.GetComponent<Renderer>().material = mat;
                    branch.name = ss;
                    branch.transform.parent = branchRoot.transform;
                    // Current branch is now the previous one


                    //create the leaf
                    if (Radius <= radiusThreshold)
                    {
                        leaves = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        //leaves.GetComponent<MeshFilter>().mesh = leafMesh;
                        leaves.GetComponent<Renderer>().material = leafMat;
                        leaves.transform.position = lastBranchPosition;
                        leaves.transform.parent = this.transform;
                    }
                    
                

                    lastBranchPosition = position;
                    break;

                // + and - for turn right and left (yzangle: rotate along X-axis)
                case "+":
                    yzAngle += growthAngle;
                    Debug.Log(yzAngle);
                    lastAngle.x = yzAngle * (Mathf.PI / 180);
                    break;
                case "-":
                    yzAngle -= growthAngle;
                    Debug.Log(yzAngle);
                    lastAngle.x = yzAngle * (Mathf.PI / 180);
                    break;
                // & and ^ for up and down(backward and forward)(xyAngle: rotate along Z-axis)
                case "&":
                    xyAngle += growthAngle;
                    lastAngle.z = xyAngle * (Mathf.PI / 180);
                    break;
                case "^":
                    xyAngle -= growthAngle;
                    lastAngle.z = xyAngle * (Mathf.PI / 180);
                    break;
                // < and > for roll left and right(xzAngle: rotate along Y-axis)
                case "<":
                    xzAngle += growthAngle;
                    lastAngle.y = xzAngle * (Mathf.PI / 180);
                    break;
                case ">":
                    xzAngle -= growthAngle;
                    lastAngle.y = xzAngle * (Mathf.PI / 180);
                    break;
                case "|":
                    xzAngle += 180;
                    xzAngle = xzAngle % 360;
                    lastAngle.y = xzAngle * (Mathf.PI / 180);
                    break;
                case "[":
                    // Save angles and positions
                    savedPositions.Push(lastBranchPosition);
                    //Debug.Log ("push"+lastAngle.x +"-"+lastAngle.y+"-"+lastAngle.z);
                    savedAngles.Push(yzAngle);
                    savedAngles.Push(xzAngle);
                    savedAngles.Push(xyAngle);
                    savedAngles.Push(lastAngle);
                    savedLength.Push(Length);
                    savedRadius.Push(Radius);
                    Length *= scaleLength;
                    Radius *= scaleRadius;
                    break;
                case "]":
                    lastBranchPosition = (Vector3)savedPositions.Pop();
                    lastAngle = (Vector3)savedAngles.Pop();
                    xyAngle = (float)savedAngles.Pop();
                    xzAngle = (float)savedAngles.Pop();
                    yzAngle = (float)savedAngles.Pop();
                    Length = (float)savedLength.Pop();
                    Radius = (float)savedRadius.Pop() * 0.95f;
                    //Debug.Log ("pop" + lastAngle.x +"-"+lastAngle.y+"-"+lastAngle.z);
                    break;
            }
        }
    }

    public float clampFloat(float value, float from, float to)
    {
        Debug.Log("enter the clamp function");
        Debug.Log("value: "+value);
        if (value >= to)
        {
            value = to;
            Debug.Log("larger than to: "+value);
        }
        else if (value <= from)
        {
            value = from;
            Debug.Log("less than from: "+value);
        }
        return value;
    }

    public void getWholeAlphabet(int depth)
    {
        wholeAlphabet = new string[depth + 1];
        wholeAlphabet[0] = axiom;
        string preAlphabet = "";
        for (int i = 0; i < wholeAlphabet[0].Length; i++)
        {
            preAlphabet = preAlphabet + wholeAlphabet[0][i];
        }
        string currentAlphabet;
        for (int i = 1; i < depth + 1; i++)
        {
            currentAlphabet = "";
            for (int j = 0; j < preAlphabet.Length; j++)
            {
                string symbol = preAlphabet[j].ToString();
                switch (symbol)
                {
                    case "F":
                        for (int n = 0; n < varFRewriting.Length; n++)
                        {
                            currentAlphabet = currentAlphabet + varFRewriting[n];
                        }
                        break;
                    case "[":
                        currentAlphabet = currentAlphabet + "[";
                        break;
                    case "]":
                        currentAlphabet = currentAlphabet + "]";
                        break;
                    case "+":
                        currentAlphabet = currentAlphabet + "+";
                        break;
                    case "-":
                        currentAlphabet = currentAlphabet + "-";
                        break;
                    case "&":
                        currentAlphabet = currentAlphabet + "&";
                        break;
                    case "^":
                        currentAlphabet = currentAlphabet + "^";
                        break;
                    case "<":
                        currentAlphabet = currentAlphabet + "<";
                        break;
                    case ">":
                        currentAlphabet = currentAlphabet + ">";
                        break;
                    case "|":
                        currentAlphabet = currentAlphabet + "|";
                        break;
                }
            }
            wholeAlphabet[i] = currentAlphabet;
            preAlphabet = currentAlphabet;
        }
    }
}
