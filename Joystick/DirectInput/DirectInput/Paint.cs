using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SharpDX.DirectInput;

namespace DirectInputNamespace {
    class Paint {
        // ustawienie poczatkowych wartosci
        private float posX = 0, posY = 0;
        private float mouseSpeed = 100;
        private Canvas canvas;
        private Joystick joystick;
        public Paint(Canvas canvas, Joystick device) {
            this.canvas = canvas;
            this.joystick = device;
        }
        public void InputThread() {
            canvas.WyczyscEkran();  // przygotowujemy nasz ekran do rysowania
            while(canvas.Visible) { // jezeli canvas jest widoczny, czyli po prostu istnieje
                int inputOffset = (1 << 15) - 1;
                int x = joystick.GetCurrentState().X - inputOffset;
                int y = joystick.GetCurrentState().Y - inputOffset;

                float xOffset = (float)x / inputOffset;
                float yOffset = (float)y / inputOffset;

                int slider = joystick.GetCurrentState().Z;
                mouseSpeed = 150 / ((1 << 16) - 1) + 1;
                posX += xOffset * mouseSpeed;
                posY += yOffset * mouseSpeed;
                // wyzej tak jak wszedzie, odczytywanie i wyliczanie wartosci osi X, Y, Z joysticka

                // zebysmy nie wyszli poza nasze okno
                if(posX > canvas.SzerokoscOkna) {
                    posX = canvas.SzerokoscOkna;  
                }
                if(posX < 0) {
                    posX = 0;
                }
                if(posY > canvas.WysokoscOkna) {
                    posY = canvas.WysokoscOkna;
                }
                if(posY < 0) {
                    posY = 0;
                }

                // uzywamy przycisku 1 na joysticku do rysowania
                if(joystick.GetCurrentState().Buttons[0]) {
                    slider /= 1000;
                    // slider w tym przypadku zmienia wartosc 'r' elipsy/kola, czyli dzieki temu mozemy rysowac 
                    // ciensze lub gruszbe linie, powiekszajac lub zmniejszajac nasz pedzel
                    canvas.Rysuj(posX, posY, slider);
                }
                // jezeli wcisniemy przycisk 3 na joysticku, czyscimy ekran
                if(joystick.GetCurrentState().Buttons[2]) {
                    canvas.WyczyscEkran();
                }
                canvas.poruszWskaznikiem(posX, posY);  // za kazdym razem mozemy po prostu poruszac wskaznikiem/pedzlem

                Thread.Sleep(10); // jak nic nie robimy, to usypiamy watek, mozemy go potrzebowac
            }
        }
    }
}
