using System;
using System.Linq;
using Iviz.Core;
using Iviz.Displays;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace VNC
{
    public class XRKeyboard : MonoBehaviour
    {
        [SerializeField] Transform row0;
        [SerializeField] Transform row1;
        [SerializeField] Transform row2;
        [SerializeField] Transform row3;
        [SerializeField] Transform row4;
        [SerializeField] Transform row5;
        [SerializeField] TMP_Text textTemplate;

        void Awake()
        {
            const float separator = 0.01f;
            const float size = 0.06f;

            float totalSize = 0;
            foreach (var child in row0.GetChildren())
            {
                totalSize += child.localScale.x;
            }

            totalSize += (row0.childCount - 1) * separator;

            float start0 = -totalSize / 2 - size / 2;

            void PositionRow([NotNull] Transform row)
            {
                float start = start0;
                row.localPosition = row.localPosition.WithX(0);
                foreach (var child in row.GetChildren())
                {
                    float localScaleX = child.localScale.x;
                    child.localPosition = child.localPosition.WithX(start + localScaleX / 2);
                    start += localScaleX + separator;
                }
            }

            PositionRow(row1);
            PositionRow(row2);
            PositionRow(row3);
            PositionRow(row4);

/*
            start = start0 - size / 2 - separator / 2;
            row1.localPosition = row1.localPosition.WithX(0);
            foreach (var child in row1.GetChildren())
            {
                float localScaleX = child.localScale.x;
                child.localPosition = child.localPosition.WithX(start + localScaleX / 2);
                start += localScaleX + separator;
            }
            
            start = start0 - size / 2 - separator / 2;
            row2.localPosition = row2.localPosition.WithX(0);
            foreach (var child in row2.GetChildren())
            {
                float localScaleX = child.localScale.x;
                child.localPosition = child.localPosition.WithX(start + localScaleX / 2);
                start += localScaleX + separator;
            }        
            
            start = start0 - size / 2 - separator / 2;
            row3.localPosition = row3.localPosition.WithX(0);
            foreach (var child in row3.GetChildren())
            {
                float localScaleX = child.localScale.x;
                child.localPosition = child.localPosition.WithX(start + localScaleX / 2);
                start += localScaleX + separator;
            }
            
            start = start0 - size / 2 - separator / 2;
            row4.localPosition = row4.localPosition.WithX(0);
            foreach (var child in row4.GetChildren())
            {
                float localScaleX = child.localScale.x;
                child.localPosition = child.localPosition.WithX(start + localScaleX / 2);
                start += localScaleX + separator;
            }
            */


            totalSize = 0;
            foreach (var child in row5.GetChildren())
            {
                totalSize += child.localScale.x;
            }

            totalSize += (row0.childCount - 1) * separator;

            start0 = -totalSize / 2 + size;
            PositionRow(row5);

            /*
            row5.localPosition = row5.localPosition.WithX(0);
            foreach (var child in row5.GetChildren())
            {
                float localScaleX = child.localScale.x;
                child.localPosition = child.localPosition.WithX(start + localScaleX / 2);
                start += localScaleX + separator;
            }
            */

            // --------

            float y = 2 * (size + separator) + 0.5f * size;
            row0.localPosition = row0.localPosition.WithY(y);

            y = 1 * (size + separator) + 0.5f * size;
            row1.localPosition = row1.localPosition.WithY(y);

            y = 0 * (size + separator) + 0.5f * size;
            row2.localPosition = row2.localPosition.WithY(y);

            y = -1 * (size + separator) + 0.5f * size;
            row3.localPosition = row3.localPosition.WithY(y);

            y = -2 * (size + separator) + 0.5f * size;
            row4.localPosition = row4.localPosition.WithY(y);

            y = -3 * (size + separator) + 0.5f * size;
            row5.localPosition = row5.localPosition.WithY(y);

            // --------

            void SetCharas([NotNull] Transform row, string[] charsRow)
            {
                int charOff = 0;
                foreach (var child in row.GetChildren().ToArray())
                {
                    if (charsRow[charOff++] is not { Length: > 0 } chars)
                    {
                        continue;
                    }

                    var textObject = Instantiate(textTemplate.gameObject).GetComponent<TMP_Text>();
                    textObject.transform.SetParentLocal(child.parent);
                    textObject.transform.localPosition = child.localPosition;
                    textObject.text = chars;
                    textObject.gameObject.SetActive(true);
                }
            }

            string[] charsRow1 = { "^", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "ß", "´", "Back" };
            SetCharas(row1, charsRow1);

            /*
            int charOff = 0;
            foreach (var child in row1.GetChildren().ToArray())
            {
                var textObject = Instantiate(textTemplate.gameObject).GetComponent<TMP_Text>();
                textObject.transform.SetParentLocal(child.parent);
                textObject.transform.localPosition = child.localPosition;
                textObject.text = charsRow1[charOff++];
                textObject.gameObject.SetActive(true);
            }
            */

            string[] charsRow2 = { "Tab", "Q", "W", "E", "R", "T", "Z", "U", "I", "O", "P", "Ü", "+", "Enter" };
            SetCharas(row2, charsRow2);

            string[] charsRow3 = { "Caps Lk", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Ö", "Ä", "'", "" };
            SetCharas(row3, charsRow3);

            string[] charsRow4 = { "Shift", "<", "Y", "X", "C", "V", "B", "N", "M", ",", ".", "-", "Shift" };
            SetCharas(row4, charsRow4);

            string[] charsRow5 = { "Ctrl", "Win", "Alt", "", "Alt", "Ctrl" };
            SetCharas(row5, charsRow5);

            /*
            charOff = 0;
            foreach (var child in row2.GetChildren().ToArray())
            {
                var textObject = Instantiate(textTemplate.gameObject).GetComponent<TMP_Text>();
                textObject.transform.SetParentLocal(child.parent);
                textObject.transform.localPosition = child.localPosition.WithZ(-0.002f);
                textObject.text = charsRow2[charOff++];
                textObject.gameObject.SetActive(true);
            }
            
            charOff = 0;
            foreach (var child in row3.GetChildren().ToArray())
            {
                var textObject = Instantiate(textTemplate.gameObject).GetComponent<TMP_Text>();
                textObject.transform.SetParentLocal(child.parent);
                textObject.transform.localPosition = child.localPosition.WithZ(-0.002f);
                textObject.text = charsRow3[charOff++];
                textObject.gameObject.SetActive(true);
            }
            */
        }

        void SetDisplay(LineDisplay display, float width, float height, float radius)
        {
            Span<LineWithColor> lines = stackalloc LineWithColor[20 * 4 + 4];
            const float q = Mathf.PI / 2;

            Span<Vector3> p0 = stackalloc Vector3[]
            {
                new Vector3(-width / 2 + radius, height / 2 + radius, 0),
                new Vector3(width / 2 + radius, height / 2 + radius, 0),
                new Vector3(width / 2 + radius, -height / 2 + radius, 0),
                new Vector3(-width / 2 + radius, -height / 2 + radius, 0),
            };

            for (int j = 0; j < 4; j++)
            {
                var p = p0[j];
                for (int i = 0; i < 20; i++)
                {
                    float a = (i + 0) * q / 20 + q * j;
                    float x0 = Mathf.Cos(a);
                    float y0 = Mathf.Sin(a);

                    float b = (i + 1) * q / 20 + q * j;
                    float x1 = Mathf.Cos(b);
                    float y1 = Mathf.Sin(b);

                    lines[i] = new LineWithColor(new Vector3(x0, y0, 0), new Vector3(x1, y1, 0));
                }
            }

            //lines[i] = new LineWithColor(new Vector3(x0, y0, 0), new Vector3(x1, y1, 0));
        }
    }
}