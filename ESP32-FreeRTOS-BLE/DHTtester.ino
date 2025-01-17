// Example testing sketch for various DHT humidity/temperature sensors
// Written by ladyada, public domain

// REQUIRES the following Arduino libraries:
// - DHT Sensor Library: https://github.com/adafruit/DHT-sensor-library
// - Adafruit Unified Sensor Lib: https://github.com/adafruit/Adafruit_Sensor
#include <string>
#include <iostream>
#include "DHT.h"
#include "Arduino.h"
#include "BluetoothSerial.h"
String device_name = "Pomiary";
BluetoothSerial SerialBT;

TaskHandle_t task1_handle = NULL;

#define DHTPIN 2     // Digital pin connected to the DHT sensor
// Feather HUZZAH ESP8266 note: use pins 3, 4, 5, 12, 13 or 14 --
// Pin 15 can work but DHT must be disconnected during program upload.

// Uncomment whatever type you're using!
//#define DHTTYPE DHT11   // DHT 11
#define DHTTYPE DHT22   // DHT 22  (AM2302), AM2321
//#define DHTTYPE DHT21   // DHT 21 (AM2301)

// Connect pin 1 (on the left) of the sensor to +5V
// NOTE: If using a board with 3.3V logic like an Arduino Due connect pin 1
// to 3.3V instead of 5V!
// Connect pin 2 of the sensor to whatever your DHTPIN is
// Connect pin 3 (on the right) of the sensor to GROUND (if your sensor has 3 pins)
// Connect pin 4 (on the right) of the sensor to GROUND and leave the pin 3 EMPTY (if your sensor has 4 pins)
// Connect a 10K resistor from pin 2 (data) to pin 1 (power) of the sensor

// Initialize DHT sensor.
// Note that older versions of this library took an optional third parameter to
// tweak the timings for faster processors.  This parameter is no longer needed
// as the current DHT reading algorithm adjusts itself to work on faster procs.
DHT dht(26, DHTTYPE);
long long x = 10000;
float h, t, f;
float prev_h, prevt;
String inf1, inf2,inf3;
void temp(void * parametrs){
  for(;;){
  
   h = dht.readHumidity();
   t = dht.readTemperature();

  }
}

void blu(void * parametrs){
  for(;;){
    // if(h !=0 || t!= 0 )
    //   vTaskSuspend(task1_handle);
   //inf1 = "Wilgotoność " + std::to_string(h);
   // inf2 = "Temperatura: " + std::to_string(t);
    prevt = t;
    prev_h = h;
    SerialBT.print("Wilgotność ");
    SerialBT.println(h);
    SerialBT.print("Temperatura ");
    SerialBT.println(t);
    vTaskDelay(1000/portTICK_PERIOD_MS);
    // if(h !=0 || t!= 0 )
    //   vTaskResume(task1_handle);
    // x--;
  }
}



void setup() {
  Serial.begin(115200);

  SerialBT.begin(device_name);

  dht.begin();

     xTaskCreatePinnedToCore(
    temp,
    "Task 1",
    5000,
    NULL,
    1,
    &task1_handle,
    1
  );

     xTaskCreatePinnedToCore(
    blu,
    "Task 2",
    10000,
    NULL,
    1,
    NULL,
    0
  );
}

void loop() {


}
