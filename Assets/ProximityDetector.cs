using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
    public GameObject objectToDetect;
    public GameObject Texto;
    public float proximityThreshold = 2f;

    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, objectToDetect.transform.position);
        if (distance < proximityThreshold)
        {
            renderer.material.color = Color.red;
            Texto.SetActive(true);
        }
        else
        {
            renderer.material.color = Color.white;
            Texto.SetActive(false);
        }
    }
}
