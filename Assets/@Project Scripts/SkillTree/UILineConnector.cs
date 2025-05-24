using UnityEngine;
using UnityEngine.UI;

public class UILineConnector : MonoBehaviour
{
    public RectTransform from;
    public RectTransform to;
    public RectTransform lineRect;

    void Update()
    {
        if (from == null || to == null || lineRect == null) return;

        Vector3 start = from.position;
        Vector3 end = to.position;
        Vector3 direction = end - start;
        float distance = direction.magnitude;

        lineRect.sizeDelta = new Vector2(distance, lineRect.sizeDelta.y);
        lineRect.position = (start + end) / 2f;

        lineRect.rotation = Quaternion.FromToRotation(Vector3.right, direction);
        if (direction.y < 0)
            lineRect.rotation *= Quaternion.Euler(0, 0, 180);
    }
}
