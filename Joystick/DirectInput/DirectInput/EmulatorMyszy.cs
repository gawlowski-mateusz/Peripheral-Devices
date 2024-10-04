using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SharpDX.DirectInput;
using System.Runtime.InteropServices;

namespace DirectInputNamespace {
    class EmulatorMyszy {
        private float mouseX = 0, mouseY = 0;  // pozycja X i Y myszki na poczatku ustawiona na 0
        private float predkoscMyszy = 150;  // predkosc myszy ustawiona bazowo na 150
        Joystick joystick;
        bool czyWcisnietyPrawyPrzyciskMyszy = false;  // zmienna do operowania emulacja prawego przycisku myszy
        bool czyJestUzywanyJoystick = false;  // zmienna dzieki ktorej podczas emulowania myszki mozemy zmieniac miedzy kontrolerem, a myszka

        public EmulatorMyszy(Joystick device) {
            this.joystick = device;
        }
        public void WlaczEmulacje() {
            while(true) {
                // zmienna dzieki wyliczymy pozycje joysticka jako myszki
                int inputOffset = (1 << 15) - 1;  // przesuniecie bitowe o 15 pozycji w lewo, pozniej odjecie od tego 1
                int x = joystick.GetCurrentState().X - inputOffset;  // wyliczenie pozycji X, obecna pozycja osi X - 
                int y = joystick.GetCurrentState().Y - inputOffset;  // wyliczenie pozycji Y

                float xOffset = (float)x / inputOffset;  // zmienna dzieki ktorej bedziemy wiedzieli o ile na osi X poruszyl sie wskaznik
                float yOffset = (float)y / inputOffset;

                int slider = joystick.GetCurrentState().Z;  // zczytujemy obecna pozycje slidera w joysticku (os Z)

                predkoscMyszy = 150 * slider / ((1 << 16) - 1);  // dzieki temu mozemy regulowac predkosc myszy za pomoca slidera
                                                                 // im wyzsza wartosc slidera tym wskaznik myszki porusza sie szybciej

                mouseX += xOffset * predkoscMyszy;  // wyliczenie kolejnej pozycji wskaznika na osi X, xOffset moze przyjac wartosci ujemne dlatego mozemy caly czas dodawac do aktualnej pozycji
                mouseY += yOffset * predkoscMyszy;

                // funkcjonalnosc wbudowana w C# oraz sharpDX, flagi dzieki ktorym mozemy zdefiniowac zachowania myszki
                uint flags = (uint)(MouseEventFlags.ABSOLUTE | MouseEventFlags.MOVE);

                if(joystick.GetCurrentState().Buttons[0])  //Lewy przycisk myszy - Fire lub przycisk 1 joysticka
                {
                    flags |= (uint)MouseEventFlags.LEFTDOWN; // lewy przycisk wcisniety
                }
                else {
                    flags |= (uint)MouseEventFlags.LEFTUP;
                }

                // zmienna czyWcisnietyPrawyPrzyciskMyszy jest po to, aby prawy przycisk zarejestrowal sie ze zostal wcisniety tylko raz
                if(joystick.GetCurrentState().Buttons[1])  //Prawy przycisk myszy lub przycisk 2 na joysticku
                {
                    if(czyWcisnietyPrawyPrzyciskMyszy == false) {
                        flags |= (uint)MouseEventFlags.RIGHTUP;
                        czyWcisnietyPrawyPrzyciskMyszy = true;
                    }
                }
                else {
                    if(czyWcisnietyPrawyPrzyciskMyszy == true) {
                        flags |= (uint)MouseEventFlags.RIGHTDOWN;  // prawy przycisk wcisniety
                        czyWcisnietyPrawyPrzyciskMyszy = false;
                    }
                }

                if(joystick.GetCurrentState().Buttons[2]) {  // jezeli wcisniemy przycisk 3 na joysticku
                    czyJestUzywanyJoystick = !czyJestUzywanyJoystick;  // wtedy uzywamy joysticka zamiast myszki
                    mouseX = 0;
                    mouseY = 0;
                }

                if(czyJestUzywanyJoystick) // jezeli uzywamy joysticka 
                    mouse_event(flags, (uint)mouseX, (uint)mouseY, 0, 0);  // za jego pomoca mozemy poruszac kursorem myszki na ekranie
            }

        }
        [Flags]  // tutaj sa zdefiniowane flagi, wartosci z dokumentacji
        public enum MouseEventFlags : uint {
            LEFTDOWN = 0x00000002,  // wcisniety lewy przycisk myszy
            LEFTUP = 0x00000004,
            MOVE = 0x00000001,  // poruszenie myszka
            ABSOLUTE = 0x00008000,  // pozycja absolute
            RIGHTDOWN = 0x00000008,  // wcisniety prawy przycisk myszy
            RIGHTUP = 0x00000010
        }

        [DllImport("user32.dll")]  // dll bez ktorego moga byc problemy w dzialaniu emulatora
        // metoda ktora odczytuja zdarzenia myszki, wszystkie flagi oraz jej pozycje na ekranie
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
    }
}
