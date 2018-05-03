using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour {
	[SerializeField] GameObject gridObject;

	private bool gridObjectExists;
	public int gridsToGenerate = 15;

	private const float GRID_SPACE_HEIGHT = 9.61f;
	//the grid transforms we want to change
	private List<Transform> gridTransforms = new List<Transform>();
	private List<MeshRenderer[]> gridMeshes = new List<MeshRenderer[]>();


	public float zToLerp = -11.4f;

	public float gridSpeed = 2f;
	public float maxDistanceOfCamera = 2f;

	public int currentGridHeader = 0;
	public int lastGridElement = 0;

	public Color startColor, endColor;

	//happens as soon as the script starts
	void Awake()
	{
		gridTransforms = new List<Transform> ();

		gridObjectExists = gridObject != null;

		if (gridObjectExists) {
			GameObject previousGo = gridObject;
			for (int i = 0; i < gridsToGenerate; i++) 
			{
				GameObject go = Instantiate (gridObject);
				go.transform.position = previousGo.transform.position;
				go.transform.position = new Vector3 (previousGo.transform.position.x, previousGo.transform.position.y, previousGo.transform.position.z - GRID_SPACE_HEIGHT);

				//gets all the mesh renderers of the grid object
				MeshRenderer[] rs = go.GetComponentsInChildren<MeshRenderer> ();
				gridMeshes.Add (rs);
				for (int j = 0; j < rs.Length; j++) 
				{
					rs [j].material.color = endColor;
				}

				gridTransforms.Add (go.transform);

				previousGo = go;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (gridObjectExists) 
		{
			for (int i = 0; i < gridTransforms.Count; i++) 
			{
				Transform grid = gridTransforms [i];
				grid.position = Vector3.MoveTowards (grid.position, new Vector3 (grid.position.x, grid.position.y, zToLerp - (GRID_SPACE_HEIGHT * i)), Time.deltaTime * gridSpeed);

				float dist = Vector3.Distance (Camera.main.transform.position, gridTransforms [i].position);

				// performance intensive, will call garbage collector often
//				MeshRenderer[] rs = gridTransforms [i].GetComponentInChildren<MeshRenderer> ();
				for (int j = 0; j < gridMeshes[i].Length; j++) 
				{
					//Lerp the grid color according to the distance between the object and the camera
					gridMeshes[i][j].material.color = Color.Lerp(gridMeshes[i][j].material.color, endColor, Time.deltaTime * (dist / maxDistanceOfCamera) / 1000);
				}

				Vector3 toTarget = (Camera.main.transform.position - gridTransforms [i].position).normalized;
				// if the grid object is behind the camera then set its position the same as GridObject
				if (Vector3.Dot (toTarget, transform.forward) > 0)
				{
					Transform t = gridObject.transform;
					gridTransforms [i].position = new Vector3 (t.position.x, t.position.y, t.position.z - GRID_SPACE_HEIGHT);
					currentGridHeader++;

					for (int j=0; j < gridMeshes[i].Length; j++)
					{
						gridMeshes[i][j].material.color = startColor;
					}

					if (currentGridHeader > gridTransforms.Count) 
					{
						currentGridHeader = 0;
					}
				}
			}
		}

	}
}
