// ------------------------------------------------------------------------------
// Gesture class for gesture controller
// author: Radomir Slaboshpitsky 
// mail: radiys92@gmail.com
// ------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Information of gesture
/// </summary>
public class Gesture
{
	public delegate void func(Gesture g);

	/// <summary>
	/// This delegate call when gesture added information of new touch
	/// </summary>
	public func OnGestureStay;

	/// <summary>
	/// Get or set touches of gesture
	/// </summary>
	public List<GestureTouch> Frames { get; set; }

	/// <summary>
	/// Return count of gesture touches
	/// </summary>
	public int FramesCount { get { return Frames.Count; } }

	/// <summary>
	/// Return ID of gesture or -1 if gesture haven't touches data
	/// </summary>
	public int ID { get { return Frames.Count > 0 ? Frames[0].buttonID : -1; } }

	/// <summary>
	/// Return start point (in screen 2D space) of gesture
	/// </summary>
	public Vector2 StartPoint { get { return Frames.Count > 0 ? Frames[0].position : Vector2.zero; } }

	/// <summary>
	/// Return end point (in screen 2D space) of gesture
	/// </summary>
	public Vector2 EndPoint { get { return Frames.Count > 0 ? Frames[Frames.Count-1].position : Vector2.zero; } }

	/// <summary>
	/// Calculate and return center point of gesture
	/// </summary>
	public Vector2 CenterPoint
	{
		get
		{
			Vector2 res = Frames[0].position;
			for (int i = 1; i < Frames.Count; i++)
				res += Frames[i].position;
			res /= Frames.Count;
			return res;
		}
	}

	/// <summary>
	/// Return angle of vector.
	///      270
	///       |
	///   0 --|-- 180
	///       |
	///       90
	/// </summary>
	private float Angle(Vector2 point)
	{
		return Mathf.Atan2(point.y, point.x) * (180 / Mathf.PI) + 180;
	}


	/// <summary>
	/// Return angle of first and last point of gesture
	/// </summary>
	/// <returns>The last angle.</returns>
	public float FirsLastAngle()
	{
		return Angle(EndPoint - StartPoint);
	}

	/// <summary>
	/// Calculate and return turns at gesture. Uses for process broken lines.
	/// </summary>
	/// <returns>Count of turns</returns>
	/// <param name="minAngle">Minimum angle</param>
	/// <param name="countOfFrames">Count of frames per every turn (max)</param>
	public int TurnsCount(float minAngle,int countOfFrames)
	{
		if (Frames.Count == 0)
			return 0;
		int res = 0;

		for (int i = 1; i < Frames.Count-countOfFrames-1; i++)
		{
			for (int j = 0; j < countOfFrames; j++)
			{
				if (//Frames[i].deltaPosition != Frames[i + j].deltaPosition &&
				    Frames[i].phase == TouchPhase.Moved &&
				    Frames[i + j].phase == TouchPhase.Moved)
				{
					if (Vector2.Angle(Frames[i].deltaPosition, Frames[i + j].deltaPosition) > minAngle)
					{
						res++;
						i += countOfFrames;
						break;
					}
				}
			}
		}
		
		return res;
	}

	/// <summary>
	/// Get gesture code by translating every frame to numeric code.
	/// </summary>
	/// <value>The code.</value>
	public string Code
	{
		get
		{
			string res = "";
			foreach (GestureTouch t in Frames)
				if (t.deltaPosition.sqrMagnitude != 0)
			{
				float dat = Angle(t.deltaPosition) + 210;
				if (dat > 180)
					dat -= 180;
				dat /= 60;
				res += (int)dat;
			}
			return res;
		}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Gesture"/> class.
	/// </summary>
	public Gesture()
	{
		Frames = new List<GestureTouch>();
		OnGestureStay = (Gesture g) => {};
	}

	~Gesture()
	{
		OnGestureStay = (Gesture g) => {};
	}

	/// <summary>
	/// Adds the touch to gesture data
	/// </summary>
	/// <param name="touch">Touch.</param>
	public void AddTouch(GestureTouch touch)
	{
		Frames.Add(touch);
		OnGestureStay(this);
	}

	/// <summary>
	/// Get time of gesture
	/// </summary>
	public float GestureTime 
	{
		get
		{
			float d = 0;
			foreach (GestureTouch t in Frames)
				d += t.deltaTime;
			return d;
		}
	}

	/// <summary>
	/// Get length of gesture in screen pixels
	/// </summary>
	public float Distance
	{
		get
		{
			float l = 0;
			foreach (GestureTouch t in Frames)
				l += t.deltaPosition.magnitude;
			return l;
		}
	}

	/// <summary>
	/// Gets the first touch
	/// </summary>
	public GestureTouch FirstTouch { get { return Frames[0]; } }

	/// <summary>
	/// Gets the last touch
	/// </summary>
	public GestureTouch LastTouch { get { return Frames[Frames.Count - 1]; } }
}
