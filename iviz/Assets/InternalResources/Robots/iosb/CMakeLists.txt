cmake_minimum_required(VERSION 2.8.3)
project(e1_description)

find_package(catkin REQUIRED)

#######################################
## Declare ROS messages and services ##
#######################################


###################################
## catkin specific configuration ##
###################################

catkin_package(
#  INCLUDE_DIRS include
#  LIBRARIES omnirob_description
#  CATKIN_DEPENDS other_catkin_pkg
#  DEPENDS system_lib
)

###########
## Build ##
###########


#############
## Install ##
#############


# Mark other files for installation (e.g. launch and bag files, etc.)
install(FILES
  launch/e1_description.launch
  launch/e1_tf_broadcaster.launch
  launch/e1_dummy_gripper_tf_publisher.launch
  DESTINATION ${CATKIN_PACKAGE_SHARE_DESTINATION}/launch
)
install(FILES
  meshes/boom.dae
  meshes/boom_texture.png
  meshes/bucket.dae
  meshes/bucket_texture.png
  meshes/bucket_raedlinger.dae
  meshes/cabin.dae
  meshes/cabin_mesh.png
  meshes/grey_only.png
  meshes/intermediate.dae
  meshes/intermediate_texture.png
  meshes/kinshofer_body.dae
  meshes/kinshofer_gripper.dae
  meshes/kinshofer_rotator.dae
  meshes/quickcoupler_oq40.dae
  meshes/lever.dae
  meshes/motor-cover.png
  meshes/side-panel.png
  meshes/stick.dae
  meshes/stick_texture.png
  meshes/track_wheel.dae
  meshes/track_wheel_texture.png
  meshes/universalbox.dae
  meshes/WackerNeusonET18_new.png
  DESTINATION ${CATKIN_PACKAGE_SHARE_DESTINATION}/meshes
)
install(FILES
  DESTINATION ${CATKIN_PACKAGE_SHARE_DESTINATION}/Media/models
)
install(FILES
  urdf/e1_arm_joints.urdf
  urdf/e1_base_joints.urdf
  urdf/e1_cylinders.urdf
  urdf/e1_inverse_kinematics_arm_joints.urdf
  urdf/e1_inverse_kinematics_base_joints.urdf
  urdf/e1_inverse_kinematics_bucket.urdf.xacro
  urdf/e1_inverse_kinematics_gripper.urdf.xacro
  urdf/e1_links.urdf
  urdf/e1_oilquick_bucket_raedlinger.urdf.xacro
  urdf/e1_oilquick_gripper_kinshofer.urdf.xacro
  urdf/e1_sensors.urdf
  urdf/e1_without_tool.urdf.xacro
  urdf/oilquick_bucket_raedlinger.urdf
  DESTINATION ${CATKIN_PACKAGE_SHARE_DESTINATION}/urdf
)
