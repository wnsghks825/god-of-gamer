using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GodOfGamer
{

    public class Level
    {
        public uint level { get; private set; }

        public Level()
        {
            level = uint.MinValue;
        }

        public void IncreaseLevel()
        {
            level++;
        }
    }
}

