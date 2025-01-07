using UnityEngine;

public class ScrollingText : MonoBehaviour
{
    [SerializeField] private Vector3 scrollDirection = new Vector3(0, -1, 0);
    [SerializeField] private float scrollSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + scrollDirection * scrollSpeed * Time.deltaTime;

        Debug.Log(transform.position);
    }
}
