# Informs that a planar table has been detected at a given location

Header header

# The pose gives you the transform that take you to the coordinate system
# of the table, with the origin somewhere in the table plane and the 
# z axis normal to the plane
geometry_msgs/Pose pose

# There is no guarantee that the table does NOT extend further than the
# convex hull; this is just as far as we've observed it.
# The origin of the table coordinate system is inside the convex hull

# Set of points forming the convex hull of the table
geometry_msgs/Point[] convex_hull
