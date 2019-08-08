using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MicrophoneInput : MonoBehaviour
{
    [SerializeField]
    private float triggerLevel;

    [SerializeField]
    private float intensity = 40;

    [SerializeField]
    private float release = 5;

    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private Sprite microphoneEnable;

    [SerializeField]
    private Sprite microphoneDisable;

    [SerializeField]
    private Image spriteRenderer;

    private int dec = 128;
    private float[] waveData;

    private int micPosition;

    private AudioClip clipRecord;

    private bool enable = false;

    IEnumerator Start()
    {
        waveData = new float[dec];
        clipRecord = Microphone.Start(Microphone.devices.First(), true, 999, 44100);
        yield return new WaitForSeconds(1f);
        enable = true;
    }

    void Update()
    {
        if(enable)
        {
            int micPosition = Microphone.GetPosition(null) - (dec + 1); // null means the first microphone
            clipRecord.GetData(waveData, micPosition);

            // Getting a peak on the last 128 samples
            float levelMax = 0;
            for (int i = 0; i < dec; i++)
            {
                float wavePeak = waveData[i] * waveData[i];
                if (levelMax < wavePeak)
                {
                    levelMax = wavePeak;
                }
            }

            if(levelMax > triggerLevel)
            {
                mixer.GetFloat("Volume", out float current);
                mixer.SetFloat("Volume", Mathf.Lerp(current, levelMax * intensity - 100, Time.deltaTime * release));
            }
            else
            {
                mixer.GetFloat("Volume", out float current);
                mixer.SetFloat("Volume", Mathf.Lerp(current, current - 5, Time.deltaTime * release));
            }
        }
        else
        {
            mixer.SetFloat("Volume", 0);
        }
    }

    public void ToggleActivation()
    {
        enable = !enable;
        spriteRenderer.sprite = enable ? microphoneEnable : microphoneDisable;
    }
}
