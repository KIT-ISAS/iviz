# This contains the top-leftmost and bottom-rightmost coordinates of a region of interest (typically in an image)
# the coordinates are always normalized and must belong to [0.,1.].
# The (xmin, ymin) tuple stores the top-leftmost coordinates of the ROI, while (xmax, ymax) represents the
# bottom-rightmost coordinates.
# c is a confidence level (between 0. and 1.) associated to that ROI
Header header # Header timestamp should be acquisition time of the original image
float32 xmin
float32 ymin
float32 xmax
float32 ymax
float32 c
