using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour {

    public bool randomChord = false; 
    private int position = 0;

    private float base_freq = 220;


    private float[] pitch = new float[3];
    private float volume = 1;

    public GameObject source0;
    public GameObject source1;
    public GameObject source2;

    public AudioMixer mixer;

    public GameObject collide_kick;

    private GameObject[] sources = new GameObject[3];

    public GameObject controler;

    public bool play = false;

    private int type = -1;

    private float low_pas_cutoff = 1000;


    public GameObject bassSource;
    public float bassTime = 0.5f;
    int currBassNote = 0;
    float bassPlayedAgo = 0f;

    static float[] base_signal = new float[44100];

    public void SetPlay(bool play)
    {
        this.play = play;
        if (play)
        {
            source0.GetComponent<AudioSource>().Play();
            source1.GetComponent<AudioSource>().Play();
            source2.GetComponent<AudioSource>().Play();
        }
        else
        {
            source0.GetComponent<AudioSource>().Stop();
            source1.GetComponent<AudioSource>().Stop();
            source2.GetComponent<AudioSource>().Stop();
        }
    }

    float Spike(float playedAgo, float dur)
    {
        //return 1f;
        return Mathf.Max(0f, 1f - playedAgo / dur);
    }

    void PlayBassNote()
    {
        bassPlayedAgo += Time.deltaTime;
        bassSource.GetComponent<AudioSource>().volume += (volume - bassSource.GetComponent<AudioSource>().volume) * Time.deltaTime;
        bassSource.GetComponent<AudioSource>().volume = bassSource.GetComponent<AudioSource>().volume;
        bassSource.GetComponent<AudioLowPassFilter>().cutoffFrequency += (low_pas_cutoff - bassSource.GetComponent<AudioLowPassFilter>().cutoffFrequency) * Time.deltaTime;
        bassSource.GetComponent<AudioLowPassFilter>().cutoffFrequency = bassSource.GetComponent<AudioLowPassFilter>().cutoffFrequency;
        if (bassPlayedAgo >= bassTime)
        {
            currBassNote = currBassNote % 3;
            bassSource.GetComponent<AudioSource>().Stop();
            bassSource.GetComponent<AudioSource>().pitch = pitch[currBassNote] * 2f;
            bassSource.GetComponent<AudioSource>().Play();
            bassPlayedAgo = 0f;
            currBassNote++;
        }
    }

    private void FixedUpdate()
    {
        if (play)
        {
            if (controler.GetComponent<rotor>().ChordType != type)
            {
                type = controler.GetComponent<rotor>().ChordType;
                if (randomChord && type != -1)
                {
                    type = Random.Range(0, 4);
                }
                switch (type)
                {
                    case 0:
                        pitch[0] = 6f / 5f;
                        pitch[1] = 3f / 2f;
                        pitch[2] = 2;
                        volume = 0.125f;
                        low_pas_cutoff = 1000;
                        break;
                    case 1:
                        pitch[0] = 4f / 3f;
                        pitch[1] = 8f / 5f;
                        pitch[2] = 2;
                        volume = 0.125f;
                        low_pas_cutoff = 1000;
                        break;
                    case 2:
                        pitch[0] = 9f / 8f;
                        pitch[1] = 3f / 2f;
                        pitch[2] = 16f / 9f;
                        volume = 0.25f;
                        low_pas_cutoff = 2000;
                        break;
                    case 3:
                        pitch[0] = 9f / 8f;
                        pitch[1] = 4f / 3f;
                        pitch[2] = 16f / 9f;
                        volume = 0.125f;
                        low_pas_cutoff = 1000;
                        break;
                    case -7:
                        pitch[0] = 6f / 5f;
                        pitch[1] = 3f / 2f;
                        pitch[2] = 2;
                        volume = 0.25f;
                        low_pas_cutoff = 2000;
                        break;
                }
                for (int i = 0; i < sources.Length; i++)
                {
                    sources[i].GetComponent<AudioSource>().pitch = pitch[i];
                }
                }
            if (type == -1)
            {
                float noRotateCoef = controler.GetComponent<loose>().no_rotate_time;
                noRotateCoef = Mathf.Min(1f, Mathf.Max(0f, noRotateCoef - controler.GetComponent<rotor>().GetNoRotateTime()) / noRotateCoef);
                volume = Mathf.Max(0.2f, (Mathf.Max(controler.GetComponent<rotor>().GetVelMagnitude() / 60f, 1) / 4f) * noRotateCoef);
                low_pas_cutoff = Mathf.Max(500, 5000 * noRotateCoef);
            }
            PlayBassNote();
            mixer.SetFloat("pitch", 1f + 0.01f * Mathf.Min(1f, controler.GetComponent<rotor>().GetVelMagnitude() / 100f));
            for(int i=0;i < sources.Length;i++){
                sources[i].GetComponent<AudioSource>().volume += (volume - sources[i].GetComponent<AudioSource>().volume) * Time.deltaTime;
                sources[i].GetComponent<AudioLowPassFilter>().cutoffFrequency += (low_pas_cutoff - sources[i].GetComponent<AudioLowPassFilter>().cutoffFrequency) * Time.deltaTime;
                sources[i].GetComponent<AudioLowPassFilter>().lowpassResonanceQ = 1f + (1f - (Mathf.Abs(500f - low_pas_cutoff) / 4500f));
            }
        }
    }

    void Start () {
        sources[0] = source0;
        sources[1] = source1;
        sources[2] = source2;
    }

    public void PlayCollisionKick(float intensity)
    {
        AudioSource source = collide_kick.GetComponent<AudioSource>();
        source.volume = intensity;
        source.Play();
    }

    private float GenerateSquare(float freq, int i)
    {
        int square_len = Mathf.CeilToInt(44100f / freq);
        return Mathf.Pow(-1, (i / square_len) % 2);
    }


    private float GenerateSine(float freq, int i)
    {
        return Mathf.Sin(2 * Mathf.PI * freq * ((float)i) / 44100f);
    }

    private float[] AddSignals(params float[][] signals)
    {
        float[] output = new float[44100];
        for (int i = 0; i < 44100; i++)
        {
            foreach(float[] sig in signals)
            {
                output[i] += sig[i];
            }
        }
        return output;
    }

    private void OnAudioRead(float[] data)
    {
        for(int i=0;i<data.Length;i++)
        {
            data[i] += 0.2f * base_signal[position % 44100];
            position++;
        }
    }

    private void OnPositionChange(int newPosition)
    {
        position = newPosition;
    }
}
