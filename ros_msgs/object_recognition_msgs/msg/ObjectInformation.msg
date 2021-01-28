############################################## VISUALIZATION INFO ######################################################
################### THIS INFO SHOULD BE OBTAINED INDEPENDENTLY FROM THE CORE, LIKE IN AN RVIZ PLUGIN ###################

# The human readable name of the object
string name

# The full mesh of the object: this can be useful for display purposes, augmented reality ... but it can be big
# Make sure the type is MESH
shape_msgs/Mesh ground_truth_mesh

# Sometimes, you only have a cloud in the DB
# Make sure the type is POINTS
sensor_msgs/PointCloud2 ground_truth_point_cloud
