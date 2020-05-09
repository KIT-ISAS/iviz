using Iviz.Msgs.grid_map_msgs;
using UnityEngine;

namespace Iviz.App
{
    /*
    public class GridMapListener : DisplayableListener
    {
        RosListener<GridMap> listener;
        RosSender<GridMap> sender;

        public int Width;
        public int Height;

        public Resource.ColormapId colorMap = Resource.ColormapId.hsv;

        Mesh mesh;
        Material material;

        Texture2D heightTexture;
        Texture2D intensityTexture;
        Texture2D colorMapTexture;

        // Start is called before the first frame update
        void Start()
        {
            listener = new RosListener<GridMap>(Topic, Handler);

            mesh = new Mesh();
            GetComponent<MeshFilter>().sharedMesh = mesh;

            material = Instantiate(Resources.Load<Material>("Displays/GridMap Material"));
            GetComponent<MeshRenderer>().material = material;

            transform.rotation = Quaternion.Euler(-90, 0, 0);

            colorMapTexture = Resource.Colormaps.Textures[colorMap];
            material.SetTexture("_ColorMapTexture", colorMapTexture);

            GameThread.EverySecond += UpdateStats;
        }

        public override void StartListening()
        {
            //Topic = topic;
        }

        void Handler(GridMap msg)
        {
            string parentId = msg.info.header.frame_id;
            transform.SetParentLocal(TFListener.GetOrCreateFrame(parentId, null).transform);

            int width = (int)(msg.info.length_x / msg.info.resolution + 0.5);
            int height = (int)(msg.info.length_y / msg.info.resolution + 0.5);

            EnsureSize(width, height);

            transform.localScale = new Vector3((float)msg.info.length_x, (float)msg.info.length_y, 1);
            transform.localPosition = new Vector3(-(float)msg.info.length_x/2, 0, (float)msg.info.length_y/2);
                       
            heightTexture.GetRawTextureData<float>().CopyFrom(msg.data[0].data);
            heightTexture.Apply();

            intensityTexture.GetRawTextureData<float>().CopyFrom(msg.data[0].data);
            intensityTexture.Apply();


            float min = float.MaxValue, max = float.MinValue;
            float[] array = msg.data[0].data;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < min) min = array[i];
                if (array[i] > max) max = array[i];
            }

            material.SetFloat("_MinIntensity", min);
            material.SetFloat("_InvSpanIntensity", 1f / (max - min));
        }

        void EnsureSize(int newWidth, int newHeight)
        {
            if (newWidth == Width && newHeight == Height)
            {
                return;
            }

            Width = newWidth;
            Height = newHeight;

            int size_p1 = (Width + 1) * (Height + 1);
            Vector3[] points = new Vector3[size_p1];
            float step_x = 1f / Width;
            float step_y = 1f / Height;
            for (int v = 0, off = 0; v <= Height; v++)
            {
                for (int u = 0; u <= Width; u++, off++)
                {
                    points[off] = new Vector3(
                        u * step_x,
                        v * step_y,
                        0
                        );
                }
            }

            int size = Width * Height;
            int[] indices = new int[size * 4];
            for (int v = 0; v < Height; v++)
            {
                int i_off = v * Width * 4;
                int p_off = v * (Width + 1);
                for (int u = 0; u < Width; u++, i_off += 4, p_off++)
                {
                    indices[i_off + 0] = p_off;
                    indices[i_off + 1] = p_off + 1;
                    indices[i_off + 2] = p_off + (Width + 1) + 1;
                    indices[i_off + 3] = p_off + (Width + 1);
                }
            }

            mesh.vertices = points;
            mesh.SetIndices(indices, MeshTopology.Quads, 0);
            mesh.Optimize();

            if (heightTexture != null)
            {
                Destroy(heightTexture);
            }
            heightTexture = new Texture2D(Width, Height, TextureFormat.RFloat, false);
            material.SetTexture("_HeightTexture", heightTexture);

            if (intensityTexture != null)
            {
                Destroy(intensityTexture);
            }
            intensityTexture = new Texture2D(Width, Height, TextureFormat.RFloat, false);
            material.SetTexture("_IntensityTexture", intensityTexture);
        }

        // Update is called once per frame
        void UpdateStats()
        {
            Connected = listener.Connected;
            Subscribed = listener.Subscribed;
            MessagesPerSecond = listener.UpdateStats().MessagesPerSecond;

            if (colorMap.ToString() != colorMapTexture.name)
            {
                colorMapTexture = Resources.Load<Texture2D>("colormaps/" + colorMap);
                if (colorMapTexture == null)
                {
                    Debug.LogError("Cannot find texture '" + colorMap + "'");
                }
                material.SetTexture("_ColorMapTexture", colorMapTexture);
            }
        }

        public override void Recycle()
        {
        }

        void OnDestroy()
        {
            if (mesh != null) Destroy(mesh);
            if (heightTexture != null) Destroy(heightTexture);
            if (intensityTexture != null) Destroy(intensityTexture);
            if (material != null) Destroy(material);
            listener?.Stop();
        }

        public override void Unsubscribe()
        {
            throw new System.NotImplementedException();
        }
    }
    */
}