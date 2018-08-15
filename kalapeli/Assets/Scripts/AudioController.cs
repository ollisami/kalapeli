using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public static AudioController instance = null;
    private AudioSource audioSource;
    public AudioClip[] kalaTuli;
    public AudioClip[] moottori;
    public AudioClip[] plop;
    public AudioClip[] ankkuri;
    public AudioClip[] kelaRykasy;
    public AudioClip[] kelaus;
    public AudioClip[] karkas;
    public AudioClip[] ilta;
    public AudioClip[] kalaNostettu;
    public AudioClip[] isoKala;

    // Use this for initialization
    void Start () {

        if (instance == null)
            instance = this;

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void StopPlaying()
    {
        audioSource.Stop();
    }
    
    public void PlaySound(string key)
    {
        if (audioSource.isPlaying) return;
        if (key.Equals("kalaTuli"))
        {
            audioSource.clip = kalaTuli[Random.Range(0, kalaTuli.Length)];
            audioSource.Play(0);
        }

        if (key.Equals("moottori"))
        {
            audioSource.clip = moottori[Random.Range(0, moottori.Length)];
            audioSource.Play(0);
        }

        if (key.Equals("plop"))
        {
            audioSource.clip = plop[Random.Range(0, plop.Length)];
            audioSource.Play(0);
        }

        if (key.Equals("ankkuri"))
        {
            audioSource.clip = ankkuri[Random.Range(0, ankkuri.Length)];
            audioSource.Play(0);
        }

        if (key.Equals("kelaRykasy"))
        {
            audioSource.clip = kelaRykasy[Random.Range(0, kelaRykasy.Length)];
            audioSource.Play(0);
        }

        if (key.Equals("kelaus"))
        {
            audioSource.clip = kelaus[Random.Range(0, kelaus.Length)];
            audioSource.Play(0);
        }

        if (key.Equals("karkas"))
        {
            audioSource.clip = karkas[Random.Range(0, karkas.Length)];
            audioSource.Play(0);
        }

        if (key.Equals("ilta"))
        {
            audioSource.clip = ilta[Random.Range(0, ilta.Length)];
            audioSource.Play(0);
        }

        if (key.Equals("kalaNostettu"))
        {
            audioSource.clip = kalaNostettu[Random.Range(0, kalaNostettu.Length)];
            audioSource.Play(0);
        }

        if (key.Equals("isoKala"))
        {
            audioSource.clip = isoKala[Random.Range(0, isoKala.Length)];
            audioSource.Play(0);
        }
    }
}
