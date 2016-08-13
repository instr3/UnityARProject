using UnityEngine;
using System.Collections;
using System;  
using System.Collections.Generic;  
using System.IO;  
using System.Linq;  
using System.Text;  
using UnityEngine;  
using System.Collections;  

public class MicrophoneControl : MonoBehaviour {

	private AudioClip clip;

	//录音的采样率
	const int samplingRate = 44100;

	/// <summary>
	/// 开始录音
	/// </summary>
	public void Recording()
	{
		string[] micDevices = Microphone.devices;
		if (micDevices.Length == 0)
		{
			Debug.Log("没有找到录音组件");
		
			return;
		}

		Debug.Log("录音时长为30秒");

		Microphone.End(null);//录音前先停掉录音，录音参数为null时采用的是默认的录音驱动
		clip = Microphone.Start(null, false, 30, samplingRate);
	
	}

	/// <summary>
	/// 停止录音
	/// </summary>
	// Use this for initialization
	public void StopRecord()
	{
		int audioLength;
		int lastPos = Microphone.GetPosition(null);
		if (Microphone.IsRecording(null))
		{
			audioLength = lastPos / samplingRate;
		}
		else
		{
			audioLength = 30;
		}
		Debug.Log (audioLength);
		Microphone.End(null);

		if (audioLength < 1.0f)
		{
			Debug.Log("录音时长短");

		}
	}

	public float Volume
	{
		get
		{
			Debug.Log ("开始分析");
			// 采样数
			int sampleSize = 128;
			float[] samples = new float[sampleSize];
			int startPosition = Microphone.GetPosition(null) - (sampleSize+1);
			// 得到数据
			if (startPosition < 0) startPosition = 0;
			sampleSize = Microphone.GetPosition (null) - 1 - startPosition;
			clip.GetData(samples, startPosition);

			// Getting a peak on the last 128 samples
			float levelMax = 0;
			for (int i = 0; i < sampleSize; ++i)
			{
				float wavePeak = samples[i] * 99;
				Debug.Log (wavePeak);
				if (levelMax < wavePeak)
					levelMax = wavePeak;
			}
			return levelMax;
		}
	}

	public Int16[] GetClipData()  
	{  
		if (clip == null)  
		{  
			Debug.Log("GetClipData audio.clip is null");  
			return null;   
		}  

		float[] samples = new float[clip.samples];  

		clip.GetData(samples, 0);  


		Int16[] outData = new Int16[samples.Length];  
		//Int16[] intData = new Int16[samples.Length];   
		//converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]   

		int rescaleFactor = 32767; //to convert float to Int16   

		for (int i = 0; i < samples.Length; i++)  
		{ 
			outData[i] = (short)(samples[i] * rescaleFactor); 

		}  
		if (outData == null || outData.Length <= 0)  
		{  
			Debug.Log("GetClipData intData is null");  
			return null;   
		}  
		//return intData;   
		return outData;  
	} 

	public void PlayClipData(Int16[] intArr)  
	{  

		/*string aaastr = intArr.ToString();  
		long  aaalength=aaastr.Length;  
		Debug.LogError("aaalength=" + aaalength);  

		string aaastr1 = Convert.ToString (intArr);  
		aaalength = aaastr1.Length;  
		Debug.LogError("aaalength=" + aaalength);  */

		if (intArr.Length == 0)  
		{  
			Debug.Log("get intarr clipdata is null");  
			return;  
		}  
		//从Int16[]到float[]   
		float[] samples = new float[intArr.Length];  
		Debug.Log ("声音长度："+intArr.Length);
		/*for (int i = 0; i < intArr.Length; i++) {
			Debug.Log ("");
		}*/
		int rescaleFactor = 32767;  
		for (int i = 0; i < intArr.Length; i++)  
		{  
			samples[i] = (float)intArr[i] / rescaleFactor;  
		}  

		//从float[]到Clip   
		AudioSource audioSource = this.GetComponent<AudioSource>();  
		if (audioSource.clip == null)  
		{  
			audioSource.clip = AudioClip.Create("playRecordClip", intArr.Length, 1, 44100, false, false);  
		}  
		audioSource.clip.SetData(samples, 0);  
		audioSource.mute = false;  
		audioSource.Play();  
	}

	public void PlayRecord()
	{
		Debug.Log ("开始播放");
		StopRecord();
		Debug.Log ("音量大小"+Volume);
		AudioSource.PlayClipAtPoint(clip, Vector3.zero);
	}

	public void PlayRecordByInt16(){
		Debug.Log ("播放int16转存声音");
		StopRecord ();
		PlayClipData (GetClipData ());
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
