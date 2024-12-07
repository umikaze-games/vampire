using UnityEngine;

public class SpinWeapons : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed=10;


	private void Update()
	{
		RotateFireball();
	}

	private void RotateFireball()
	{
		//float time = 0;
		//time += Time.deltaTime;
		transform.Rotate(0, 0, Time.deltaTime*rotateSpeed);
	}

	
}
