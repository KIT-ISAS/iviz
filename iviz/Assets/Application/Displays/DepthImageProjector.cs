using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace Iviz.App.Displays
{
    public class DepthImageProjector : MonoBehaviour, IDisplay
    {
        //const string MaterialResourcePath = "Displays/DepthImage Material";

        public Material material;

        /*
        public int K_width = 640;
        public int K_height = 480;
        float K_fx = 640 / 2 / Mathf.Tan(1f / 2);
        float K_fy = 640 / 2 / Mathf.Tan(1f / 2);
        float K_cx = 320;
        float K_cy = 240;
        */

        [Serializable]
        public class Configuration
        {
            public Resource.Module module => Resource.Module.DepthImageProjector;
            public string colorName = "";
            public string depthName = "";
            public float pointSize = 1f;
            public float fovAngle = 1.0f * Mathf.Rad2Deg;
        }

        readonly Configuration config = new Configuration();
        public Configuration Config
        {
            get => config;
            set
            {
                ColorName = value.colorName;
                DepthName = value.depthName;
                PointSize = value.pointSize;
                FovAngle = value.fovAngle;
            }
        }

        public string ColorName
        {
            get => config.colorName;
            set
            {
                config.colorName = value;
            }
        }

        public string DepthName
        {
            get => config.depthName;
            set
            {
                config.depthName = value;
            }
        }

        public float PointSize
        {
            get => config.pointSize;
            set
            {
                config.pointSize = value;
                UpdateQuadComputeBuffer();
            }
        }

        public float FovAngle
        {
            get => config.fovAngle;
            set
            {
                config.fovAngle = value;
                if (DepthImage != null)
                {
                    UpdatePosValues(DepthImage.Texture);
                }
            }
        }

        ImageListener colorImage;
        public ImageListener ColorImage
        {
            get => colorImage;
            set
            {
                if (colorImage != null)
                {
                    colorImage.ImageTexture.TextureChanged -= UpdateColorTexture;
                }
                colorImage = value;
                if (colorImage != null)
                {
                    UpdateColorTexture(colorImage.Texture);
                    colorImage.ImageTexture.TextureChanged += UpdateColorTexture;
                }
                else
                {
                    UpdateColorTexture(null);
                }
            }
        }

        ImageListener depthImage;
        public ImageListener DepthImage
        {
            get => depthImage;
            set
            {
                if (depthImage != null)
                {
                    depthImage.ImageTexture.TextureChanged -= UpdatePointComputeBuffers;
                    depthImage.ImageTexture.ColormapChanged -= UpdateColormap;
                }
                depthImage = value;
                if (depthImage != null)
                {
                    UpdateDepthTexture(depthImage.Texture);
                    UpdateColormap(depthImage.ColormapTexture);
                    depthImage.ImageTexture.TextureChanged += UpdateDepthTexture;
                    depthImage.ImageTexture.ColormapChanged += UpdateColormap;
                }
                else
                {
                    UpdateDepthTexture(null);
                }
            }
        }

        public string Name => "DepthImageProjector";


        public Bounds WorldBounds { get; private set; }
        public Bounds Bounds { get; private set; }
        public bool ColliderEnabled { get => false; set { } }

        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
        }

        int width, height;
        Vector2[] uvs = new Vector2[0];

        ComputeBuffer pointComputeBuffer;
        ComputeBuffer quadComputeBuffer;

        void Awake()
        {
            material = Instantiate(Resource.Materials.DepthImageProjector);

            Config = new Configuration();

            Debug.Log("Supports Compute Shaders: " + SystemInfo.supportsComputeShaders);
            Bounds = new Bounds(Vector3.zero, Vector3.one * 20);
        }

        static readonly int PropQuad = Shader.PropertyToID("_Quad");
        void UpdateQuadComputeBuffer()
        {
            Vector2[] quad = {
                    new Vector2( 0.5f,  0.5f),
                    new Vector2( 0.5f, -0.5f),
                    new Vector2(-0.5f, -0.5f),
                    new Vector2(-0.5f,  0.5f),
            };
            if (quadComputeBuffer == null)
            {
                Debug.Log("Building quadComputeBuffer");
                quadComputeBuffer = new ComputeBuffer(4, Marshal.SizeOf<Vector2>());
                material.SetBuffer(PropQuad, quadComputeBuffer);
            }
            quadComputeBuffer.SetData(quad, 0, 0, 4);
        }

        static readonly int PropColor = Shader.PropertyToID("_ColorTexture");
        void UpdateColorTexture(Texture2D texture)
        {
            material.SetTexture(PropColor, texture);

            if (texture == null)
            {
                material.EnableKeyword("USE_INTENSITY");
            }
            else
            {
                material.DisableKeyword("USE_INTENSITY");
            }
        }


        static readonly int PropDepth = Shader.PropertyToID("_DepthTexture");
        void UpdateDepthTexture(Texture2D texture)
        {
            material.SetTexture(PropDepth, texture);

            UpdatePointComputeBuffers(texture);
            UpdatePosValues(texture);
        }

        static readonly int PropPoints = Shader.PropertyToID("_Points");
        void UpdatePointComputeBuffers(Texture2D sourceTexture)
        {
            if (sourceTexture == null || (sourceTexture.width == width && sourceTexture.height == height))
            {
                return;
            }

            width = sourceTexture.width;
            height = sourceTexture.height;
            uvs = new Vector2[width * height];
            float invWidth = 1.0f / width;
            float invHeight = 1.0f / height;
            int off = 0;
            for (int v = 0; v < height; v++)
            {
                for (int u = 0; u < width; u++, off++)
                {
                    uvs[off] = new Vector2((u + 0.5f) * invWidth, (v + 0.5f) * invHeight);
                }
            }

            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
            }
            pointComputeBuffer = new ComputeBuffer(uvs.Length, Marshal.SizeOf<Vector2>());
            pointComputeBuffer.SetData(uvs, 0, 0, uvs.Length);
            material.SetBuffer(PropPoints, pointComputeBuffer);
        }

        static readonly int PropIntensity = Shader.PropertyToID("_IntensityTexture");
        void UpdateColormap(Texture2D texture)
        {
            material.SetTexture(PropIntensity, texture);
        }


        static readonly int PropIntensityCoeff = Shader.PropertyToID("_IntensityCoeff");
        static readonly int PropIntensityAdd = Shader.PropertyToID("_IntensityAdd");
        void UpdateIntensityValues(float intensityCoeff, float intensityAdd)
        {
            material.SetFloat(PropIntensityCoeff, intensityCoeff);
            material.SetFloat(PropIntensityAdd, intensityAdd);
        }

        static readonly int PropPointSize = Shader.PropertyToID("_PointSize");
        static readonly int PropPosST = Shader.PropertyToID("_Pos_ST");
        void UpdatePosValues(Texture2D texture)
        {
            if (texture == null)
            {
                return;
            }
            float ratio = (float)texture.height / texture.width;
            float posCoeff_X = 2 * Mathf.Tan(config.fovAngle * Mathf.Deg2Rad / 2);
            float posCoeff_Y = posCoeff_X * ratio;
            float posAdd_X = -0.5f * posCoeff_X;
            float posAdd_Y = -0.5f * posCoeff_Y;

            material.SetFloat(PropPointSize, posCoeff_X / texture.width * config.pointSize);
            material.SetVector(PropPosST, new Vector4(posCoeff_X, posCoeff_Y, posAdd_X, posAdd_Y));
        }


        static readonly int PropLocalToWorld = Shader.PropertyToID("_LocalToWorld");
        static readonly int PropWorldToLocal = Shader.PropertyToID("_WorldToLocal");
        void Update()
        {
            if (DepthImage == null || DepthImage.Texture == null)
            {
                return;
            }

            material.SetMatrix(PropLocalToWorld, transform.localToWorldMatrix);
            material.SetMatrix(PropWorldToLocal, transform.worldToLocalMatrix);

            WorldBounds = Utils.TransformBound(Bounds, transform);
            Graphics.DrawProcedural(material, WorldBounds, MeshTopology.Quads, 4, uvs.Length);
        }

        public void Stop()
        {
            if (colorImage != null)
            {
                colorImage.ImageTexture.TextureChanged -= UpdateColorTexture;
                colorImage = null;
            }
            if (depthImage != null)
            {
                depthImage.ImageTexture.TextureChanged -= UpdateDepthTexture;
                depthImage.ImageTexture.ColormapChanged -= UpdateColormap;
                depthImage = null;
            }
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
            }
            if (quadComputeBuffer != null)
            {
                quadComputeBuffer.Release();
                quadComputeBuffer = null;
            }

            width = 0;
            height = 0;
            uvs = new Vector2[0];
            Config = new Configuration();
        }

        void OnDestroy()
        {
            if (material != null)
            {
                Destroy(material);
            }
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
            }
            if (quadComputeBuffer != null)
            {
                quadComputeBuffer.Release();
            }
        }

        void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                return;
            }
            // unity bug causes all compute buffers to disappear when focus is lost
            if (pointComputeBuffer != null)
            {
                pointComputeBuffer.Release();
                pointComputeBuffer = null;
            }
            if (quadComputeBuffer != null)
            {
                quadComputeBuffer.Release();
                quadComputeBuffer = null;
            }

            uvs = new Vector2[0];
            width = 0;
            height = 0;

            UpdateQuadComputeBuffer();
            ColorImage = ColorImage;
            DepthImage = DepthImage;
            Debug.Log("DepthImageProjector: Rebuilding compute buffers");
        }

        /*
        void CameraInfoHandler(CameraInfo msg)
        {
            int new_K_width = (int)msg.width;
            int new_K_height = (int)msg.height;
            float new_K_fx = (float)msg.K[0];
            float new_K_fy = (float)msg.K[4];
            float new_K_cx = (float)msg.K[2];
            float new_K_cy = (float)msg.K[5];

            if (new_K_width != K_width || new_K_height != K_height ||
                new_K_fx != K_fx || new_K_fy != K_fy ||
                new_K_cx != K_cx || new_K_cy != K_cy)
            {
                K_width = new_K_width;
                K_height = new_K_height;
                K_fx = new_K_fx;
                K_fy = new_K_fy;
                K_cx = new_K_cx;
                K_cy = new_K_cy;
                RebuildMesh();
                Debug.Log("DepthImageListener: Rebuilding mesh");

                GetComponent<MeshRenderer>().enabled = true;
            }
        }
        */
    }
}