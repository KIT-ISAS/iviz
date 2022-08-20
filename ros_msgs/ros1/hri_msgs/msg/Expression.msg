# Represents a human facial expression, either in a categorical way, or
# using the valence/arousal model of emotions
Header header

# the list of expressions is based on Chambers MSc thesis, Bristol Robotics Lab 2020, and includes the six basic emotions in Eckman's model.
#
# Note that a node making use of this message definition is not supposed or
# expected to handle all listed expressions.
#
# This list might change based on future needs/requests.

string NEUTRAL="neutral"
string ANGRY="angry"
string SAD="sad"
string HAPPY="happy"
string SURPRISED="surprised"
string DISGUSTED="disgusted"
string SCARED="scared"
string PLEADING="pleading"
string VULNERABLE="vulnerable"
string DESPAIRED="despaired"
string GUILTY="guilty"
string DISAPPOINTED="disappointed"
string EMBARRASSED="embarrassed"
string HORRIFIED="horrified"
string SKEPTICAL="skeptical"
string ANNOYED="annoyed"
string FURIOUS="furious"
string SUSPICIOUS="suspicious"
string REJECTED="rejected"
string BORED="bored"
string TIRED="tired"
string ASLEEP="asleep"
string CONFUSED="confused"
string AMAZED="amazed"
string EXCITED="excited"

string expression

# Valence/Arousal model of emotions
float32 valence # from -1.0 to +1.0
float32 arousal # from -1.0 to +1.0

float32 confidence # from 0.0 to 1.0
