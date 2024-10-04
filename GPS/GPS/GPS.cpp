#include <iostream>
#include <windows.h>
#include <sstream>
#include "Serial.h"
//#include "stdafx.h"
#include <tchar.h>
#include <fstream>

using namespace std;

#define RX_BUFFSIZE 300 // buffer size definition

bool switch_function() {
    char option;

    cout << "\n==== MAIN MENU ====" << endl;
    cout << "1. Load data from .txt file" << endl;
    cout << "2. Load data from chosen Serial port" << endl;
    cout << "0. Exit" << endl;
    cout << "Choose option: >> ";
    cin >> option;
    cout << endl;

    do {
        switch (option) {
            case '1':
                return true;

            case '2':
                return false;
        }
    } while (option != '0');
}

int _tmain(int argc, _TCHAR *argv[]) {

    bool run_type = switch_function();

    try {
        if (!run_type) {
            // Get information about Port
            string port;

            cout << "Enter the COM port of the GPS device [ex. COM7]: ";
            cin >> port;
            cout << "Opening selected port..." << endl;
            port += ":";

            TCHAR *portCom = new TCHAR[port.size() + 1];
            portCom[port.size()] = 0;
            copy(port.begin(), port.end(), portCom);

            // Port configure
            tstring commPortName(portCom);
            // Connect with selected port
            Serial serial(commPortName);

            cout << "Port opened successfully" << endl;
            cout << "Processing..." << endl;

            char array[RX_BUFFSIZE]; // buffer with GPS data

            // GPS data read
            int b = -1;
            int charsRead;
            do {
                b++;
                charsRead = NULL;

                for (int k = 0; k < 100; k++) array[k] = NULL;
			    charsRead = serial.read(array, RX_BUFFSIZE);

                Sleep(1000);
            } while (b < 10);
        } else {
            char array[1000];

            std::ifstream file("up_lab3.txt");  // choose file name in need
            cout << "File loaded successfully" << endl;
            cout << "Processing..." << endl;

            if (!file.is_open()) {
                std::cout << "File can not be read" << std::endl;
                return 1;
            }

            // GPS data read
            int b = -1;
            int charsRead;
            do {
                b++;
                charsRead = NULL;

                for (int k = 0; k < 100; k++) array[k] = NULL;

                Sleep(1000);
            } while (b < 10);

            if (file.is_open()) { // file check
                int i = 0;
                char symbol;

                // Odczytanie symboli z pliku tekstowego i zapisanie do tablicy tab
                while (file.get(symbol) && i < 1000) {
                    array[i] = symbol;
                    i++;
                }
                file.close();
        }

        string time, latitude, longitude;   // NMEA data variables

        char GGA[72];

            // search $GPGGA sequence - Global Positioning System Fix Data and get its data
            for (int i = 0; i < 1000; i++) {

                if (array[i] == '$' && array[i + 1] == 'G' && array[i + 2] == 'P' && array[i + 3] == 'G' &&
                    array[i + 4] == 'G' && array[i + 5] == 'A') {
                    cout << "\nGGA found at position: " << i << endl;
                    int index = i;

                    for (char &g: GGA) {
                        g = array[index];
                        index++;
                        cout << g;
                    }

                    index = 0;

                    for (int j = 0; j < 72; j++) {

                        if (j == 7) {
                            for (int k = 0; k < 6; ++k) {
                                time += GGA[j + k];
                            }
                        }

                        if (j == 18) {
                            for (int k = 0; k < 9; ++k) {
                                latitude += GGA[j + k];
                            }
                        }

                        if (j == 30) {
                            for (int k = 0; k < 9; ++k) {
                                longitude += GGA[j + k];
                            }
                        }
                    }
                    break;
                }
            }

//      EXAMPLES
//      https://www.google.com/maps/place/51°06'32.2"N+17°03'38.2"E/
//      https://www.google.com/maps/place/51%C2%B006'32.2%22N+17%C2%B003'38.2%22E/
//      $GPGGA,155841.000,5106.5358,N,01703.6373,E,1,9,0.92,154.4,M,42.6,M,,*5B

            string google = "https://www.google.pl/maps/place/";

            size_t index = latitude.find_first_not_of('0');

            if (index != std::string::npos) {
                latitude = latitude.substr(index);
            } else {
                latitude = "0";
            }

            index = longitude.find_first_not_of('0');

            if (index != std::string::npos) {
                longitude = longitude.substr(index);
            } else {
                longitude = "0";
            }

            cout << "\nTime: " << time;
            cout << "\nLatitude: " << latitude;
            cout << "\nLongitude: " << longitude;

            index = latitude.find('.') - 2;

            for (int j = 0; j < latitude.size(); ++j) {
                if (j == index) {
                    google += " ";
                    google += latitude[j];
                } else {
                    google += latitude[j];
                }
            }

            google += ",";

            index = longitude.find('.') - 2;

            for (int j = 0; j < longitude.size(); ++j) {
                if (j == index) {
                    google += " ";
                    google += longitude[j];
                } else {
                    google += longitude[j];
                }
            }

            google += '/';

            cout << "\nGoogle Maps (please copy and paste following line): " << google << endl << endl;
        }

        fseek(stdin, 0, SEEK_END);
        getchar();

        return 0;
    } catch (const char *error) {
        cout << error << endl;
    }
}