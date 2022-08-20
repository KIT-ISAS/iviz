# Discrete engagement levels of a person with respect to the robot
# It is for instance published on /humans/persons/person_id/engagement_status 
# to access to the engagement level of a detected human. 
# It will output one of the five following levels: unknown, disengaged, 
# engaging, engaged and disengaging.
Header header  

# unknown: no information is provided about the engagement level 
uint8 UNKNOWN=0
# disengaged: the human has not looked in the direction of the robot
uint8 DISENGAGED=1
# engaging: the human has started to look in the direction of the robot
uint8 ENGAGING=2
# engaged: the human is fully engaged with the robot
uint8 ENGAGED=3
# disengaging: the human has started to look away from the robot
uint8 DISENGAGING=4

uint8 level

