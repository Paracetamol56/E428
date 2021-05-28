using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Prefab : MonoBehaviour
{
    // Parameters
    [SerializeField]
    private AudioClip Clip;
    [SerializeField]
    private float Volume = 1;

    private AudioSource Source;
    // Start is called before the first frame update
    void Start()
    {
        Source = GetComponent<AudioSource>();
        if (Clip != null)
        {
            // Set audio source clip to parametter
            Source.clip = Clip;
            // Play Once
            Source.volume = Volume;
            Source.PlayOneShot(Clip);
            // Prepare deletion
            StartCoroutine(Destroy_After_Playing());
        }
        else
        {
            Debug.Log("Clip Not Set");
        }
    }
    IEnumerator Destroy_After_Playing()
    {
        yield return new WaitForSeconds(Clip.length + 0.1f);
        Destroy(gameObject);
    }
    
}
