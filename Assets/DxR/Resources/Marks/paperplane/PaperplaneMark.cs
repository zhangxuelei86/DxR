using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DxR
{
    /// <summary>
    /// This is the class for point mark which enables setting of channel
    /// values which may involve calling custom scripts. The idea is that 
    /// in order to add a custom channel, the developer simply has to implement
    /// a function that takes in the "channel" name and value in string format
    /// and performs the necessary changes under the SetChannelValue function.
    /// </summary>
    public class PaperplaneMark : Mark
    {
        public Vector3 origOrientation;
        public Vector3 curOrientation;

        public PaperplaneMark() : base()
        {
            origOrientation = curOrientation = Vector3.left;
        }
        
        public override void SetChannelValue(string channel, string value)
        {
            switch (channel)
            {
                case "length":
                    base.SetMaxSize(value);
                    break;
                 case "xorient":
                    SetOrientation(value, 0);
                    break;
                case "yorient":
                    SetOrientation(value, 1);
                    break;
                case "zorient":
                    SetOrientation(value, 2);
                    break;
                default:
                    base.SetChannelValue(channel, value);
                    break;
            }
        }
        
        // vectorIndex = 0 for x, 1 for y, 2 for z
        private void SetOrientation(string value, int vectorIndex)
        {
            // Set target direction dim to normalized size.
            Vector3 targetOrient = Vector3.zero;
            targetOrient[vectorIndex] = float.Parse(value);
            targetOrient.Normalize();

            // Copy coordinate to current orientation and normalize.
            curOrientation[vectorIndex] = targetOrient[vectorIndex];
            curOrientation.Normalize();

            Quaternion rotation = Quaternion.FromToRotation(origOrientation, curOrientation);
            transform.rotation = rotation;
        }
    }
}
