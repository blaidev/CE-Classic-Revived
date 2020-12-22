using System.Collections;
using PowerTools;
using UnityEngine;

public class KillAnimation : MonoBehaviour
{
	public AnimationClip BlurAnim;

	public DeadBody bodyPrefab;

	public Vector3 BodyOffset;

	public IEnumerator CoPerformKill(PlayerControl source, PlayerControl target)
	{
		bool isParticipant = PlayerControl.LocalPlayer == source || PlayerControl.LocalPlayer == target;
		PlayerPhysics sourcePhys = source.GetComponent<PlayerPhysics>();
		SetMovement(source, canMove: false);
		SetMovement(target, canMove: false);
		if (isParticipant)
		{
			Camera.main.GetComponent<FollowerCamera>().Locked = true;
		}
		bool iszomb = PlayerControl.GameOptions.Gamemode == 1;
		if (!iszomb)
		{
			target.Die(DeathReason.Kill);
		}
		SpriteAnim sourceAnim = source.GetComponent<SpriteAnim>();
		yield return new WaitForAnimationFinish(sourceAnim, BlurAnim);
		source.NetTransform.SnapTo(target.transform.position);
		sourceAnim.Play(sourcePhys.IdleAnim);
		SetMovement(source, canMove: true);
		DeadBody deadBody = Object.Instantiate(bodyPrefab);
		Vector3 position = target.transform.position + BodyOffset;
		position.z = position.y / 1000f;
		deadBody.transform.position = position;
		deadBody.ParentId = target.PlayerId;
		if (!iszomb)
		{
			target.SetPlayerMaterialColors(deadBody.GetComponent<Renderer>());
		}
		SetMovement(target, canMove: true);
		if (isParticipant)
		{
			Camera.main.GetComponent<FollowerCamera>().Locked = false;
		}
	}

	public static void SetMovement(PlayerControl source, bool canMove)
	{
		source.moveable = canMove;
		source.NetTransform.enabled = canMove;
		source.MyPhysics.enabled = canMove;
		source.MyPhysics.ResetAnim(stopCoroutines: false);
		source.NetTransform.Halt();
	}
}