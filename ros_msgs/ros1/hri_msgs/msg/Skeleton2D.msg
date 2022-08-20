# This message contains a list of skeletal keypoints 
# (0, 0) is at top-left corner of image

Header header        # Header timestamp should be acquisition time of the original image

# skeletal key Points naming
# Follows OpenPose coco model convention
# https://github.com/CMU-Perceptual-Computing-Lab/openpose/blob/master/doc/output.md#pose-output-format-coco

uint8 NOSE = 0
uint8 NECK = 1
uint8 RIGHT_SHOULDER = 2
uint8 RIGHT_ELBOW = 3
uint8 RIGHT_WRIST = 4
uint8 LEFT_SHOULDER = 5
uint8 LEFT_ELBOW = 6
uint8 LEFT_WRIST = 7
uint8 RIGHT_HIP = 8
uint8 RIGHT_KNEE = 9
uint8 RIGHT_ANKLE = 10
uint8 LEFT_HIP = 11
uint8 LEFT_KNEE = 12
uint8 LEFT_ANKLE = 13
uint8 LEFT_EYE = 14
uint8 RIGHT_EYE = 15
uint8 LEFT_EAR = 16
uint8 RIGHT_EAR = 17


NormalizedPointOfInterest2D[] skeleton


