using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPullout : MonoBehaviour {

	public int pulloutXLength;
	public int pulloutYLength;
	private bool isPulledOut = false;

	//Move this to the main game controller
    public void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
			Pullout();
        }
    }

    public void Pullout(){
		Vector2 transPos = this.GetComponent<RectTransform> ().anchoredPosition;


		this.GetComponent<RectTransform>().anchoredPosition = new Vector2(transPos.x + (isPulledOut ? pulloutXLength * -1 : pulloutXLength), transPos.y + (isPulledOut ? pulloutYLength * -1 : pulloutYLength));

		isPulledOut = !isPulledOut;

	}
}
