using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolFetcher : MonoBehaviour
{
    AudioSource audiosauce;
    // Start is called before the first frame update
    void Start()
    {
        audiosauce = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audiosauce.volume = VolumeController.vol;
    }
}
