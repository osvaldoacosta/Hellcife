using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveNumberController : MonoBehaviour
{
    public TextMeshProUGUI waveTextMesh;
    public int waveNumber= 0;
    // Start is called before the first frame update
    void Start()
    {
        waveTextMesh = this.GetComponent<TextMeshProUGUI>();
        GameEventManager.instance.onStartWave+= onStartWave;
    }
    public void onStartWave(){
        waveNumber++;
        waveTextMesh.text = "" + waveNumber;
    }
}
