using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenMacroBoard.SDK;
using StreamDeckSharp;

namespace SequenceGenerator.Elgato
{
    public abstract class ElgatoScreen
    {
        public ElgatoButton[] buttons;

        public abstract void Init();

        public abstract void Display();
    }

    public abstract class ElgatoButton
    {
        public KeyBitmap buttonImage;

        public abstract void OnButtonPush();
    }
}
