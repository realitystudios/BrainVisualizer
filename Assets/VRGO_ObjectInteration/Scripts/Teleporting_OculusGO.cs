using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting_OculusGO : MonoBehaviour
{
	public GameObject positionMarker;

	public Transform bodyTransforn;

	public LayerMask excludeLayers;

	public float angle = 45f;

	public float strength = 10f;

	int maxVertexcount = 100;

	private float vertexDelta = 0.08f;

	private LineRenderer arcRenderer;

	private Vector3 velocity;

	private Vector3 groundPos;

	private Vector3 lastNormal;

	private bool groundDetected = false;

	private List<Vector3> vertexList = new List<Vector3>();

	private bool displayActive = false;


	public void Teleport()
	{
		if (groundDetected)
		{
			bodyTransforn.position = groundPos + lastNormal * 0.1f;
		}
		else
		{
			Debug.Log("Ground wasn't detected");
		}
	}

	public void ToggleDisplay(bool active)
	{
		arcRenderer.enabled = active;
		positionMarker.SetActive(active);
		displayActive = active;
	}

	private void Awake()
	{
		arcRenderer = GetComponent<LineRenderer>();
		arcRenderer.enabled = false;
		positionMarker.SetActive(false);
	}

	private void FixedUpdate()
	{
		if (displayActive)
		{
			UpdatePath();
		}
	}

	private void UpdatePath()
	{
		groundDetected = false;

		vertexList.Clear();


		velocity = Quaternion.AngleAxis(-angle, transform.right) * transform.forward * strength;

		RaycastHit hit;

		Vector3 pos = transform.position;
		vertexList.Add(pos);

		while (!groundDetected && vertexList.Count < maxVertexcount)
		{
			Vector3 newPos = pos + velocity * vertexDelta
				+ 0.5f * Physics.gravity * vertexDelta * vertexDelta;

			velocity += Physics.gravity * vertexDelta;

			vertexList.Add(newPos);

			if (Physics.Linecast (pos, newPos, out hit, ~excludeLayers) && hit.collider.CompareTag ("Teleport"))
			{
				groundDetected = true;
				groundPos = hit.point;
				lastNormal = hit.normal;
			}
			pos = newPos;
		}

		positionMarker.SetActive(groundDetected);

		if (groundDetected)
		{
			arcRenderer.materials [0].color = Color.blue;
			positionMarker.transform.position = groundPos + lastNormal * 0.1f;
			positionMarker.transform.LookAt(groundPos);
		}

		if(!groundDetected)
		{
			arcRenderer.materials [0].color = Color.red;
		}

		arcRenderer.positionCount = vertexList.Count;
		arcRenderer.SetPositions(vertexList.ToArray());
	}
}
