using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Iviz.Controllers
{
    class ARHololensController : ARController
    {
        public override bool FindAnchor(in Vector3 position, out Vector3 anchor, out Vector3 normal)
        {
            anchor = new Vector3();
            normal = Vector3.up;
            return false;
        }
    }
}
