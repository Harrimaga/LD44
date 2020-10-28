using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace CottonCandy
{
    public class Hotkey
    {

        public List<Keys> keys;
        public List<CButtons> ckeys;
        public bool OnlyOnce = false, prev = false;

        /// <summary>
        /// Don't use this one, use the one with the bool
        /// </summary>
        [Obsolete]
        public Hotkey()
        {

        }

        /// <summary>
        /// Enum with all of the Controller buttons that can be used. Missing one? just add one here and make sure to also add it in the isdown method!
        /// </summary>
        public enum CButtons
        {
            A,
            B,
            X,
            Y,
            BumperLeft,
            BumperRight,
            DpadL,
            DpadR,
            DpadU,
            DpadD,
            LeftStickToL,
            LeftStickToR,
            LeftStickUp,
            LeftStickDown,
            RightStickToL,
            RightStickToR,
            RightStickUp,
            RightStickDown,
            LeftTrigger,
            RightTrigger,
            Menu,
            None

        }

        /// <summary>
        /// Use the add method to add hotkeys
        /// </summary>
        /// <param name="onlyonce">Set this true if you want it to only return true once per click</param>
        public Hotkey(bool onlyonce)
        {
            keys = new List<Keys>();
            ckeys = new List<CButtons>();
            OnlyOnce = onlyonce;
        }

        /// <summary>
        /// Gets if the hotkey is down
        /// </summary>
        public bool IsDown()
        {
            foreach (Keys k in keys) //Checking if one of the Keyboard keys is down
            {
                if (Keyboard.GetState().IsKeyDown(k))
                {
                    if (OnlyOnce)
                    {
                        if (prev)
                        {
                            return false;
                        }
                        prev = true;
                    }
                    return true;
                }
            }
            foreach (CButtons c in ckeys) //checking if one of the controller hotkeys is down
            {
                switch (c)
                {
                    case (CButtons.A):
                        if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.B):
                        if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.X):
                        if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.Y):
                        if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.BumperLeft):
                        if (GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.BumperRight):
                        if (GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.DpadD):
                        if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.DpadL):
                        if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.DpadR):
                        if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.DpadU):
                        if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.Menu):
                        if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.LeftStickToL):

                        if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -StickSensitivity)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.LeftStickToR):
                        if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > StickSensitivity)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.LeftStickUp):

                        if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > StickSensitivity)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.LeftStickDown):
                        if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -StickSensitivity)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.RightStickToL):

                        if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X < -StickSensitivity)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.RightStickToR):
                        if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X > StickSensitivity)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.RightStickDown):

                        if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y < -StickSensitivity)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.RightStickUp):
                        if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y > StickSensitivity)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.LeftTrigger):
                        if (GamePad.GetState(PlayerIndex.One).Triggers.Left >= 0.95)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                    case (CButtons.RightTrigger):
                        if (GamePad.GetState(PlayerIndex.One).Triggers.Right >= 0.95)
                        {
                            if (OnlyOnce)
                            {
                                if (prev)
                                {
                                    return false;
                                }
                                prev = true;
                            }
                            return true;
                        }
                        break;
                }
            }
            prev = false;
            return false;
        }

        /// <summary>
        /// Adds a key. Returns itself so you call it again in the same statement, for example: Hotkey h = new Hotkey().add(Keys.k).add(Hotkey.CButtons.A);
        /// </summary>
        /// <param name="k">The key to be added</param>
        public Hotkey add(Keys k)
        {
            foreach (Keys kk in keys)
            {
                if (kk == k)
                {
                    return this;
                }
            }
            keys.Add(k);
            return this;
        }

        /// <summary>
        /// Adds a key. Returns itself so you call it again in the same statement, for example: Hotkey h = new Hotkey().add(Keys.k).add(Hotkey.CButtons.A);
        /// </summary>
        /// <param name="k">The key to be added</param>
        public Hotkey add(CButtons c)
        {
            foreach (CButtons cc in ckeys)
            {
                if (cc == c)
                {
                    return this;
                }
            }
            ckeys.Add(c);
            return this;
        }

        /// <summary>
        /// Removes the given key
        /// </summary>
        public void remove(Keys k)
        {
            keys.Remove(k);
        }

        /// <summary>
        /// Removes the given key
        /// </summary>
        public void remove(CButtons c)
        {
            ckeys.Remove(c);
        }

        /// <summary>
        /// Gets a string with all the keys that are mapped to this hotkey
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            string s = "";
            bool first = true;
            foreach (Keys k in keys)
            {
                if (!first)
                {
                    s += " ";
                }
                first = false;
                s += Enum.GetName(typeof(Keys), k);
            }
            foreach (CButtons c in ckeys)
            {
                if (!first)
                {
                    s += " ";
                }
                first = false;
                s += Enum.GetName(typeof(CButtons), c);
            }
            return s;
        }

        /// <summary>
        /// Checks if the given key is part of this hotkey
        /// </summary>
        /// <param name="k">The key that you want to check</param>
        public bool hasKey(Keys k)
        {
            foreach (Keys kk in keys)
            {
                if (kk == k)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the given Controller button is part of this hotkey
        /// </summary>
        /// <param name="k">The Controller button that you want to check</param>
        public bool hasKey(CButtons cb)
        {
            foreach (CButtons c in ckeys)
            {
                if (cb == c)
                {
                    return true;
                }
            }
            return false;
        }

        //Static stuff-----------------------------------------------------------------------------------------------------------------
        private static KeyboardState oldKeyboardState = new KeyboardState(), currentKeyboardState = new KeyboardState();
        public static double StickSensitivity = 0.5;

        /// <summary>
        /// Gives the button that is pressed that update cycle. For setting new hotkeys. FIRST CALL SHOULD BE IGNORED
        /// </summary>
        /// <returns>The Key. Returns Keys.None if none is pressed</returns>
        public static Keys UpdateInput()
        {
            oldKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            Keys[] pressedKeys;
            pressedKeys = currentKeyboardState.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (oldKeyboardState.IsKeyDown(key))
                {
                    return key;
                }
            }
            return Keys.None;
        }

        /// <summary>
        /// For setting new hotkeys but controller buttons
        /// </summary>
        /// <returns>A controller buttons. Return CButton.None if none is pressed</returns>
        public static CButtons GetControllerButton()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                return CButtons.A;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
            {
                return CButtons.B;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                return CButtons.X;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
            {
                return CButtons.Y;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed)
            {
                return CButtons.BumperLeft;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed)
            {
                return CButtons.BumperRight;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
            {
                return CButtons.DpadD;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
            {
                return CButtons.DpadL;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
            {
                return CButtons.DpadR;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
            {
                return CButtons.DpadU;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                return CButtons.DpadU;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -StickSensitivity)
            {
                return CButtons.LeftStickToL;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > StickSensitivity)
            {
                return CButtons.LeftStickToR;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -StickSensitivity)
            {
                return CButtons.LeftStickDown;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > StickSensitivity)
            {
                return CButtons.LeftStickUp;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X < -StickSensitivity)
            {
                return CButtons.RightStickToL;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X > StickSensitivity)
            {
                return CButtons.RightStickToR;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y < -StickSensitivity)
            {
                return CButtons.RightStickDown;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y > StickSensitivity)
            {
                return CButtons.RightStickUp;
            }
            if (GamePad.GetState(PlayerIndex.One).Triggers.Left >= 0.95)
            {
                return CButtons.LeftTrigger;
            }
            if (GamePad.GetState(PlayerIndex.One).Triggers.Right >= 0.95)
            {
                return CButtons.RightTrigger;
            }
            return CButtons.None;
        }

    }
}
