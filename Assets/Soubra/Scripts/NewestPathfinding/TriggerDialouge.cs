using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TriggerDialouge : MonoBehaviour
{
    public Dialogue dScript;
    public AnnaTweenToPosition attpScript;
    public GameObject tweenTo;
    public GameObject lookAt;

    public GameObject odinTarget;
    public GameObject odinLookAt;
    public Image transitionImage;
    public bool setup = false;
    public float transitionSpeed = 2f;
    public SetupBattle sbScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dScript.done && !setup)
        {
            transitionImage.color = Color.Lerp(transitionImage.color, Color.black, transitionSpeed * Time.deltaTime); //new Vector4(Color.black.r, Color.black.g, Color.black.b, Mathf.Lerp(0, 255, transitionSpeed * Time.deltaTime));
            attpScript.moveToThisPosition = tweenTo;
            attpScript.lookAtThis = lookAt;
            if (transitionImage.color == Color.black)
            {
                setup = true;
            }
        }

        if (setup)
        {
            sbScript.start = true;
        }
    }

    public void ClearPanel()
    {
        transitionImage.color = Color.Lerp(transitionImage.color, Color.clear, transitionSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dScript.TalkToNpc();
            attpScript.moveToThisPosition = tweenTo;
            attpScript.lookAtThis = lookAt;
        }
    }
}
