# Represents the gaze direction from a particular person (sender ID) to the
# person that is being gazed to (receiver ID).
#
# If the sender or receiver IDs are empty, it means that the gaze respectively
# originates or is targeted to the robot itself.
Header header

string sender
string receiver
