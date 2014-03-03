using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace PokePlanet
{
    class Input
    {
        public enum InputDirectionType
        {
            Absolute,
            Scaled,
            Full
        }

        internal static void Initialize()
        {
            TouchPanel.EnabledGestures = GestureType.FreeDrag;
            InputDirection = new Vector2();
        }

        public static void UpdateControlStates(Vector2 origin)
        {
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentMouseState = Mouse.GetState();

            InputDirection = Vector2.Zero;
//            InputDirection.X = 0f;
//            InputDirection.Y = 0f;


            //Windows 8 touch gestures for monogame
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                if (gesture.GestureType == GestureType.FreeDrag)
                {
                    CurrentInputDirectionType = InputDirectionType.Absolute;
                    InputDirection = gesture.Delta;
                }
            }

            //get mouse state and capture button type and respond button press
            MousePosition.X = currentMouseState.X;
            MousePosition.Y = currentMouseState.Y;

            bool leftMouseDown = currentMouseState.LeftButton == ButtonState.Pressed;
            if (leftMouseDown)
            {
                CurrentInputDirectionType = InputDirectionType.Full;
                Vector2 posDelta = MousePosition - origin;
                posDelta.Normalize();
                InputDirection = posDelta;
                //                posDelta = posDelta * playerMoveSpeed;
                //                player.Position = player.Position + posDelta;
            }

            Vector2 analogDirection = currentGamePadState.ThumbSticks.Left;
            if (analogDirection.X != 0 || analogDirection.Y != 0)
            {
                CurrentInputDirectionType = InputDirectionType.Scaled;
                InputDirection = analogDirection;
                InputDirection.Y = -InputDirection.Y;
            }

            //            player.Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
            //            player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;

            Keys[] arrowKeysPressed = currentKeyboardState.GetPressedKeys();
            Keys[] arrowKeys = { Keys.Left, Keys.Right, Keys.Up, Keys.Down };
            bool usingArrowKeys = false;
            foreach (var key in arrowKeys.Where(arrowKeysPressed.Contains))
                usingArrowKeys = true;

            bool dPadInUse = currentGamePadState.DPad.Down == ButtonState.Pressed || currentGamePadState.DPad.Up == ButtonState.Pressed || currentGamePadState.DPad.Left == ButtonState.Pressed || currentGamePadState.DPad.Right == ButtonState.Pressed;

            if (usingArrowKeys || dPadInUse)
            {
                CurrentInputDirectionType = InputDirectionType.Full;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                InputDirection.X = -1f;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                InputDirection.X = 1f;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                InputDirection.Y = 1f;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                InputDirection.Y = -1f;
            }
        }

        public static InputDirectionType CurrentInputDirectionType;
        public static Vector2 MousePosition;
        public static KeyboardState currentKeyboardState;
        public static KeyboardState previousKeyboardState;
        public static GamePadState currentGamePadState;
        public static GamePadState previousGamePadState;
        public static MouseState currentMouseState;
        public static MouseState previousMouseState;

        public static bool ExitPressed()
        {
            return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape);
        }

        public static Vector2 InputDirection;
    }
}
