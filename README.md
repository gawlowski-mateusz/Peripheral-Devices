# Peripheral-Devices

This repository contains code and project files related to various IoT and embedded systems applications using ESP32, GPS, Joystick, Camera, SIM card reader, and EAN code reader. Below is an overview of the components and functionalities implemented in this project.

## Features

### 1. ESP32 Microcontroller
The ESP32 is a microcontroller with built-in Wi-Fi and Bluetooth capabilities, used here to control various components such as sensors and peripherals, and for Bluetooth communication. The code includes examples of sensor data acquisition and Bluetooth transmission.

### 2. GPS System
A GPS module is interfaced with ESP32 for real-time position tracking. The system uses GPS data (e.g., longitude, latitude) to determine the current location. NMEA sentence parsing is also implemented for data extraction.

### 3. Joystick Control
A joystick module is used as an input device for controlling movement or navigating menus. It is connected to the PC and processed as an analog input for movement control in applications.

### 4. Camera Integration
An external camera is connected to PC, allowing capturing of images and video. The code includes functions to:
- Capture photos
- Record video
- Detect motion in the video stream
- Adjust brightness, contrast, and saturation

### 5. SIM Card Reader
The project also implements SIM card reading for GSM-based communication. This allows for network connectivity and sending/receiving messages using a SIM800L module.

### 6. EAN Code Reader
The code includes a barcode reader using the EAN-13 standard for reading product barcodes. The barcode scanner processes 1D codes and outputs the corresponding product information.
