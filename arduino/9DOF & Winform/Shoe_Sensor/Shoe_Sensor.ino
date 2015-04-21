#include <Wire.h>
#include <SPI.h>

#include <Adafruit_ADS1015.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_LSM303_U.h>
#include <Adafruit_L3GD20_U.h>
#include <Adafruit_9DOF.h>

Adafruit_9DOF                     dof   = Adafruit_9DOF();
Adafruit_LSM303_Accel_Unified     accel = Adafruit_LSM303_Accel_Unified(30301);
Adafruit_LSM303_Mag_Unified       mag   = Adafruit_LSM303_Mag_Unified(30302);

//calibration data goes here for specific lsm303accel
double Xx = 0.964450, Xy = 0.023183, Xz = 0.025325;
double Yx = 0.023183, Yy = 1.008029, Yz = 0.007651;
double Zx = 0.025325, Zy = 0.007651, Zz = 0.945532;

Adafruit_ADS1115                  ads1(0x48); // Construct an ads1115 at the default address: 0x48
Adafruit_ADS1115                  ads0(0x49); // Construct an ads1115 at the default address: 0x48


//these are the volatile variables that can be modified by the windows GUI
double posRollAlarm = 30;
double posPitchAlarm = 30;
double negRollAlarm = -30;
double negPitchAlarm = -30;

bool RAlarm = false;
bool PAlarm = false;
//allows the use of multiple timing schemes without delays.
unsigned long curMilli = millis();
unsigned long prevMilli = 0, prevMilli1 = 0, prevMilli2 = 0;
unsigned long interval = 75, interval1 = 1000, interval2 = 1000;

double R = 0;
double P = 0;
double H = 0;

double X = 0;
double Y = 0;
double Z = 0;

int adc0, adc1, adc2, adc3, adc4, adc5, adc6, adc7;

double Xaccel = 0, Yaccel = 0, Zaccel = 0;

int buzzer = 6, StatusLED = 13;

String RxString;
char RxBit;

char state = 'x';

void setup()
{
  initSensors();

  Serial.begin(115200);
  ads0.setGain(GAIN_ONE); //1x gain +/-4.096V 1bit=2mV
  ads1.setGain(GAIN_ONE); //1x gain +/-4.096V 1bit=2mV
  pinMode(buzzer, OUTPUT);

}

void SerialRx()
{
  while (Serial.available() > 0)
  {
    delay(3);
    if (Serial.available() > 0)
    {

      // read the incoming byte:
      RxBit = Serial.read();
      if (RxBit != '\n')
      {
        RxString += RxBit;
      }
    }
  }


  switch (RxString[0])
  {
    case 'r':
      state = 'r';
      Serial.println(RxString);
      break;

    case 's':
      posRollAlarm = (RxString.substring(1, 4)).toFloat();
      negRollAlarm = (RxString.substring(6, 10)).toFloat();
      posPitchAlarm = (RxString.substring(12, 15)).toFloat();
      negPitchAlarm = (RxString.substring(17, 21)).toFloat();
      Serial.println(String(posRollAlarm));
      Serial.println(String(negRollAlarm));
      Serial.println(String(posPitchAlarm));
      Serial.println(String(negPitchAlarm));
      Serial.println(RxString);
      break;

    case 'x':
      state = 'x';
      noTone(buzzer);
      RAlarm = false;
      PAlarm = false;
      Serial.println(RxString);
      break;
      //        default:
      //          state = 'x';
      //          noTone(buzzer);

  }
  RxString = "";

}

void loop()
{
  SerialRx();

  curMilli = millis();
  if ((curMilli - prevMilli >= (interval)) && state == 'r')
  {
    prevMilli = curMilli;
    ADCstuff();
    DOF();
    BTOut();
  }



}

void ADCstuff()
{
  adc0 = ads0.readADC_SingleEnded(0);
  adc4 = ads1.readADC_SingleEnded(0);
  adc1 = ads0.readADC_SingleEnded(1);
  adc5 = ads1.readADC_SingleEnded(1);
  adc2 = ads0.readADC_SingleEnded(2);
  adc6 = ads1.readADC_SingleEnded(2);
  adc3 = ads0.readADC_SingleEnded(3);
  adc7 = ads1.readADC_SingleEnded(3);
}

void BTOut()
{
  //  byte running = 192;
  //  byte ADCbit = 128;
  //  byte XYZbit = 64;
  //  byte RPHbit = 32;

  String ADCstr = /*String(ADCbit) +*/ String("*") + ":" + adc0 + ":" + adc1 + ":" + adc2 + ":" + adc3 + ":" + adc4 + ":" + adc5 + ":" + adc6 + ":" + adc7 + ":*";/* + String(ADCbit + 1);*/

  Serial.println(ADCstr);

  String XYZstr = /*String(XYZbit) +*/ String("&") + ":" + Xaccel + ":" + Yaccel + ":" + Zaccel + ":&";

  Serial.println(XYZstr);

  String RPHstr = /*String(RPHbit) +*/ String("~") + ":" + R + ":" + P + ":" + H + ":~";

  Serial.println(RPHstr);

  String output = /*String(running) +*/ ADCstr + XYZstr + RPHstr;

  //Serial.println(output);
}

void DOF()
{
  sensors_event_t      accel_event;
  sensors_event_t      mag_event;
  sensors_vec_t        orientation;

  /* Read the accelerometer and magnetometer */
  accel.getEvent(&accel_event);
  mag.getEvent(&mag_event);

  X = accel_event.acceleration.x;
  Y = accel_event.acceleration.y;
  Z = accel_event.acceleration.z;


  Xaccel = X * Xx + Y * Xy + Z * Xz;
  Yaccel = X * Yx + Y * Yy + Z * Yz;
  Zaccel = X * Zx + Y * Zy + Z * Zz;

  /* Use the new fusionGetOrientation function to merge accel/mag data */
  if (dof.fusionGetOrientation(&accel_event, &mag_event, &orientation))
  {
    R = (orientation.roll);
    P = (orientation.pitch);
    H = (orientation.heading);
  }

  if ((R) >= posRollAlarm && !RAlarm || (R) <= negRollAlarm && !RAlarm)
  {
    noTone(buzzer);
    tone(buzzer, 880);
    RAlarm=true;
  }
  else if ((P) >= posPitchAlarm && !PAlarm || (P) <= negPitchAlarm && !PAlarm)
  {
    noTone(buzzer);
    tone(buzzer, 1000);
    PAlarm=true;
  }

  else if ((R) < posRollAlarm && (R) > negRollAlarm && (P) < posPitchAlarm && (P) > negPitchAlarm)
  {
    noTone(buzzer);
    RAlarm = false;
    PAlarm = false;
  }
//  else
//  {
//    noTone(buzzer);
//    RAlarm = false;
//    PAlarm = false;
//    
//  }

}

void initSensors()
{
  ads0.begin();
  ads1.begin(); //second ads module
  //initialize the sensors
  if (!accel.begin())
  {
    /* There was a problem detecting the LSM303 ... check your connections */
    //    Serial.println(F("Ooops, no LSM303 detected ... Check your wiring!"));
    //while (1);
  }
  if (!mag.begin())
  {
    /* There was a problem detecting the LSM303 ... check your connections */
    //    Serial.println("Ooops, no LSM303 detected ... Check your wiring!");
    //while (1);
  }
}






