cmake_minimum_required(VERSION 3.26)
project(GPS)

set(CMAKE_CXX_STANDARD 14)
set (CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -static-libstdc++ -static-libgcc")
set(CMAKE_EXE_LINKER_FLAGS "-static")


include_directories(.)

add_executable(GPS
        GPS.cpp
        Serial.cpp
        Serial.h)
