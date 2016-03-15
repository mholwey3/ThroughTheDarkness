using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GenerationScript : MonoBehaviour {
	
	private object[] resourcesGenerationContent;
	private List<GameObject> resourcesGenerationContentFixed = new List<GameObject>();
	private List<string> tagList = new List<string>();
	private List<List<GameObject>> resourcesSortedContent = new List<List<GameObject>>();

	private List<GameObject> parentsInLevel = new List<GameObject>();
	private List<Transform> parentTransforms = new List<Transform>();
	private List<GameObject> sortableChildObjects = new List<GameObject>();
	
	private bool sorted = false;
	private int current_level;

	private bool itemsSorted = false;

	private GameObject[] spots;
	private GameObject[] walls;
	private GameObject[] doorways;

	void Start()
	{
		newLevel ();
	}

	private void SwitchAllRooms()
	{
		if(sorted)
		{
			GameObject[] temp_parent = parentsInLevel.ToArray();
			foreach(GameObject item in temp_parent)
			{
				List<GameObject> temp_list = resourcesSortedContent[tagList.IndexOf(item.tag)];
				int rand_num = Random.Range(0, resourcesSortedContent[tagList.IndexOf(item.tag)].Count);
				GameObject temp = Instantiate(temp_list[rand_num], new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z), item.transform.rotation) as GameObject;
				temp.transform.parent = GameObject.FindGameObjectWithTag("Level").transform;
				parentsInLevel.Remove(item);
				parentsInLevel.Add(temp);
				DestroyImmediate(item);
			}
			SortParentLevelObjects();
			SwitchAllItems();
		}
		else
		{
			Debug.Log("Content Not Sorted");
		}
	}

	private void SwitchAllItems()
	{
		SortChildLevelObjects();
		GameObject[] temp_children = sortableChildObjects.ToArray();
		//Debug.Log(tagList.Count);
		//Debug.Log(tagList.IndexOf("Shard"));
		//Debug.Log(resourcesSortedContent[16].Count);
		foreach(GameObject item in temp_children)
		{
			List<GameObject> temp_list = resourcesSortedContent[tagList.IndexOf(item.tag)];
			int rand_num = Random.Range(0, resourcesSortedContent[tagList.IndexOf(item.tag)].Count);
			GameObject temp = Instantiate(temp_list[rand_num], new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.z), item.transform.rotation) as GameObject;
			temp.transform.parent = item.transform.parent;
			sortableChildObjects.Remove(item);
			sortableChildObjects.Add(temp);
			DestroyImmediate(item);
		}
	}

	private void UpdateFromResources()
	{
		resourcesGenerationContentFixed.Clear();
		resourcesGenerationContent = Resources.LoadAll("Generation3D");
		Debug.Log("Amount of resources in Generation3D folder: " + resourcesGenerationContent.Length);
		
		foreach (object item in resourcesGenerationContent)
		{
			resourcesGenerationContentFixed.Add(item as GameObject);
		}

		resourcesGenerationContent = Resources.LoadAll("Level_Layouts3D");
		Debug.Log("Amount of resources in Level_Layouts3D folder: " + resourcesGenerationContent.Length);

		foreach(object item in resourcesGenerationContent)
		{
			resourcesGenerationContentFixed.Add(item as GameObject);
		}
		
		Debug.Log("Amount of resources moved to list: " + resourcesGenerationContentFixed.Count);
		
		SortResourcesContent();
		SortParentLevelObjects();
	}

	private void SortParentLevelObjects()
	{
		parentsInLevel.Clear();
		parentTransforms.Clear();
		GameObject found_level = GameObject.FindGameObjectWithTag("Level");
		GameObject[] scene_items = GameObject.FindObjectsOfType<GameObject>();
		foreach(GameObject obj in scene_items)
		{
			if (obj.transform.parent == found_level.transform)
			{
				parentsInLevel.Add(obj);
				parentTransforms.Add(obj.transform);
			}
		}
	}

	private void SortChildLevelObjects()
	{
		sortableChildObjects.Clear();
		GameObject[] scene_items = GameObject.FindObjectsOfType<GameObject>();
		foreach(GameObject obj in scene_items)
		{
			if(parentTransforms.Contains(obj.transform.parent))
			{
				if(obj.tag != "Untagged" && obj.tag != "Level")
				{
					sortableChildObjects.Add(obj);
				}
			}
		}

	}
	
	private void SortResourcesContent()
	{
		tagList.Clear();
		resourcesSortedContent.Clear();
		foreach (GameObject item in resourcesGenerationContentFixed)
		{
			if(tagList.Contains(item.tag))
			{
				resourcesSortedContent[tagList.IndexOf(item.tag)].Add(item);
				
			}
			else
			{
				tagList.Add(item.tag);
				resourcesSortedContent.Add(new List<GameObject>());
				resourcesSortedContent[tagList.IndexOf(item.tag)].Add(item);
			}
		}
		sorted = true;
	}

	private void changeLevelLayout()
	{
		if(sorted)
		{
			GameObject level = GameObject.FindGameObjectWithTag("Level");
			List<GameObject> temp_list = resourcesSortedContent[tagList.IndexOf("Level")];
			int rand_num = Random.Range(0,temp_list.Count);
			while(rand_num == current_level)
			{
				rand_num = Random.Range(0,temp_list.Count);
			}
			current_level = rand_num;
			GameObject temp = Instantiate(temp_list[rand_num], new Vector3(level.transform.position.x, level.transform.position.y, level.transform.position.z),level.transform.rotation) as GameObject;
			//changeOrientation(temp);
			DestroyImmediate(level);
		}
	}

	public void printObjects(string tag)
	{
		if(sorted)
		{
			if(tagList.Contains(tag))
			{
				Debug.Log("All Objects with tag " + tag + ":");
				foreach(GameObject item in resourcesSortedContent[tagList.IndexOf(tag)])
				{
					Debug.Log(item);
				}
			}
			else
			{
				Debug.Log("That tag does not exist in List");
			}
		}
		else
		{
			Debug.Log("Content Not Sorted");
		}
	}

	public void printParentObjects()
	{
		foreach(GameObject obj in parentsInLevel)
		{
			Debug.Log(obj.name);
		}
	}

	public void printChildObjects()
	{
		foreach(GameObject obj in sortableChildObjects)
		{
			Debug.Log(obj.name);
		}
	}

	private void changeOrientation(GameObject obj)
	{
		int rand = Random.Range(0,2);

		if(rand == 0)
		{
			obj.transform.localScale = new Vector3(-1, obj.transform.localScale.y, obj.transform.localScale.z);
		}

		else
		{
			obj.transform.localScale = new Vector3(1, obj.transform.localScale.y, obj.transform.localScale.z);
		}
		rand = Random.Range(0,2);

		if(rand == 0)
		{
			obj.transform.localScale = new Vector3(obj.transform.localScale.x, -1, obj.transform.localScale.z);
		}
		else
		{
			obj.transform.localScale = new Vector3(obj.transform.localScale.x, 1, obj.transform.localScale.z);
		}
	}

	public void printSceneObjects()
	{
		if(sorted)
		{
			int num = parentsInLevel.Count + sortableChildObjects.Count;
			Debug.Log("Number of Scene Objects: " + num);
		}
		else
		{
			Debug.Log("Content Not Sorted");
		}
	}

	public void printTagList()
	{
		foreach(string str in tagList)
		{
			Debug.Log(str);
		}
	}

	public void newLevel()
	{
		if(!itemsSorted)
		{
			UpdateFromResources();
		}
		SwitchAllRooms();
		Decide();
	}

	public void Decide()
	{
		spots = GameObject.FindGameObjectsWithTag("StartAndEnd");
		walls = GameObject.FindGameObjectsWithTag("Wall");
		doorways = GameObject.FindGameObjectsWithTag("Door");
		
		int start = Random.Range(0, spots.Length);
		int end = Random.Range(0, spots.Length);
		if(spots.Length == 1)
		{
			Debug.Log("Can't complete with only 1 entrance/exit");
			return;
		}
		while(end == start)
		{
			end = Random.Range(0, spots.Length);
		}
		
		foreach (GameObject obj in walls)
		{
			if(obj.transform.parent == spots[start].transform)
			{
				float tempX = obj.transform.parent.position.x;
				float tempZ = obj.transform.parent.position.z;
				GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(tempX, 0.0f , tempZ);
				GameObject.FindGameObjectWithTag("Player").transform.rotation = obj.transform.parent.rotation;
				//obj.transform.position = new Vector3(tempX - 2f,obj.transform.parent.position.y, obj.transform.parent.position.z);*/
				obj.transform.position = new Vector3(obj.transform.parent.position.x,-100, obj.transform.parent.position.z);
			}
			else if(obj.transform.parent == spots[end].transform)
			{
				obj.transform.position = new Vector3(obj.transform.parent.position.x,-100, obj.transform.parent.position.z);
			}
			else
			{
				obj.transform.position = new Vector3(obj.transform.parent.position.x,obj.transform.parent.position.y,obj.transform.parent.position.z);
			}
		}
		foreach(GameObject obj in doorways)
		{
			if(obj.GetComponent<End>() != null)
			{
				DestroyImmediate(obj.GetComponent<End>());
				DestroyImmediate(obj.GetComponent<SphereCollider>());
			}
			if(obj.transform.parent == spots[start].transform || obj.transform.parent == spots[end].transform)
			{
				if(obj.transform.parent == spots[end].transform)
				{
					obj.AddComponent<SphereCollider>();
					SphereCollider temp = obj.GetComponent<SphereCollider>();
					temp.radius = .95f;
					temp.center = new Vector3(0f,1f,0f);
					temp.isTrigger = true;
					obj.AddComponent<End>();
				}
				obj.transform.position = new Vector3(obj.transform.parent.position.x,obj.transform.parent.position.y,obj.transform.parent.position.z);
			}
			else
			{
				obj.transform.position = new Vector3(obj.transform.parent.position.x,-100, obj.transform.parent.position.z);
			}
		}
	}
	
}