using System.Collections;
using UnityEngine;

namespace TMPro.Examples
{
    public class TextMeshProFloatingText : MonoBehaviour
    {
        private Vector3 lastPOS = Vector3.zero;
        private Quaternion lastRotation = Quaternion.identity;
        private Transform m_cameraTransform;

        private GameObject m_floatingText;
        private Transform m_floatingText_Transform;
        private TextMesh m_textMesh;
        private TextMeshPro m_textMeshPro;

        private Transform m_transform;

        public int SpawnType;
        public Font TheFont;

        //private int m_frame = 0;

        private void Awake()
        {
            m_transform = transform;
            m_floatingText = new GameObject(name + " floating text");

            // Reference to Transform is lost when TMP component is added since it replaces it by a RectTransform.
            //m_floatingText_Transform = m_floatingText.transform;
            //m_floatingText_Transform.position = m_transform.position + new Vector3(0, 15f, 0);

            m_cameraTransform = Camera.main.transform;
        }

        private void Start()
        {
            if (SpawnType == 0)
            {
                // TextMesh Pro Implementation
                m_textMeshPro = m_floatingText.AddComponent<TextMeshPro>();
                m_textMeshPro.rectTransform.sizeDelta = new Vector2(3, 3);

                m_floatingText_Transform = m_floatingText.transform;
                m_floatingText_Transform.position = m_transform.position + new Vector3(0, 15f, 0);

                //m_textMeshPro.fontAsset = Resources.Load("Fonts & Materials/JOKERMAN SDF", typeof(TextMeshProFont)) as TextMeshProFont; // User should only provide a string to the resource.
                //m_textMeshPro.fontSharedMaterial = Resources.Load("Fonts & Materials/LiberationSans SDF", typeof(Material)) as Material;

                m_textMeshPro.alignment = TextAlignmentOptions.Center;
                m_textMeshPro.color = new Color32((byte) Random.Range(0, 255), (byte) Random.Range(0, 255),
                    (byte) Random.Range(0, 255), 255);
                m_textMeshPro.fontSize = 24;
                //m_textMeshPro.enableExtraPadding = true;
                //m_textMeshPro.enableShadows = false;
                m_textMeshPro.enableKerning = false;
                m_textMeshPro.text = string.Empty;

                StartCoroutine(DisplayTextMeshProFloatingText());
            }
            else if (SpawnType == 1)
            {
                //Debug.Log("Spawning TextMesh Objects.");

                m_floatingText_Transform = m_floatingText.transform;
                m_floatingText_Transform.position = m_transform.position + new Vector3(0, 15f, 0);

                m_textMesh = m_floatingText.AddComponent<TextMesh>();
                m_textMesh.font = Resources.Load<Font>("Fonts/ARIAL");
                m_textMesh.GetComponent<Renderer>().sharedMaterial = m_textMesh.font.material;
                m_textMesh.color = new Color32((byte) Random.Range(0, 255), (byte) Random.Range(0, 255),
                    (byte) Random.Range(0, 255), 255);
                m_textMesh.anchor = TextAnchor.LowerCenter;
                m_textMesh.fontSize = 24;

                StartCoroutine(DisplayTextMeshFloatingText());
            }
            else if (SpawnType == 2)
            {
            }
        }


        //void Update()
        //{
        //    if (SpawnType == 0)
        //    {
        //        m_textMeshPro.SetText("{0}", m_frame);
        //    }
        //    else
        //    {
        //        m_textMesh.text = m_frame.ToString();
        //    }
        //    m_frame = (m_frame + 1) % 1000;

        //}


        public IEnumerator DisplayTextMeshProFloatingText()
        {
            var CountDuration = 2.0f; // How long is the countdown alive.    
            var starting_Count = Random.Range(5f, 20f); // At what number is the counter starting at.
            var current_Count = starting_Count;

            var start_pos = m_floatingText_Transform.position;
            Color32 start_color = m_textMeshPro.color;
            float alpha = 255;
            var int_counter = 0;


            var fadeDuration = 3 / starting_Count * CountDuration;

            while (current_Count > 0)
            {
                current_Count -= Time.deltaTime / CountDuration * starting_Count;

                if (current_Count <= 3) alpha = Mathf.Clamp(alpha - Time.deltaTime / fadeDuration * 255, 0, 255);

                int_counter = (int) current_Count;
                m_textMeshPro.text = int_counter.ToString();
                //m_textMeshPro.SetText("{0}", (int)current_Count);

                m_textMeshPro.color = new Color32(start_color.r, start_color.g, start_color.b, (byte) alpha);

                // Move the floating text upward each update
                m_floatingText_Transform.position += new Vector3(0, starting_Count * Time.deltaTime, 0);

                // Align floating text perpendicular to Camera.
                if (!lastPOS.Compare(m_cameraTransform.position, 1000) ||
                    !lastRotation.Compare(m_cameraTransform.rotation, 1000))
                {
                    lastPOS = m_cameraTransform.position;
                    lastRotation = m_cameraTransform.rotation;
                    m_floatingText_Transform.rotation = lastRotation;
                    var dir = m_transform.position - lastPOS;
                    m_transform.forward = new Vector3(dir.x, 0, dir.z);
                }

                yield return new WaitForEndOfFrame();
            }

            //Debug.Log("Done Counting down.");

            yield return new WaitForSeconds(Random.Range(0.1f, 1.0f));

            m_floatingText_Transform.position = start_pos;

            StartCoroutine(DisplayTextMeshProFloatingText());
        }


        public IEnumerator DisplayTextMeshFloatingText()
        {
            var CountDuration = 2.0f; // How long is the countdown alive.    
            var starting_Count = Random.Range(5f, 20f); // At what number is the counter starting at.
            var current_Count = starting_Count;

            var start_pos = m_floatingText_Transform.position;
            Color32 start_color = m_textMesh.color;
            float alpha = 255;
            var int_counter = 0;

            var fadeDuration = 3 / starting_Count * CountDuration;

            while (current_Count > 0)
            {
                current_Count -= Time.deltaTime / CountDuration * starting_Count;

                if (current_Count <= 3) alpha = Mathf.Clamp(alpha - Time.deltaTime / fadeDuration * 255, 0, 255);

                int_counter = (int) current_Count;
                m_textMesh.text = int_counter.ToString();
                //Debug.Log("Current Count:" + current_Count.ToString("f2"));

                m_textMesh.color = new Color32(start_color.r, start_color.g, start_color.b, (byte) alpha);

                // Move the floating text upward each update
                m_floatingText_Transform.position += new Vector3(0, starting_Count * Time.deltaTime, 0);

                // Align floating text perpendicular to Camera.
                if (!lastPOS.Compare(m_cameraTransform.position, 1000) ||
                    !lastRotation.Compare(m_cameraTransform.rotation, 1000))
                {
                    lastPOS = m_cameraTransform.position;
                    lastRotation = m_cameraTransform.rotation;
                    m_floatingText_Transform.rotation = lastRotation;
                    var dir = m_transform.position - lastPOS;
                    m_transform.forward = new Vector3(dir.x, 0, dir.z);
                }


                yield return new WaitForEndOfFrame();
            }

            //Debug.Log("Done Counting down.");

            yield return new WaitForSeconds(Random.Range(0.1f, 1.0f));

            m_floatingText_Transform.position = start_pos;

            StartCoroutine(DisplayTextMeshFloatingText());
        }
    }
}