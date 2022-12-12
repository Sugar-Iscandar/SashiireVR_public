using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class InstructionManager : MonoBehaviour
    {
        [SerializeField] GameObject prefabArrow;
        [SerializeField] GameObject prefabRightController;
        [SerializeField] Vector3[] rightControllerInstructionShowPoints;
        [SerializeField] Vector3[] rightControllerInstructionRotate;
        int indexOfRightControllerShowPoint;
        [SerializeField] Vector3[] arrowInstructionShowPoints;
        [SerializeField] Vector3[] arrowInstructionRotate;
        int indexOfArrowShowPoint;
        GameObject currentRightController;
        GameObject currentArrow;

        public void Initialize()
        {
            indexOfRightControllerShowPoint = 0;
            indexOfArrowShowPoint = 0;
            ShowRightControllerInstruction();
        }

        public void ShowRightControllerInstruction()
        {
            if (currentRightController != null)
            {
                Destroy(currentRightController);
            }
            currentRightController = Instantiate(
                prefabRightController,
                rightControllerInstructionShowPoints[indexOfRightControllerShowPoint],
                Quaternion.Euler(rightControllerInstructionRotate[indexOfRightControllerShowPoint])
            );
            indexOfRightControllerShowPoint++;
        }
        public void FinishRightControllerInstruction()
        {
            Destroy(currentRightController);
        }

        public void ShowAllowInstruction()
        {
            if (currentArrow != null)
            {
                Destroy(currentArrow);
            }
            currentArrow = Instantiate(
                prefabArrow,
                arrowInstructionShowPoints[indexOfArrowShowPoint],
                Quaternion.Euler(arrowInstructionRotate[indexOfArrowShowPoint])
            );
            indexOfArrowShowPoint++;
        }

        public void FinishAllowInstruction()
        {
            Destroy(currentArrow);
        }
    }
}
