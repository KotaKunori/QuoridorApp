using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunting
{
    public class Player1Kojila : MonoBehaviour
    {
        public View view;

        private bool moveFlag = false;
        private Vector3 currentPosition;
        public Vector3 startPosition;
        public List<Vector3> targetPositions;
        public List<Quaternion> targetRotations;
        Animator animator;

        float speed = 5.0f;

        // Start is called before the first frame update
        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            targetPositions = new List<Vector3>();
            targetRotations = new List<Quaternion>();
        }

        public void SetTargetPosition(MoveActions action, Position initialPosition)
        {
            startPosition = new Vector3((initialPosition.y - 1) / 2 * view.getScale(), 0, (initialPosition.x - 1) / 2 * view.getScale());
            targetPositions.Add(startPosition);
            switch (action)
            {
                case MoveActions.right:
                    targetRotations.Add(Quaternion.Euler(0, 0, 0));
                    break;
                case MoveActions.left:
                    targetRotations.Add(Quaternion.Euler(0, 180, 0));
                    break;
                case MoveActions.down:
                    targetRotations.Add(Quaternion.Euler(0, 90, 0));
                    break;
                case MoveActions.up:
                    targetRotations.Add(Quaternion.Euler(0, -90, 0));
                    break;
                default:
                    return;
            }
            moveFlag = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (moveFlag)
            {
                currentPosition = gameObject.transform.position;
                gameObject.transform.position = Vector3.MoveTowards(currentPosition, targetPositions[0], speed * Time.deltaTime);
                gameObject.transform.rotation = targetRotations[0];
                if (view.getCameraState() == CameraState.player1View)
                {
                    Camera.main.transform.position = gameObject.transform.position + new Vector3(0, 2, 0);
                    Camera.main.transform.rotation = gameObject.transform.rotation;
                }

                animator.SetBool("walking", true);

                if (currentPosition == targetPositions[0])
                {
                    targetPositions.RemoveAt(0);
                    targetRotations.RemoveAt(0);
                    if (targetPositions.Count == 0)
                    {
                        moveFlag = false;
                        animator.SetBool("walking", false);
                    }
                }
            }
        }
    }

}