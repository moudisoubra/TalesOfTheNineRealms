using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    public float speed;
    [Range(0,1f)]
    public float distanceToGround;
    public GameObject playerCamera;
    public Transform playerFoot;
    public Animator playerAnimator;
    public LayerMask lm;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        float moveSides = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float movefront = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(moveSides, 0, movefront);

        playerAnimator.SetFloat("Walk", movefront);
    }

    private void OnAnimatorIK(int layerIndex)
    {

        if (playerAnimator)
        {
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);



            RaycastHit hit;
            Ray ray = new Ray(playerAnimator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);

            if (Physics.Raycast(ray, out hit, distanceToGround + 1f, lm))
            {
                if(hit.transform.CompareTag("Walkable"))
                {
                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceToGround;
                    playerAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    playerAnimator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }

            playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);
            ray = new Ray(playerAnimator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);

            if (Physics.Raycast(ray, out hit, distanceToGround + 1f, lm))
            {
                if (hit.transform.CompareTag("Walkable"))
                {
                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceToGround;
                    playerAnimator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    playerAnimator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }
        }
    }
}
