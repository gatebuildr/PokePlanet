using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
            InputDirection = new Vector2();
        }

        public static void UpdateControlStates(Vector2 origin)
        {
            PreviousGamePadState = CurrentGamePadState;
            PreviousKeyboardState = CurrentKeyboardState;
            PreviousMouseState = CurrentMouseState;
            CurrentKeyboardState = Keyboard.GetState();
            CurrentGamePadState = GamePad.GetState(PlayerIndex.One);
            CurrentMouseState = Mouse.GetState();

            InputDirection = Vector2.Zero;
//            InputDirection.X = 0f;
//            InputDirection.Y = 0f;

            //get mouse state and capture button type and respond button press
            MousePosition.X = CurrentMouseState.X;
            MousePosition.Y = CurrentMouseState.Y;

            bool leftMouseDown = CurrentMouseState.LeftButton == ButtonState.Pressed;
            if (leftMouseDown)
            {
                CurrentInputDirectionType = InputDirectionType.Full;
                Vector2 posDelta = MousePosition - origin;
                posDelta.Normalize();
                InputDirection = posDelta;
                //                posDelta = posDelta * playerMoveSpeed;
                //                player.Position = player.Position + posDelta;
            }

            Vector2 analogDirection = CurrentGamePadState.ThumbSticks.Left;
            if (analogDirection != Vector2.Zero)
            {
                CurrentInputDirectionType = InputDirectionType.Scaled;
                InputDirection = analogDirection;
                InputDirection.Y = -InputDirection.Y;
            }

            Keys[] arrowKeysPressed = CurrentKeyboardState.GetPressedKeys();
            Keys[] arrowKeys = { Keys.Left, Keys.Right, Keys.Up, Keys.Down };
            var usingArrowKeys = arrowKeys.Where(arrowKeysPressed.Contains).Any();

            var dPadInUse = CurrentGamePadState.DPad.Down == ButtonState.Pressed || CurrentGamePadState.DPad.Up == ButtonState.Pressed || CurrentGamePadState.DPad.Left == ButtonState.Pressed || CurrentGamePadState.DPad.Right == ButtonState.Pressed;
            Console.WriteLine("dPadInUse: " + dPadInUse);

            if (usingArrowKeys || dPadInUse)
            {
                CurrentInputDirectionType = InputDirectionType.Full;
            }

            if (CurrentKeyboardState.IsKeyDown(Keys.Left) || CurrentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                InputDirection.X = -1f;
            }
            if (CurrentKeyboardState.IsKeyDown(Keys.Right) || CurrentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                InputDirection.X = 1f;
            }
            if (CurrentKeyboardState.IsKeyDown(Keys.Down) || CurrentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                InputDirection.Y = 1f;
            }
            if (CurrentKeyboardState.IsKeyDown(Keys.Up) || CurrentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                InputDirection.Y = -1f;
            }
        }

        public static InputDirectionType CurrentInputDirectionType;
        public static Vector2 MousePosition;
        public static KeyboardState CurrentKeyboardState;
        public static KeyboardState PreviousKeyboardState;
        public static GamePadState CurrentGamePadState;
        public static GamePadState PreviousGamePadState;
        public static MouseState CurrentMouseState;
        public static MouseState PreviousMouseState;

        public static bool ExitPressed()
        {
            return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape);
        }

        public static Vector2 InputDirection;

        public static Vector2 GetMoveVector(float speed)
        {
            var moveVector = new Vector2(InputDirection.X, InputDirection.Y);

            if (moveVector == Vector2.Zero)
                return moveVector;

            switch (CurrentInputDirectionType)
            {
                case InputDirectionType.Absolute:
                    break;
                case InputDirectionType.Full:
                    moveVector.Normalize();
                    moveVector *= Player.PlayerMoveSpeed;
                    break;
                case InputDirectionType.Scaled:
                    moveVector *= Player.PlayerMoveSpeed;
                    break;
            }
            return moveVector;
        }
    }
}
