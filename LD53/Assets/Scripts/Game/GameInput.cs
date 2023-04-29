using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class GameInput : MonoBehaviour
    {
        private Controls controls;

        private void Awake()
        {
            controls = new Controls();
            controls.Enable();
        }

        public Vector2 GetMove()
        {
            return controls.Player.Move.ReadValue<Vector2>();
        }

        public Vector2 GetLook()
        {
            return controls.Player.Look.ReadValue<Vector2>();
        }

        public bool GetInteractButton()
        {
            return controls.Player.Interact.WasPerformedThisFrame();
        }

        public bool GetInteractButtonDown()
        {
            return controls.Player.Interact.WasPressedThisFrame();
        }

        public bool GetInteractButtonUp()
        {
            return controls.Player.Interact.WasReleasedThisFrame();
        }

        public bool GetCheatLevelButtonDown()
        {
            return controls.Player.CheatLevel.WasPressedThisFrame();
        }

        public bool GetCheatButton()
        {
            return controls.Player.Cheat.WasPerformedThisFrame();
        }

        public bool GetCheatButtonDown()
        {
            return controls.Player.Cheat.WasPressedThisFrame();
        }

        public bool GetCheatButtonUp()
        {
            return controls.Player.Cheat.WasReleasedThisFrame();
        }
    }
}
