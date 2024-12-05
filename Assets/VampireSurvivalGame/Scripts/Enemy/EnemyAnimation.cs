using System.Threading;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
	public float duration = 2f;  // 控制一个完整缩放周期的持续时间
	private float minScale = 0.9f;  // 最小缩放值
	private float maxScale = 1.1f;  // 最大缩放值
	private float timer;  // 时间计数器

	private void Update()
	{
		// 更新时间计数器，循环在0到duration之间
		timer += Time.deltaTime;
		if (timer > duration)
			timer -= duration;

		// 使用正弦函数创建一个周期为duration的平滑循环
		float t = Mathf.Sin(timer / duration * Mathf.PI * 2) * 0.5f + 0.5f;

		// 使用t作为插值参数，计算当前缩放
		float scale = Mathf.Lerp(minScale, maxScale, t);
		transform.localScale = new Vector3(scale, scale, scale);
	}
}
