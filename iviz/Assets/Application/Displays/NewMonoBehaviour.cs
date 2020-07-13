
using UnityEngine;


namespace Iviz.App
{


    public class NewMonoBehaviour : MonoBehaviour
    {
        void OnEnable()
        {
            string robotDescription = ConnectionManager.Connection.GetParameter("e1_description");
            Urdf.Robot robot = Urdf.Robot.Create(robotDescription);
        }
    }
}