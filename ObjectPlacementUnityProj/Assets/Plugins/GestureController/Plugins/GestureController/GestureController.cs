using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class, which implements tools for control gestures from touches and mouse.
/// Enabled multitouch.
/// Implemented as singleton, don't need initialization.
/// For uses functional, use OnGestureStart/OnGestureEnd delegats.
///
/// author: Radomir Slaboshpitsky 
/// mail: radiys92@gmail.com
/// 2014
/// </summary>
public class GestureController : MonoSingleton<GestureController> 
{
	private Vector2[] _pos = new Vector2[3] {Vector2.zero,Vector2.zero,Vector2.zero};
	private float[] _time = new float[3] {0,0,0};
	private bool[] _pressed = new bool[3] {false,false,false};
    private Gesture[] _lg = new Gesture[3];
    List<Gesture> g;

	/// <summary>
	/// Gets the count of gestures.
	/// </summary>
    public static int CountOfGestures 
    {
        get
        {
			if (GestureController.Instance == null)
				return 0;
			else
			{
	            if (GestureController.Instance.g==null)
	                return 0;
	            else
	                return GestureController.Instance.g.Count;
			}
        }
    }

    public delegate void func(Gesture g);
    private delegate void Ffunc();
    private func _onGestureStart;
    private func _onGestureEnd;

	/// <summary>
	/// Gets or sets the gesture start delegate.
	/// Return null if GestureController haven't instance and it cannot be created
	/// </summary>
    public static func OnGestureStart
    {
        get { return Instance == null? null: Instance._onGestureStart; }
        set { if (Instance!=null) Instance._onGestureStart = value; }
    }

	/// <summary>
	/// Gets or sets the gesture end delegate.
	/// Return null if GestureController haven't instance and it cannot be created
	/// </summary>
    public static func OnGestureEnd
    {
        get { return Instance == null? null : Instance._onGestureEnd; }
        set { if (Instance!=null) Instance._onGestureEnd = value; }
    }

    private Ffunc FUpdate;

    public override void OnDestroy ()
    {
        base.OnDestroy ();
        _onGestureEnd = (a) => {};
        _onGestureStart = (a) => {};
        foreach (Gesture gest in g)
            gest.OnGestureStay = (a) => {};
    }

    public GestureController()
    {
        g = new List<Gesture>();
        _onGestureEnd = (a) => {};
        _onGestureStart = (a) => {};
    }

    private GestureTouch GetDeltaTouch(int ButtonID)
    {
        GestureTouch touch = new GestureTouch();
        touch.position = Input.mousePosition;
        touch.deltaPosition = touch.position-_pos[ButtonID];
        touch.Time = Time.time;
        touch.deltaTime = touch.Time - _time[ButtonID];
        touch.phase = TouchPhase.Moved;
        touch.buttonID = 0;
        _time[ButtonID] = touch.Time;
        _pos[ButtonID] = touch.position;
        return touch;
    }

    private Vector2 v3tov2 (Vector3 vec)
    {
        return new Vector2(vec.x, vec.y);
    }

    private GestureTouch GetTouch(UnityEngine.Touch t)
    {
        GestureTouch touch = new GestureTouch();
        touch.position = t.position;
        touch.deltaPosition = t.deltaPosition;
        touch.deltaTime = t.deltaTime;
        touch.Time = Time.time;
        touch.phase = t.phase;
        touch.buttonID = t.fingerId;
        return touch;
    }

	private bool[] _break = new bool[3] {false,false,false};

	void UpdateMouseGesture(int ButtonID)
	{
		if (_pressed[ButtonID])
			_lg[ButtonID].AddTouch(GetDeltaTouch(ButtonID));
		
		if (_break[ButtonID] && _pressed[ButtonID])
		{
			_pressed[ButtonID] = false;
			// on finish gesture
			GestureTouch touch = GetDeltaTouch(ButtonID);
			touch.phase = TouchPhase.Ended;
			_lg[ButtonID].AddTouch(touch);
			_onGestureEnd(_lg[ButtonID]);
			return;
		}
		
		if (!Input.GetMouseButton(ButtonID) && _break[ButtonID])
			_break[ButtonID] = false;
		else if (_break[ButtonID])
			return;
		
		if (Input.GetMouseButton(ButtonID) && !_pressed[ButtonID])
		{
			_pressed[ButtonID]= true;
			_pos[ButtonID] = Input.mousePosition;
			_time[ButtonID] = Time.time;
			// on start gesture
			_lg[ButtonID] = new Gesture();
			GestureTouch touch = new GestureTouch();
			touch.position = _pos[ButtonID];
			touch.deltaTime = 0;
			touch.deltaPosition = Vector2.zero;
			touch.phase = TouchPhase.Began;
			touch.buttonID = ButtonID;
			touch.Time = Time.time;
			_lg[ButtonID].AddTouch(touch);
			_onGestureStart(_lg[ButtonID]);
		}
		if (!Input.GetMouseButton(ButtonID) && _pressed[ButtonID])
		{
			_pressed[ButtonID] = false;
			// on finish gesture
			GestureTouch touch = GetDeltaTouch(ButtonID);
			touch.phase = TouchPhase.Ended;
			_lg[ButtonID].AddTouch(touch);
			_onGestureEnd(_lg[ButtonID]);
			_lg[ButtonID] = null;
		}
	}

    void FixedMouseUpdate()
    {
		UpdateMouseGesture(0);
		UpdateMouseGesture(1);
		UpdateMouseGesture(2);
    }

	/// <summary>
	/// Gets Gesture by it ID
	/// </summary>
	/// <returns>Gesture</returns>
	/// <param name="id">Identifier</param>
    public static Gesture GetGByID(int id)
    {
		if (GestureController.Instance)
		{
	        foreach (Gesture t in GestureController.Instance.g)
	            if (t.ID == id)
	                return t;
		}
        return null;
    }

    void FixedTouchUpdate()
    {
        Gesture temp;
        foreach (UnityEngine.Touch t in Input.touches)
        {
            temp = GetGByID(t.fingerId);
            if (t.phase == TouchPhase.Canceled || t.phase == TouchPhase.Ended)
            {
                if (temp != null)
                {
                    temp.AddTouch(GetTouch(t));
                    _onGestureEnd((Gesture)temp);
                    g.Remove(temp);
                }
            }
            else
            {
                if (temp != null)
                    temp.AddTouch(GetTouch(t));
                else if (t.phase == TouchPhase.Began)
                {
                    temp = new Gesture();
                    temp.AddTouch(GetTouch(t));
                    g.Add(temp);
                    _onGestureStart(temp);
                }
            } 
        }
    }

    private void CancelGesture(Gesture G)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.WP8Player)
        {
            Gesture temp = GetGByID(G.ID);
            if (temp != null)
            {
                //print("Stop");
                _onGestureEnd((Gesture)temp);
                g.Remove(temp);
            }
        } else
        {
			for (int i=0;i<_lg.Length;i++)
				if (_lg[i] != null && _lg[i].ID == G.ID)
					_break[i] = true;
        }
    }

	/// <summary>
	/// Stops the gesture.
	/// </summary>
	/// <param name="g">Gesture which need to be ended</param>
    public static void StopGesture(Gesture g)
    {
        GestureController.Instance.CancelGesture(g);
    }

    private IEnumerator Loop()
    {
        while (true)
        {
            FUpdate();
            yield return null;
        }
    }

    void Awake()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.WP8Player)
            FUpdate = FixedTouchUpdate;
        else
            FUpdate = FixedMouseUpdate;
        StartCoroutine("Loop");
        Input.multiTouchEnabled = true;
    }

}