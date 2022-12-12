using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Girls
{
    public class GirlAnimationController : MonoBehaviour
    {
        Animator animator;

        public void Initialize()
        {
            animator = GetComponent<Animator>();
        }

        public void OnWakeUp()
        {
            animator.SetTrigger("isWakeUp");
        }
    }
}
