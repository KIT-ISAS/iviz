cmake_minimum_required(VERSION 2.8.3)
project(iviz_msgs)

find_package(catkin REQUIRED COMPONENTS
	message_generation
	std_msgs
	geometry_msgs
	sensor_msgs
	rospy
)

add_message_files(DIRECTORY msg)
add_service_files(DIRECTORY srv)

generate_messages(
	DEPENDENCIES
	std_msgs
	geometry_msgs
	sensor_msgs
)

catkin_package(CATKIN_DEPENDS
	message_runtime
	std_msgs
	geometry_msgs
	sensor_msgs
)

