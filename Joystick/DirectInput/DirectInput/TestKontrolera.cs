using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SharpDX.DirectInput;

namespace DirectInputNamespace {
    class TestKontrolera {
        private Canvas canvas;
        private Joystick joystick;
        public TestKontrolera(Canvas canvas, Joystick joystick) {
            this.canvas = canvas;
            this.joystick = joystick;
        }
        public void InputThread() {
            canvas.showCross = true;  // aby pokazal sie wskaznik
            while(canvas.Visible) {
                int x = joystick.GetCurrentState().X;  // zczytujemy obecna pozycje X joysticka
                int y = joystick.GetCurrentState().Y;  // zczytujemy obecna pozycje Y joysticka
                // zmienna aby ograniczyc mozliwosci poruszania sie wskaznika
                int maxInput = (1 << 16) - 1;  // przesuniecie bitowe o 16 pozycji w lewo i pozniej odjecie 1
                // wywolanie metody poruszania wskaznikiem (pozycja x, pozycja y), ciagle wyliczana
                canvas.poruszWskaznikiem(x * canvas.SzerokoscOkna / maxInput, y * canvas.WysokoscOkna / maxInput);
                Thread.Sleep(10);
            }
        }
    }

}
