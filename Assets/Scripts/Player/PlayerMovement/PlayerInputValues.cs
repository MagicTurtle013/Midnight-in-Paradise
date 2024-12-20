using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace MpPlayerInput
{
	public class PlayerInputValues : MonoBehaviour
	{
		public static PlayerInputValues Instance;
		[Header("Character Input Values")] public Vector2 Move;
		public Vector2 Look;
		public bool Jump;
		public bool Sprint;

		[Header("Movement Settings")] public bool analogMovement;

		[Header("Mouse Cursor Settings")] public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(this);
			}
		}

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif

		public void MoveInput(Vector2 newMoveDirection)
		{
			Move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			Look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			Jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			Sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
}
