using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MovieManagerTest : MonoBehaviour
{
    VideoPlayer vp;
    ParticleSystem ps;
    Material rend;
    AudioSource aus;
    //public InputField infe;

    // NOTE: make this a 'static' float so we can access it from any other script.
    public float[] spectrumData = new float[512];

    // Start is called before the first frame update
    void Start()
    {
        vp = GetComponent<VideoPlayer>();//.material.mainTexture as MovieTexture;
        ps = GetComponent<ParticleSystem>();
        rend = GetComponent<ParticleSystemRenderer>().material;
        aus = GetComponent<AudioSource>();

        rend.SetFloat("_ImageSize", Screen.width/2);

        //infe.text;
        //if(infe != null) infe.onEndEdit.AddListener(delegate { changeVid(); });
        vp.source = VideoSource.Url;
        vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Halo Infinite - E3 2018 - Announcement Trailer.mp4");
        vp.Play();
    }

    void changeVid()
    {
        //vp.Stop();
        //vp.source = VideoSource.Url;
        //vp.url = infe.text;
        //vp.Play();
    }

    // Update is called once per frame
    void Update()
    {

        aus.GetSpectrumData(spectrumData, 0, FFTWindow.Hanning);

        int numPartitions = 2;
        float[] aveMag = new float[numPartitions];
        float partitionIndx = 0;
        int numDisplayedBins = 512 / 2;

        for (int i = 0; i < numDisplayedBins; i++)
        {
            if (i < numDisplayedBins * (partitionIndx + 1) / numPartitions)
            {
                aveMag[(int)partitionIndx] += spectrumData[i] / (512 / numPartitions);
            }
            else
            {
                partitionIndx++;
                i--;
            }
        }

        for (int i = 0; i < numPartitions; i++)
        {
            aveMag[i] = (float)0.5 + aveMag[i] * 100;
            if (aveMag[i] > 100)
            {
                aveMag[i] = 100;
            }
        }

        float mag = aveMag[0];

        var emis = ps.emission;
        emis.rateOverTime = mag * mag * mag * 100;
        var main = ps.main;
        main.startSize = mag * mag * mag * Screen.width / 100;
        main.startSpeed = mag * mag * mag * 0.5f;
        var noise = ps.noise;
        noise.strengthX = Input.mousePosition.x / (Screen.width * 10);
        noise.strengthY = Input.mousePosition.y / (Screen.height * 10);
    }
}
