using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class MainCamera : MonoBehaviour
{
	public CinemachineImpulseSource inpulse;
	public CinemachineImpulseSource randomInpulse;

	public float dashShakeLevel = .3f;// 震动幅度
	public float attackShakeLevel = .15f;
	public float commonShakeLevel = .35f;// 震动幅度
	public float shakeTime = 0.15f;   // 震动时间
	public int shakeFps = 45;    // 震动的FPS

	private Vector2 shakeDir;  //震动方向
	private bool isshakeCamera = false;// 震动标志
	private float level = 6f;
	private float fps;
	private float shakeTimer = 0.0f;
	private float frameTimer = 0.0f;
	private float shakeDelta = 0.005f;

	private Camera selfCamera;
	public CinemachineVirtualCamera cinemachine;
	public CinemachineVirtualCamera viewCamera;
	public Rigidbody2D viewPoint;
	private bool boost = false;
	private bool reduce = false;

	public float minScale = 10.75f;
	public float maxScale = 9.25f;

	public float scaleSpeed = 0.15f;

	public bool moving = false;
	public float speed;
	public Vector2 dir;
	private Rigidbody2D rb;


	public Material lineMaterial;
	public void Start()
	{
		AddListener();
		selfCamera = gameObject.GetComponent<Camera>();
	}

	private void AddListener()
	{
		MessageManager.Instance.AddListener(MessageManager.MessageId.MiddleExplodeBegin, Shake);

		MessageManager.Instance.AddListener(MessageManager.MessageId.DashBegin, Dash);
		MessageManager.Instance.AddListener(MessageManager.MessageId.AttackBegin, Attack);

		MessageManager.Instance.AddListener(MessageManager.MessageId.CompleteLoadStage, CompletLoadStage);

		MessageManager.Instance.AddListener(MessageManager.MessageId.PlayerDeadPre, Shake);

		MessageManager.Instance.AddListener(MessageManager.MessageId.ShutDownEnd, CameraClose);
		MessageManager.Instance.AddListener(MessageManager.MessageId.NextStage, CameraClose);

		MessageManager.Instance.AddListener(MessageManager.MessageId.ResetEffectBegin, CameraOpen);

		MessageManager.Instance.AddListener(MessageManager.MessageId.HackSearchBegin, HackSearchBegin);
		MessageManager.Instance.AddListener(MessageManager.MessageId.HackSearchEnd, HackSearchEnd);
		
		MessageManager.Instance.AddListener(MessageManager.MessageId.Shake, Shake);
	}

	void CameraClose(Message message)
	{
		selfCamera.enabled = false;
	}

	void CameraOpen(Message message)
	{
		selfCamera.enabled = true;
	}
	
	void CompletLoadStage(Message message)
	{
		//StartCoroutine(GetCameraScope());
	}

	IEnumerator GetCameraScope()
	{
		yield return 0;
		GameObject o = GameObject.Find("CameraScope");
		
//		CinemachineConfiner2D cd = GetComponentInChildren<CinemachineConfiner2D>();
		//cd.m_BoundingShape2D = o.GetComponent<Collider2D>();
		
//		CinemachineConfiner2D viewConfiner2D = viewCamera.gameObject.GetComponent<CinemachineConfiner2D>();
//		viewConfiner2D.m_BoundingShape2D = o.GetComponent<Collider2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (boost)
		{
			cinemachine.m_Lens.OrthographicSize -= scaleSpeed;
			cinemachine.m_Lens.OrthographicSize = Mathf.Clamp(cinemachine.m_Lens.OrthographicSize, minScale, maxScale);
		}

		if (reduce)
		{
			cinemachine.m_Lens.OrthographicSize += scaleSpeed;
			cinemachine.m_Lens.OrthographicSize = Mathf.Clamp(cinemachine.m_Lens.OrthographicSize, minScale, maxScale);
		}

		if (moving)
		{
			dir.x = Input.GetAxisRaw("Horizontal");
			viewPoint.velocity = dir * speed;
		}
	}

	public void Shake(Message message = null)
	{
		if (message == null || message.f == 0)
		{
			randomInpulse.GenerateImpulseWithForce(commonShakeLevel);
		}
		else
		{
			randomInpulse.GenerateImpulseWithForce(message.f);
		}
	}

	public void Shake(Vector2 dir, float customTime = 0, float customFps = 0, float customLevel = 0)
	{
		//inpulse.GenerateImpulseWithVelocity(dir.normalized * (customLevel == 0 ? dashShakeLevel : customLevel));
	}

	public IEnumerator Scale(float boostTime, float reduceTime)
	{
		float originSize = cinemachine.m_Lens.OrthographicSize;
		boost = true;
		yield return new WaitForSeconds(boostTime);
		boost = false;
		reduce = true;
		yield return new WaitForSeconds(reduceTime);
		reduce = false;
		cinemachine.m_Lens.OrthographicSize = originSize;
	}

	private IEnumerator Boost(float time)
	{
		boost = true;
		yield return new WaitForSeconds(time);
		boost = false;
	}

	private IEnumerator Reduce(float time)
	{
		reduce = true;
		yield return new WaitForSeconds(time);
		reduce = false;
	}

	private void Dash(Message message = null)
	{
		Shake(message.pos2);
	}
	
	private void Attack(Message message = null)
	{
		Shake(message.pos2, 0,0,attackShakeLevel);
	}
	
	public void HackSearchBegin(Message message = null)
	{
		Shake(Vector2.up, 0.05f, 20);
		viewPoint.transform.position = new Vector2(10.67f, 0);
		viewCamera.gameObject.SetActive(true);
		cinemachine.gameObject.SetActive(false);
		moving = true;
	}

	public void HackSearchEnd(Message message = null)
	{
		viewCamera.gameObject.SetActive(false);
		cinemachine.gameObject.SetActive(true);
		moving = false;
	}
}