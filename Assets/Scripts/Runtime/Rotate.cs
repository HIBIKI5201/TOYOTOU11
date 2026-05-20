using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotateSpeed = new Vector3(0, 350, 0);
    public float speed = 10f;

    private void Update()
    {
        transform.Rotate(rotateSpeed * speed * Time.deltaTime);
    }
}
