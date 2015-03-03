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

//#define Xcal accel_event.acceleration.x*Xx+accel_event.acceleration.y*Xy+accel_event.acceleration.z*Xz
//#define Ycal accel_event.acceleration.x*Yx+accel_event.acceleration.y*Yy+accel_event.acceleration.z*Yz
//#define Zcal accel_event.acceleration.x*Zx+accel_event.acceleration.y*Zy+accel_event.acceleration.z*Zz

Adafruit_ADS1115                  ads0(0x48); // Construct an ads1115 at the default address: 0x48
Adafruit_ADS1115                  ads1(0x48); // Construct an ads1115 at the default address: 0x48

//
//#include <Adafruit_BLE_UART.h>
//#define ADAFRUITBLE_REQ 10
//#define ADAFRUITBLE_RDY 3     // This should be an interrupt pin, on Uno thats #2 or #3
//#define ADAFRUITBLE_RST 9
//
//Adafruit_BLE_UART BTLEserial = Adafruit_BLE_UART(ADAFRUITBLE_REQ, ADAFRUITBLE_RDY, ADAFRUITBLE_RST);

//these are the volatile variables that can be modified by the windows GUI
double rollAlarm = 30;
double pitchAlarm = 30;

//allows the use of multiple timing schemes without delays.
unsigned long curMilli = millis();
unsigned long prevMilli = 0, prevMilli1 = 0, prevMilli2 = 0;
unsigned long interval = 50,interval1 = 1000, interval2 = 1000;

double R = 0;
double P = 0;
double H = 0;

double X = 0;
double Y = 0;
double Z = 0;

int16_t adc0, adc1;//, adc2, adc3, adc4, adc5, adc6, adc7;

double Xaccel = 0, Yaccel = 0, Zaccel = 0;

char type = 'h';

void setup()
{
  initSensors();
//  BTLEserial.setDeviceName("LCD9Ard"); /* 7 characters max! */

//  BTLEserial.begin(115200);
  Serial.begin(115200);
  ads0.setGain(GAIN_ONE); //1x gain +/-4.096V 1bit=2mV
  //ads1.setGain(GAIN_ONE); //1x gain +/-4.096V 1bit=2mV

}
//aci_evt_opcode_t laststatus = ACI_EVT_DISCONNECTED;
//aci_evt_opcode_t status = BTLEserial.getState();

void loop()
{
  curMilli = millis();
//
//  BTLEserial.pollACI();
//
//  // Ask what is our current status
//  status = BTLEserial.getState();
//
//  // If the status changed....
//  if (status != laststatus) {
//    // print it out!
//    if (status == ACI_EVT_DEVICE_STARTED) {
//      //Serial.println(F("* Advertising started"));
//    }
//    if (status == ACI_EVT_CONNECTED) {
//      //Serial.println(F("* Connected!"));
//
//    }
//    if (status == ACI_EVT_DISCONNECTED) {
//      //Serial.println(F("* Disconnected or advertising timed out"));
//    }
//    // OK set the last status change to this one
//    laststatus = status;
//  }
//  //  if (status == ACI_EVT_CONNECTED) {
//  //    // Lets see if there's any data for us!
//  //    //    if (BTLEserial.available()) {
//  //    //      //  Serial.print("* "); 
//  //    //      //  Serial.print(BTLEserial.available()); 
//  //    //      //  Serial.println(F(" bytes available from BTLE"));
//  //    //    }
//  //    // OK while we still have something to read, get a character and print it out
//  //    while (BTLEserial.available()) {
//  //      char c = BTLEserial.read();
//  //      BTLEserial.write(c);
//  //      if(c=='h'||c=='m')
//  //      {
//  //        type = c;
//  //      }
//  //    }
//  //  }
  if (curMilli - prevMilli >= (interval))
  {
    prevMilli = curMilli;
    ADCstuff();
    DOF();
    BTLEOut();

  }
}

void ADCstuff()
{

  adc0 = ads0.readADC_SingleEnded(0);
  adc1 = 12000;//ads0.readADC_SingleEnded(1);
  //adc2 = ads0.readADC_SingleEnded(2);
  //adc3 = ads0.readADC_SingleEnded(3);
  //adc4 = ads1.readADC_SingleEnded(0);
  //adc5 = ads1.readADC_SingleEnded(1);
  //adc6 = ads1.readADC_SingleEnded(2);
  //adc7 = ads1.readADC_SingleEnded(3); 


}

void BTLEOut()
{
//  char buf[10];
//  dtostrf(R, 6,2,buf);
//  String Rstr = buf;
//  dtostrf(P, 6,2,buf);
//  String Pstr = buf;
//  dtostrf(H, 6,2,buf);
//  String Hstr = buf;
  String RPHstr = String("~")+":"+R+":"+P+":"+H+":"+"~";

  String ADCstr = String("*")+":"+adc0+":"+adc1+":"+"*";

  String XYZstr = String("&")+":"+Xaccel+":"+Yaccel+":"+Zaccel+":"+"&";
//  if (status == ACI_EVT_CONNECTED)
//  {
////    BTLEserial.print("~");
//    BTLEserial.print(RPHstr);
////    BTLEserial.println("~");
//    BTLEserial.print(ADCstr);
////    BTLEserial.print(adc0);
////    BTLEserial.print(":"); 
////    BTLEserial.print(adc1); 
////    BTLEserial.println("*");
//    
//  }
//  Serial.print("~");  
  Serial.println(RPHstr);  
//  Serial.println("~");

//  char buff[
//    String ADC0 = 

  Serial.println(XYZstr);

  Serial.println(ADCstr);
//  Serial.println(adc0);
//  Serial.print("AIN1: "); 
//  Serial.println(adc1);

}

void DOF()
{
  sensors_event_t      accel_event;
  sensors_event_t      mag_event;
  sensors_vec_t        orientation;

  /* Read the accelerometer and magnetometer */
  accel.getEvent(&accel_event);
  mag.getEvent(&mag_event);

//  if(accel.getEvent(&accel_event))
//  {
  X = accel_event.acceleration.x;
  Y = accel_event.acceleration.y;
  Z = accel_event.acceleration.z;
  
  
    Xaccel = X*Xx+Y*Xy+Z*Xz;
//    Serial.println(X*Xx+Y*Xy+Z*Xz);
    Yaccel = X*Yx+Y*Yy+Z*Yz;
    Zaccel = X*Zx+Y*Zy+Z*Zz;
//  }

  /* Use the new fusionGetOrientation function to merge accel/mag data */
  if (dof.fusionGetOrientation(&accel_event, &mag_event, &orientation))
  {
    R = (orientation.roll);
    P = (orientation.pitch);
    H = (orientation.heading);
  }
}

void initSensors()
{ 
  ads0.begin();

  //ads1.begin(); //second ads module
  //initialize the sensors
  if(!accel.begin())
  {
    /* There was a problem detecting the LSM303 ... check your connections */
    //    Serial.println(F("Ooops, no LSM303 detected ... Check your wiring!"));
    while(1);
  }
  if(!mag.begin())
  {
    /* There was a problem detecting the LSM303 ... check your connections */
    //    Serial.println("Ooops, no LSM303 detected ... Check your wiring!");
    while(1);
  }
}





