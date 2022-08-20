# Describes body language/attitude/gesture detected from a body.
Header header

# Additional gestures might be added in the future, please open
# issues/pull requests to suggest new ones.

uint8 HANDS_ON_FACE = 1
uint8 ARMS_CROSSED = 2
uint8 LEFT_HAND_RAISED = 3
uint8 RIGHT_HAND_RAISED = 4
uint8 BOTH_HANDS_RAISED = 5
uint8 WAVING = 6 # eg, greeting someone with the hand

uint8 OTHER = 0

uint8 gesture # one of the above constants
