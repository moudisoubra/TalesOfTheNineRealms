using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class CircleCreate : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;
    [Range(0, 3)]
    public float height = 0;
    [Range(0, 5)]
    public float xradius = 5;
    [Range(0, 5)]
    public float yradius = 5;
    [Range(0, 10)]
    public float multiplier = 10;
    [Range(0, 0.5f)]
    public float speed;
    [Range(0, 50)]
    public int positionAngle;

    public Vector3 temp;

    public float x;
    public float y;
    public float z;


    public LineRenderer line;

    public bool reCreate;
    public bool position;

    public GameObject Camera;
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
    }

    private void Update()
    {

        if (reCreate)
        {
            CreatePoints();
        }

        if (position)
        {
            SpawnGrabage();
            //position = false;
        }

    }

    void CreatePoints()
    {

        float angle = Random.Range(0f, 360f);

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, z + height));
            temp = new Vector3(x, y, z + height);
            angle += (360f / segments);
        }
        reCreate = false;

    }


    void PositionCamera(int index)
    {
        Camera.transform.position = line.GetPosition(index);
    }

    void SpawnGrabage()
    {
        float x;
        float z;

        float angle = Random.Range(0f, 360f);
        Debug.Log(line.GetPosition(1));
        x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius * multiplier;
        z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius * multiplier;

        Camera.transform.position = new Vector3(x, 0, z);
        Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(x, 0, z), Quaternion.identity);
    }
}