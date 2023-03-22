using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
    public GameObject objectToDetect;
    public GameObject loja;
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
            Texto.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                loja.SetActive(true);
            }
        }
        else
        {
            Texto.SetActive(false);
            loja.SetActive(false);
        }
    }
}
