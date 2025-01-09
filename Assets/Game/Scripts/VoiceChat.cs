using Mirror;
using Mirror.BouncyCastle.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VoiceChat : NetworkBehaviour
{
    private const int SAMPLE_RATE = 44100;
    private const int RECORD_DURATION = 1;
    private bool isRecording = false;
    private AudioSource _audioSource;
    private AudioClip _audioClip;
    private int _lastSamplePosition;
    private Coroutine _lastAudioSend;


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (!isLocalPlayer) return;
        _audioClip = Microphone.Start(null, false, RECORD_DURATION * 310, SAMPLE_RATE);
    }

    private void Update()
    {   
        if (!isLocalPlayer) return;
        
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (_lastAudioSend != null)
            {
                StopCoroutine(_lastAudioSend); 
                _lastAudioSend = null;
            }
            else
            {
                StartRecord();
            }
        }

        if(Input.GetKeyUp(KeyCode.E))
        {
            _lastAudioSend = StartCoroutine(StopRecord());
        }
    }

    private void StartRecord()
    {
        isRecording = true;
        _lastSamplePosition = Microphone.GetPosition(null);
        StartCoroutine(SendAudioPeriod());
    }

    private IEnumerator StopRecord() 
    {
        yield return new WaitForSeconds(RECORD_DURATION / 2f);
        isRecording = false;
        _lastAudioSend= null;
    }

    private IEnumerator SendAudioPeriod()
    {
        while(isRecording)
        {
            SendAudio();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void SendAudio()
    {
        Debug.Log(_audioClip is null);
        int currentSamplePos = Microphone.GetPosition(null);
        int SampleCount = currentSamplePos - _lastSamplePosition;
        if (SampleCount < 0) 
        {
            SampleCount += _audioClip.samples;
        }

        float[] sample = new float[SampleCount];
        
        if (sample.Length > 0)
        {
            _audioClip.GetData(sample, _lastSamplePosition);
            _lastSamplePosition = currentSamplePos;

            byte[] byteArray = new byte[sample.Length * 4];
            Buffer.BlockCopy(sample, 0, byteArray, 0, byteArray.Length);

            CmdSendAudio(byteArray);
        }
    }

    [Command]
    private void CmdSendAudio(byte[] audioData)
    {
        RpcReceiveData(audioData);
    }

    [ClientRpc(includeOwner =false)]
    private void RpcReceiveData(byte[] audioData)
    {
        float[] receivedData = new float[audioData.Length / 4];
        Buffer.BlockCopy(audioData,0, receivedData, 0, audioData.Length);
        AudioClip receivedClip = AudioClip.Create("ReceivedAudio", receivedData.Length, 1, SAMPLE_RATE, false);
        receivedClip.SetData(receivedData, 0);
        _audioSource.clip = receivedClip;
        _audioSource.Play();
    }
}
