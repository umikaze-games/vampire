using System.Threading;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
	public float duration = 2f;  // ����һ�������������ڵĳ���ʱ��
	private float minScale = 0.9f;  // ��С����ֵ
	private float maxScale = 1.1f;  // �������ֵ
	private float timer;  // ʱ�������

	private void Update()
	{
		// ����ʱ���������ѭ����0��duration֮��
		timer += Time.deltaTime;
		if (timer > duration)
			timer -= duration;

		// ʹ�����Һ�������һ������Ϊduration��ƽ��ѭ��
		float t = Mathf.Sin(timer / duration * Mathf.PI * 2) * 0.5f + 0.5f;

		// ʹ��t��Ϊ��ֵ���������㵱ǰ����
		float scale = Mathf.Lerp(minScale, maxScale, t);
		transform.localScale = new Vector3(scale, scale, scale);
	}
}
