using UnityEngine;

namespace Tools.UI
{
    public class UiArrowsTransformMover : MonoBehaviour
    {
        public void MoveUp()
        {
            transform.localPosition += new Vector3(0, 1, 0);
        }

        public void MoveDown()
        {
            transform.localPosition += new Vector3(0, -1, 0);
        }
    }
}