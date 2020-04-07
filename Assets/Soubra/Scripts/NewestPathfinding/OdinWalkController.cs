using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinWalkController : MonoBehaviour
{
    public Material transparentMaterial;
    public Material[] materials;
    public float speed = 5f;
    public float rotationSpeed = 100f;
    public Animator anim;
    public bool noWalkie;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetVector("_playerPosition", transform.position);
        }

        transparentMaterial.SetVector("_playerPosition", transform.position);

        if (!noWalkie)
        {
            float translation = Input.GetAxis("Vertical") * speed;
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;
            transform.Translate(0, 0, translation);
            transform.Rotate(0, rotation, 0);

            if (translation > 0)
            {
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }
        }
    }
}
