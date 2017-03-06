using UnityEngine;
using System.Collections;

using OpenCvSharp;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Mat src = new Mat("lenna.png", ImreadModes.GrayScale);
		Mat dst = new Mat();

		Cv2.Canny(src,dst,50,200);

		using (new Window("src image", src))
		using (new Window("dst image", dst))
		{
			Cv2.WaitKey();
		}
	}
}
