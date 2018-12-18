using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVariable : MonoBehaviour 
{
	private GameObject[] MyGuys = new GameObject[10];
	public GameObject copyGuy;
	public IntVariable MyVariable;
	public UnityEngine.UI.Text myText;
	public void Start()
	{
		SetText();
		for(int x = 0; x < MyGuys.Length; x++)
		{
			GameObject go = Instantiate(copyGuy);
			float TempX = Random.Range(-8f, 1f);	
			float TempY = Random.Range(-4f, 4f);
			go.transform.position = new Vector3(TempX, TempY, 0);
			MyGuys[x] = go;
			go.active = false;
		}
		for(int x = 0; x < MyVariable.Value; x++)
		{
			MyGuys[x].active = true;
		}
	}
	public void UpOne()
	{
		print("GOing up!");
		MyVariable.Value = MyVariable.Value == 10 ? 10 : MyVariable.Value + 1;
		MyGuys[MyVariable.Value - 1].active = true;
		//MyVariable.Value++;
		SetText();
	}
	public void DownOne()
	{
		
		MyVariable.Value = MyVariable.Value == 1 ? 1 : MyVariable.Value - 1;
		MyGuys[MyVariable.Value].active = false;
		SetText();
	}
	public void SetText()
	{
		myText.text = MyVariable.Value + "";
	}
	
	
}
