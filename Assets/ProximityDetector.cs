using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
    public GameObject objectToDetect;
    public GameObject Texto;
    public GameObject uiDaLoja;
    public float proximityThreshold = 2f;

    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, objectToDetect.transform.position);
        if (distance < proximityThreshold)
        {
            renderer.material.color = Color.red;
            
            if (uiDaLoja.activeSelf) Texto.SetActive(false);
            else Texto.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.L))
            {
                uiDaLoja.SetActive(true);
                
            }
        }
        else
        {
            renderer.material.color = Color.white;
            Texto.SetActive(false);
        }
    }
}
