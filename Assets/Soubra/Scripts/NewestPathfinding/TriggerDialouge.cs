using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TriggerDialouge : MonoBehaviour
{
    public OdinWalkController owcScript;
    public Dialogue dScript;
    public AnnaTweenToPosition attpScript;
    public GameObject tweenTo;
    public GameObject lookAt;

    public GameObject odinTarget;
    public GameObject odinLookAt;
    public Image transitionImage;
    public bool setup = false;
    public bool firstTime = false;
    public float transitionSpeed = 2f;
    public SetupBattle sbScript;
    public NewDialogueNpc ndnScript;
    // Start is called before the first frame update
    void Start()
    {
        dScript = FindObjectOfType<Dialogue>();
        ndnScript = GetComponent<NewDialogueNpc>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dScript.done && !setup && firstTime)
        {
            transitionImage.color = Color.Lerp(transitionImage.color, Color.black, transitionSpeed * Time.deltaTime);
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
            this.GetComponent<BoxCollider>().enabled = false;
            this.enabled = false;
        }
    }

    public void ClearPanel()
    {
        transitionImage.color = Color.Lerp(transitionImage.color, Color.clear, transitionSpeed * Time.deltaTime);
    }

    public void BlackPanel()
    {
        transitionImage.color = Color.Lerp(transitionImage.color, Color.black, transitionSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !setup)
        {
            firstTime = true;
            owcScript.anim.SetBool("Run", false);
            owcScript.enabled = false;
            attpScript.moveToThisPosition = tweenTo;
            attpScript.lookAtThis = lookAt;
            ndnScript.TriggerThis();
        }
    }
}
