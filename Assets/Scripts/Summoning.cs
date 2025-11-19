using UnityEngine;

public class Summoning : MonoBehaviour
{
    public GameObject resurrectAnimation;

    void Update()
    {
        Vector3 screenPos = Input.mousePosition;

        screenPos.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        resurrectAnimation.transform.position = worldPos;

        if (Input.GetMouseButton(1))
        {
            resurrectAnimation.SetActive(true);
        }
        else
        {
            resurrectAnimation.SetActive(false);
        }
    }
}
