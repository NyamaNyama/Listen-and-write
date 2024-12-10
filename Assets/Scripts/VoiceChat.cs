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
    float _recordingLength;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();  
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        if (isRecording)
        {
            SendAudio();
        }
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            StartRecord();
        }

        if(Input.GetKeyUp(KeyCode.E))
        {
            StopRecord();
        }
        
    }

    private void StartRecord()
    {
        _audioClip = Microphone.Start(null, true, RECORD_DURATION, SAMPLE_RATE);
        isRecording = true;
        _lastSamplePosition = 0;
    }



    private void StopRecord() 
    {
        Microphone.End(null);
        isRecording= false;
    }

    private void SendAudio()
    {
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
        float[] receivedData = new float[audioData.Length/4];
        Buffer.BlockCopy(audioData,0, receivedData, 0, audioData.Length);
        AudioClip receivedClip = AudioClip.Create("ReceivedAudio", receivedData.Length, 1, SAMPLE_RATE, false);
        receivedClip.SetData(receivedData, 0);
        _audioSource.clip = receivedClip;
        _audioSource.Play();
    }
}
