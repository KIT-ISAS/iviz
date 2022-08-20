# This message contains a list of facial features detected on a face
# (0, 0) is at top-left corner of image
# Features' coordinates are expressed in normalised pixel coordinates 
# (in the range [0., 1.]), from the top-left corner.
Header header

# Facial landmarks naming
# Follows dlib and OpenPose convention
# https://github.com/CMU-Perceptual-Computing-Lab/openpose/blob/master/doc/02_output.md#face-output-format
uint8 RIGHT_EAR=0
uint8 RIGHT_PROFILE_1=1
uint8 RIGHT_PROFILE_2=2
uint8 RIGHT_PROFILE_3=3
uint8 RIGHT_PROFILE_4=4
uint8 RIGHT_PROFILE_5=5
uint8 RIGHT_PROFILE_6=6
uint8 RIGHT_PROFILE_7=7

uint8 MENTON=8

uint8 LEFT_EAR=16
uint8 LEFT_PROFILE_1=15
uint8 LEFT_PROFILE_2=14
uint8 LEFT_PROFILE_3=13
uint8 LEFT_PROFILE_4=12
uint8 LEFT_PROFILE_5=11
uint8 LEFT_PROFILE_6=10
uint8 LEFT_PROFILE_7=9

uint8 RIGHT_EYEBROW_OUTSIDE=17
uint8 RIGHT_EYEBROW_1=18
uint8 RIGHT_EYEBROW_2=19
uint8 RIGHT_EYEBROW_3=20
uint8 RIGHT_EYEBROW_INSIDE=21

uint8 RIGHT_EYE_OUTSIDE=36
uint8 RIGHT_EYE_TOP_1=37
uint8 RIGHT_EYE_TOP_2=38
uint8 RIGHT_EYE_INSIDE=39
uint8 RIGHT_EYE_BOTTOM_1=41
uint8 RIGHT_EYE_BOTTOM_2=40

uint8 RIGHT_PUPIL=68

uint8 LEFT_EYEBROW_OUTSIDE=26
uint8 LEFT_EYEBROW_1=25
uint8 LEFT_EYEBROW_2=24
uint8 LEFT_EYEBROW_3=23
uint8 LEFT_EYEBROW_INSIDE=22

uint8 LEFT_EYE_OUTSIDE=45
uint8 LEFT_EYE_TOP_1=44
uint8 LEFT_EYE_TOP_2=43
uint8 LEFT_EYE_INSIDE=42
uint8 LEFT_EYE_BOTTOM_1=46
uint8 LEFT_EYE_BOTTOM_2=47

uint8 LEFT_PUPIL=69

uint8 SELLION=27
uint8 NOSE_1=28
uint8 NOSE_2=29
uint8 NOSE=30

uint8 NOSTRIL_1=31
uint8 NOSTRIL_2=32
uint8 NOSTRIL_3=33
uint8 NOSTRIL_4=34
uint8 NOSTRIL_5=35

uint8 MOUTH_OUTER_RIGHT=48
uint8 MOUTH_OUTER_TOP_1=49
uint8 MOUTH_OUTER_TOP_2=50
uint8 MOUTH_OUTER_TOP_3=51
uint8 MOUTH_OUTER_TOP_4=52
uint8 MOUTH_OUTER_TOP_5=53
uint8 MOUTH_OUTER_LEFT=54
uint8 MOUTH_OUTER_BOTTOM_1=59
uint8 MOUTH_OUTER_BOTTOM_2=58
uint8 MOUTH_OUTER_BOTTOM_3=57
uint8 MOUTH_OUTER_BOTTOM_4=56
uint8 MOUTH_OUTER_BOTTOM_5=55

uint8 MOUTH_INNER_RIGHT=60
uint8 MOUTH_INNER_TOP_1=61
uint8 MOUTH_INNER_TOP_2=62
uint8 MOUTH_INNER_TOP_3=63
uint8 MOUTH_INNER_LEFT=64
uint8 MOUTH_INNER_BOTTOM_1=67
uint8 MOUTH_INNER_BOTTOM_2=66
uint8 MOUTH_INNER_BOTTOM_3=65

NormalizedPointOfInterest2D[] landmarks # the ordering of landmarks must follow the indices defined above

uint32 height         # image height in pixels, that is, number of rows
uint32 width          # image width in pixels, that is, number of columns
