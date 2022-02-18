##
## Severity level constants
##
byte DEBUG=1 #debug level
byte INFO=2  #general level
byte WARN=4  #warning level
byte ERROR=8 #error level
byte FATAL=16 #fatal/critical level
##
## Fields
##
Header header
byte level
string name # name of the node
string msg # message 
string file # [Ignore]
string function # [Ignore]
uint32 line # line the message came from
#string[] topics # topic names that the node publishes
string[] topics # [Ignore]
